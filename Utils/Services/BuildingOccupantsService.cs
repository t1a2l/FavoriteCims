using System;
using System.Collections.Generic;
using ColossalFramework;
using static FavoriteCims.FavCimsCore;

namespace FavoriteCims.Utils.Services
{
    public static class BuildingOccupantsService
    {
        private const int MaxUnitTraversal = 524288;

        public static bool TryRead(InstanceID buildingInstance, out BuildingOccupantsSnapshot snapshot)
        {
            snapshot = new BuildingOccupantsSnapshot();

            if (buildingInstance.IsEmpty || buildingInstance.Type != InstanceType.Building || buildingInstance.Building == 0)
            {
                return false;
            }

            try
            {
                var buildingManager = Singleton<BuildingManager>.instance;
                var citizenManager = Singleton<CitizenManager>.instance;

                ushort buildingId = buildingInstance.Building;

                if (buildingId >= buildingManager.m_buildings.m_buffer.Length)
                {
                    return false;
                }

                ref Building building = ref buildingManager.m_buildings.m_buffer[buildingId];

                if ((building.m_flags & Building.Flags.Created) == 0)
                {
                    return false;
                }

                var buildingInfo = building.Info;

                if (buildingInfo == null || buildingInfo.m_class == null)
                {
                    return false;
                }

                snapshot.BuildingId = buildingId;
                snapshot.IsValid = true;

                snapshot.IsResidential = buildingInfo.m_class.m_service == ItemClass.Service.Residential;
                snapshot.IsHotel = FavCimsCore.IsHotel(buildingId);
                snapshot.IsCimCare = FavCimsCore.IsCimCareBuilding(buildingId);
                snapshot.IsInternationalTradeOffice = FavCimsCore.IsInternationalTradeOfficeBuilding(buildingId);

                snapshot.Title = GetBuildingTitle(buildingId, buildingInfo, snapshot.IsResidential, snapshot.IsAreaResidential(), snapshot.IsCimCare);

                var seenCitizens = new HashSet<uint>();
                var residentsByUnit = new Dictionary<uint, BuildingOccupantGroup>();

                uint unitId = building.m_citizenUnits;
                int unitGuard = 0;

                while (unitId != 0 && unitGuard < MaxUnitTraversal)
                {
                    uint nextUnitId = citizenManager.m_units.m_buffer[(int)unitId].m_nextUnit;

                    for (int slot = 0; slot < 5; slot++)
                    {
                        uint citizenId = citizenManager.m_units.m_buffer[(int)unitId].GetCitizen(slot);

                        if (citizenId == 0 || !seenCitizens.Add(citizenId))
                        {
                            continue;
                        }

                        if (!TryReadOccupant(buildingId, citizenId, unitId, slot, buildingInfo, snapshot, out BuildingOccupant occupant))
                        {
                            continue;
                        }

                        AddOccupant(unitId, ref occupant, residentsByUnit, snapshot);
                    }

                    unitId = nextUnitId;
                    unitGuard++;
                }

                FinalizeCounts(snapshot);

                return true;
            }
            catch (Exception ex)
            {
                Debug.Error("Failed to read building occupants: " + ex);
                return false;
            }
        }

        private static bool TryReadOccupant(ushort buildingId, uint citizenId, uint unitId, int slotIndex, BuildingInfo buildingInfo, BuildingOccupantsSnapshot snapshot, out BuildingOccupant occupant)
        {
            occupant = default;

            var citizenManager = Singleton<CitizenManager>.instance;
            var buildingManager = Singleton<BuildingManager>.instance;

            if (citizenId >= citizenManager.m_citizens.m_buffer.Length)
            {
                return false;
            }

            ref Citizen citizen = ref citizenManager.m_citizens.m_buffer[(int)citizenId];

            if ((citizen.m_flags & Citizen.Flags.Created) == 0)
            {
                return false;
            }

            occupant.CitizenId = citizenId;
            occupant.UnitId = unitId;
            occupant.SlotIndex = slotIndex;

            occupant.UnitFlags = citizenManager.m_units.m_buffer[(int)unitId].m_flags;
            occupant.Location = citizen.CurrentLocation;

            occupant.IsTourist = (citizen.m_flags & Citizen.Flags.Tourist) != 0;
            occupant.IsDead = citizen.Dead;
            occupant.IsArrested = citizen.Arrested;
            occupant.IsCriminal = citizen.Criminal;

            occupant.HomeBuilding = citizen.m_homeBuilding;
            occupant.WorkBuilding = citizen.m_workBuilding;

            occupant.Name = citizenManager.GetCitizenName(citizenId);

            if (!BuildingPresenceService.TryRead(citizenId, buildingId, out BuildingPresenceState presence))
            {
                presence = BuildingPresenceState.Away;
            }

            occupant.Presence = presence;

            var citizenInfo = citizen.GetCitizenInfo(citizenId);

            if (citizenInfo?.m_citizenAI == null)
            {
                occupant.Status = string.Empty;
                occupant.TargetInstanceId = InstanceID.Empty;
                occupant.TargetName = string.Empty;
            }
            else
            {
                occupant.Status = citizenInfo.m_citizenAI.GetLocalizedStatus(citizenId, ref citizen, out occupant.TargetInstanceId);

                occupant.TargetName = occupant.TargetInstanceId.IsEmpty ? string.Empty : buildingManager.GetBuildingName(occupant.TargetInstanceId.Building, new InstanceID { Citizen = citizenId });
            }

            occupant.Role = GetRole(buildingId, buildingInfo, snapshot, citizenId, ref citizen, occupant.UnitFlags);

            return occupant.Role != BuildingOccupantRole.Unknown;
        }

        private static BuildingOccupantRole GetRole(ushort buildingId, BuildingInfo buildingInfo, BuildingOccupantsSnapshot snapshot, uint citizenId, ref Citizen citizen, CitizenUnit.Flags unitFlags)
        {
            bool isStudent = citizen.GetCurrentSchoolLevel(citizenId) != ItemClass.Level.None;

            if (isStudent && citizen.m_workBuilding == buildingId && (buildingInfo.m_class.m_service == ItemClass.Service.Education || buildingInfo.m_class.m_service == ItemClass.Service.PlayerEducation))
            {
                return BuildingOccupantRole.Student;
            }

            if ((unitFlags & CitizenUnit.Flags.Home) != 0 && (snapshot.IsResidential || snapshot.IsAreaResidential()) || ((unitFlags & (CitizenUnit.Flags)CimCareFlags.NursingHome) != 0 || (unitFlags & (CitizenUnit.Flags)CimCareFlags.Orphanage) != 0) && snapshot.IsCimCare)
            {
                return BuildingOccupantRole.Resident;
            }

            if ((unitFlags & CitizenUnit.Flags.Hotel) != 0 && snapshot.IsHotel)
            {
                return BuildingOccupantRole.HotelGuest;
            }

            if ((unitFlags & CitizenUnit.Flags.Visit) != 0)
            {
                return BuildingOccupantRole.Visitor;
            }

            if (citizen.m_workBuilding == buildingId)
            {
                return BuildingOccupantRole.Employee;
            }

            return BuildingOccupantRole.Unknown;
        }

        private static void AddOccupant(uint unitId, ref BuildingOccupant occupant, Dictionary<uint, BuildingOccupantGroup> residentsByUnit, BuildingOccupantsSnapshot snapshot)
        {
            switch (occupant.Role)
            {
                case BuildingOccupantRole.Resident:
                    AddResident(unitId, ref occupant, residentsByUnit, snapshot);
                    break;

                case BuildingOccupantRole.Employee:
                    snapshot.Employees.Add(occupant);
                    break;

                case BuildingOccupantRole.Visitor:
                    snapshot.Visitors.Add(occupant);
                    break;

                case BuildingOccupantRole.HotelGuest:
                    snapshot.HotelGuests.Add(occupant);
                    break;

                case BuildingOccupantRole.Student:
                    snapshot.Students.Add(occupant);
                    break;
            }

            snapshot.TotalOccupantCount++;
        }

        private static void AddResident(uint unitId, ref BuildingOccupant occupant, Dictionary<uint, BuildingOccupantGroup> residentsByUnit, BuildingOccupantsSnapshot snapshot)
        {
            if (!residentsByUnit.TryGetValue(unitId, out BuildingOccupantGroup group))
            {
                group = new BuildingOccupantGroup
                {
                    UnitId = unitId,
                    Title = FavCimsLang.Text("OnBuildingResidential") + (residentsByUnit.Count + 1)
                };

                residentsByUnit.Add(unitId, group);
                snapshot.ResidentGroups.Add(group);
            }

            group.Members.Add(occupant);
        }

        private static void FinalizeCounts(BuildingOccupantsSnapshot snapshot)
        {
            foreach (var employee in snapshot.Employees)
            {
                if (employee.IsPresent)
                {
                    snapshot.PresentEmployeeCount++;
                }

                snapshot.AssignedEmployeeCount++;
            }

            foreach (var visitor in snapshot.Visitors)
            {
                if (visitor.IsPresent)
                {
                    snapshot.PresentVisitorCount++;
                }

                if (visitor.IsHeadingHere)
                {
                    snapshot.IncomingVisitorCount++;
                }
            }

            foreach (var hotelGuest in snapshot.HotelGuests)
            {
                snapshot.RegisteredHotelGuestCount++;

                if (hotelGuest.IsPresent)
                {
                    snapshot.PresentHotelGuestCount++;
                }
            }
        }

        private static string GetBuildingTitle(ushort buildingId, BuildingInfo buildingInfo, bool isResidential, bool isAreaResidential, bool isCimCare)
        {
            if (isResidential || isAreaResidential || isCimCare)
            {
                return FavCimsLang.Text("CitizensHouseHoldsTitle");
            }

            if (FavCimsCore.IsHotel(buildingId))
            {
                return FavCimsLang.Text("CitizenOnHotelBuildingTitle");
            }

            switch (buildingInfo.m_class.m_service)
            {
                case ItemClass.Service.PoliceDepartment when buildingInfo.m_class.m_level == ItemClass.Level.Level3:
                case ItemClass.Service.HealthCare when buildingInfo.m_class.m_level == ItemClass.Level.Level3:
                case ItemClass.Service.FireDepartment when buildingInfo.m_class.m_level == ItemClass.Level.Level3:
                    return FavCimsLang.Text("OnHelicopterBuildingService");

                case ItemClass.Service.PoliceDepartment when buildingInfo.m_class.m_level == ItemClass.Level.Level4:
                    return FavCimsLang.Text("OnPrisonBuildingService");

                case ItemClass.Service.PoliceDepartment:
                    return FavCimsLang.Text("OnPoliceBuildingService");

                case ItemClass.Service.FireDepartment:
                    return FavCimsLang.Text("OnFireBuildingService");

                case ItemClass.Service.Disaster when buildingInfo.GetAI() is ShelterAI:
                    return FavCimsLang.Text("OnRescueBuildingService");

                case ItemClass.Service.Disaster:
                    return FavCimsLang.Text("OnShelterBuildingService");

                case ItemClass.Service.Education:
                    return FavCimsLang.Text("OnEducationBuildingService");

                case ItemClass.Service.PlayerEducation when !isAreaResidential:
                    return FavCimsLang.Text("OnHighEducationBuildingService");

                case ItemClass.Service.HealthCare:
                    return FavCimsLang.Text("OnMedicalBuildingService");

                case ItemClass.Service.Beautification:
                    return FavCimsLang.Text("OnBuildingGuests");

                case ItemClass.Service.Industrial:
                case ItemClass.Service.PlayerIndustry:
                    return FavCimsLang.Text("OnBuildingWorkers");

                default:
                    return FavCimsLang.Text("CitizenOnBuildingTitle");
            }
        }
    }

    internal static class BuildingOccupantsSnapshotExtensions
    {
        public static bool IsAreaResidential(this BuildingOccupantsSnapshot snapshot)
        {
            return snapshot != null && !snapshot.IsResidential && FavCimsCore.IsAreaResidentalBuilding(snapshot.BuildingId);
        }
    }
}