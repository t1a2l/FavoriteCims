using System;
using System.Collections.Generic;
using ColossalFramework;

namespace FavoriteCims.Utils.Services
{
    public static class FamilyPanelDataService
    {
        public static bool TryRead(InstanceID selectedCitizen, out FamilyPanelData data)
        {
            data = null;

            if (selectedCitizen.IsEmpty || selectedCitizen.Type != InstanceType.Citizen || selectedCitizen.Citizen == 0)
            {
                return false;
            }

            try
            {
                var snapshot = new FamilyPanelData();

                if (!TryReadCitizen(selectedCitizen.Citizen, -1, out snapshot.SelectedCitizen))
                {
                    return false;
                }

                ushort homeId = snapshot.SelectedCitizen.HomeBuilding;

                if (homeId == 0)
                {
                    snapshot.State = FamilyPanelReadState.NoHome;
                    return CompleteSnapshot(snapshot, out data);
                }

                if (!TryReadHome(homeId, selectedCitizen, out snapshot.Home))
                {
                    snapshot.State = FamilyPanelReadState.HomeUnavailable;
                    return CompleteSnapshot(snapshot, out data);
                }

                if (!TryReadHousehold(snapshot))
                {
                    return false;
                }

                return CompleteSnapshot(snapshot, out data);
            }
            catch (Exception ex)
            {
                Debug.Error("Failed to read family panel data: " + ex);
                return false;
            }
        }

        private static bool TryReadCitizen(uint citizenId, int householdSlot, out FamilyCitizenData data)
        {
            data = null;

            var manager = Singleton<CitizenManager>.instance;
            var citizens = manager.m_citizens.m_buffer;

            if (citizenId == 0 || citizenId >= (uint)citizens.Length)
            {
                return false;
            }

            ref Citizen citizen = ref citizens[(int)citizenId];

            if ((citizen.m_flags & Citizen.Flags.Created) == 0)
            {
                return false;
            }

            var result = new FamilyCitizenData
            {
                CitizenId = citizenId,
                HouseholdSlot = householdSlot,
                InstanceId = new InstanceID { Citizen = citizenId },

                Name = manager.GetCitizenName(citizenId) ?? string.Empty,

                Gender = Citizen.GetGender(citizenId),
                Education = citizen.EducationLevel,
                Wealth = citizen.WealthLevel,

                GameAge = citizen.m_age,
                RealAge = FavCimsCore.CalculateCitizenAge(citizen.m_age),

                Health = citizen.m_health,
                Wellbeing = citizen.m_wellbeing,

                Happiness = Citizen.GetHappiness(citizen.m_health, citizen.m_wellbeing),

                IsDead = citizen.Dead,
                IsArrested = citizen.Arrested,
                IsCriminal = citizen.Criminal,

                IsStudent = citizen.GetCurrentSchoolLevel(citizenId) != ItemClass.Level.None,

                IsFavorite = FavCimsCore.IsFavorite(citizenId),

                HomeBuilding = citizen.m_homeBuilding,
                WorkBuilding = citizen.m_workBuilding,
                Location = citizen.CurrentLocation
            };

            var info = citizen.GetCitizenInfo(citizenId);

            if (info != null && info.m_citizenAI != null)
            {
                result.Status = info.m_citizenAI.GetLocalizedStatus(citizenId, ref citizen, out result.TargetInstanceId) ?? string.Empty;
                ReadTargetBuilding(result);
            }

            ReadVehicles(citizenId, ref citizen, result);

            result.Workplace = FamilyWorkplaceDataService.Read(result);
            result.Activity = FamilyActivityDataService.Read(result);

            data = result;
            return true;
        }

        private static bool TryReadHome(ushort buildingId, InstanceID citizenInstance, out FamilyHomeData data)
        {
            data = null;

            var manager = Singleton<BuildingManager>.instance;
            var buildings = manager.m_buildings.m_buffer;

            if (buildingId == 0 || buildingId >= buildings.Length)
            {
                return false;
            }

            var building = buildings[buildingId];

            if ((building.m_flags & Building.Flags.Created) == 0)
            {
                return false;
            }

            var info = building.Info;

            if (info == null || info.m_class == null)
            {
                return false;
            }

            var districts = Singleton<DistrictManager>.instance;
            byte districtId = districts.GetDistrict(building.m_position);

            data = new FamilyHomeData
            {
                BuildingId = buildingId,
                InstanceId = new InstanceID { Building = buildingId },

                Name = manager.GetBuildingName(buildingId, citizenInstance) ?? string.Empty,

                Service = info.m_class.m_service,
                SubService = info.m_class.m_subService,
                Level = info.m_class.m_level,

                DistrictId = districtId,

                DistrictName = districtId == 0 ? string.Empty : districts.GetDistrictName(districtId) ?? string.Empty,

                Problems = building.m_problems
            };

            return true;
        }

        private static bool TryReadHousehold(FamilyPanelData snapshot)
        {
            var citizens = Singleton<CitizenManager>.instance;
            var buildings = Singleton<BuildingManager>.instance;

            var units = citizens.m_units.m_buffer;

            uint unitId = buildings.m_buildings.m_buffer[snapshot.Home.BuildingId].m_citizenUnits;

            var visitedUnits = new HashSet<uint>();

            while (unitId != 0)
            {
                if (unitId >= (uint)units.Length || !visitedUnits.Add(unitId))
                {
                    Debug.Warning("Invalid household citizen-unit chain for building " + snapshot.Home.BuildingId + ".");

                    return false;
                }

                var unit = units[(int)unitId];

                if ((unit.m_flags & CitizenUnit.Flags.Home) != 0 && ContainsCitizen(ref unit, snapshot.SelectedCitizen.CitizenId))
                {
                    snapshot.HouseholdUnitId = unitId;

                    var seenCitizens = new HashSet<uint>();

                    for (int slot = 0; slot < 5; slot++)
                    {
                        uint memberId = unit.GetCitizen(slot);

                        if (memberId == 0 || !seenCitizens.Add(memberId))
                        {
                            continue;
                        }

                        FamilyCitizenData member;

                        if (memberId == snapshot.SelectedCitizen.CitizenId)
                        {
                            member = snapshot.SelectedCitizen;
                            member.HouseholdSlot = slot;
                        }
                        else if (!TryReadCitizen(memberId, slot, out member))
                        {
                            continue;
                        }

                        // Ignore stale membership pointing at another home.
                        if (member.HomeBuilding != snapshot.Home.BuildingId)
                        {
                            continue;
                        }

                        snapshot.Household.Add(member);
                    }

                    snapshot.State = FamilyPanelReadState.Ready;
                    return true;
                }

                unitId = unit.m_nextUnit;
            }

            snapshot.State = FamilyPanelReadState.NoHousehold;
            return true;
        }

        private static bool ContainsCitizen(ref CitizenUnit unit, uint citizenId)
        {
            for (int slot = 0; slot < 5; slot++)
            {
                if (unit.GetCitizen(slot) == citizenId)
                {
                    return true;
                }
            }

            return false;
        }

        private static void ReadTargetBuilding(FamilyCitizenData citizen)
        {
            var target = citizen.TargetInstanceId;

            if (target.IsEmpty || target.Type != InstanceType.Building || target.Building == 0)
            {
                return;
            }

            var buildings = Singleton<BuildingManager>.instance;
            var buffer = buildings.m_buildings.m_buffer;

            if (target.Building >= buffer.Length)
            {
                citizen.TargetInstanceId = InstanceID.Empty;
                return;
            }

            var building = buffer[target.Building];

            if ((building.m_flags & Building.Flags.Created) == 0)
            {
                citizen.TargetInstanceId = InstanceID.Empty;
                return;
            }

            citizen.TargetName = buildings.GetBuildingName(target.Building, citizen.InstanceId) ?? string.Empty;

            var districts = Singleton<DistrictManager>.instance;
            byte districtId = districts.GetDistrict(building.m_position);

            citizen.TargetDistrictName = districtId == 0 ? string.Empty : districts.GetDistrictName(districtId) ?? string.Empty;
        }

        private static void ReadVehicles(uint citizenId, ref Citizen citizen, FamilyCitizenData data)
        {
            var manager = Singleton<VehicleManager>.instance;

            ushort vehicleId = citizen.m_vehicle;
            var vehicles = manager.m_vehicles.m_buffer;

            if (vehicleId != 0 && vehicleId < vehicles.Length)
            {
                ref Vehicle vehicle = ref vehicles[vehicleId];

                if ((vehicle.m_flags & Vehicle.Flags.Created) != 0)
                {
                    var info = vehicle.Info;

                    if (info != null && info.m_vehicleAI != null)
                    {
                        data.CurrentVehicleInstanceId = new InstanceID { Vehicle = vehicleId };
                        data.CurrentVehicleName = manager.GetVehicleName(vehicleId) ?? string.Empty;

                        var owner = info.m_vehicleAI.GetOwnerID(vehicleId, ref vehicle);

                        if (owner.Type == InstanceType.Citizen && owner.Citizen == citizenId)
                        {
                            data.OwnedVehicleInstanceId = data.CurrentVehicleInstanceId;
                            data.OwnedVehicleName = data.CurrentVehicleName;

                            return;
                        }
                    }
                }
            }

            ushort parkedId = citizen.m_parkedVehicle;
            var parkedVehicles = manager.m_parkedVehicles.m_buffer;

            if (parkedId == 0 || parkedId >= parkedVehicles.Length)
            {
                return;
            }

            var parked = parkedVehicles[parkedId];

            if (parked.m_ownerCitizen != citizenId || parked.Info == null)
            {
                return;
            }

            data.OwnedVehicleInstanceId = new InstanceID { ParkedVehicle = parkedId };

            data.OwnedVehicleName = manager.GetParkedVehicleName(parkedId) ?? string.Empty;

            data.OwnedVehicleIsParked = true;
        }

        private static bool CompleteSnapshot(FamilyPanelData snapshot, out FamilyPanelData data)
        {
            snapshot.Pet = FamilyPetDataService.Read(snapshot);

            data = snapshot;
            return true;
        }
    }
}