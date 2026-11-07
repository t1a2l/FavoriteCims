using System;
using ColossalFramework;
using ColossalFramework.UI;
using UnityEngine;

namespace FavoriteCims.Utils.Services
{
    public static class CitizenRowDataService
    {
        /// <summary>Reads a complete row snapshot. Building cells are built as values before assignment.</summary>
        public static bool TryRead(InstanceID citizenInstance, out CitizenRowData data)
        {
            data = default;
            if (citizenInstance.IsEmpty || citizenInstance.Type != InstanceType.Citizen)
            { 
                return false; 
            }

            try
            {
                var citizenManager = Singleton<CitizenManager>.instance;
                var buildingManager = Singleton<BuildingManager>.instance;
                var vehicleManager = Singleton<VehicleManager>.instance;
                uint citizenId = citizenInstance.Citizen;
                var citizens = citizenManager.m_citizens.m_buffer;
                if (citizenId == 0 || citizenId >= (uint)citizens.Length) return false;
                ref Citizen citizen = ref citizens[(int)citizenId];
                if ((citizen.m_flags & Citizen.Flags.Created) == 0) return false;

                data.CitizenId = citizenId;
                data.Name = citizenManager.GetCitizenName(citizenId);
                data.NameTooltip = data.Name;
                data.Gender = Citizen.GetGender(citizenId);
                data.Health = citizen.m_health;
                data.Wellbeing = citizen.m_wellbeing;
                data.GameAge = citizen.m_age;
                data.Education = citizen.EducationLevel;
                data.Happiness = Citizen.GetHappiness(data.Health, data.Wellbeing);
                data.HappinessLevel = Citizen.GetHappinessLevel(data.Happiness);
                data.RealAge = FavCimsCore.CalculateCitizenAge(data.GameAge);
                data.IsDead = citizen.Dead;
                data.IsArrested = citizen.Arrested;
                data.IsCriminal = citizen.Criminal;
                data.IsTourist = (citizen.m_flags & Citizen.Flags.Tourist) != 0;
                data.IsStudent = citizen.GetCurrentSchoolLevel(citizenId) != ItemClass.Level.None;
                data.Vehicle = citizen.m_vehicle;

                data.Home = ReadHome(citizenInstance, citizen.m_homeBuilding, buildingManager);
                data.Workplace = ReadWorkplace(citizenInstance, citizen.m_workBuilding, data.IsStudent, buildingManager);
                ReadActivity(citizenInstance, citizenId, ref citizen, buildingManager, vehicleManager, ref data);
                if (data.IsDead) ReadDeathState(ref data);

                return true;
            }
            catch (Exception ex)
            {
                data = default;
                Debug.Error("Failed to read citizen row data: " + ex);
                return false;
            }
        }

        /// <summary>Creates the common empty presentation without assigning a navigation target.</summary>
        private static BuildingCellData EmptyBuildingCell()
        {
            return new BuildingCellData
            {
                DisplayName = string.Empty,
                TextTooltip = string.Empty,
                TypeTooltip = string.Empty,
                DistrictTooltip = string.Empty,
                TextColor = new Color32(21, 59, 96, 140)
            };
        }

        private static BuildingCellData ReadHome(InstanceID citizenInstance, ushort buildingId, BuildingManager buildingManager)
        {
            var home = EmptyBuildingCell();
            if (!TryReadBuilding(buildingId, buildingManager, out Building building, out BuildingInfo info)) return home;

            home.BuildingId = buildingId;
            home.DisplayName = buildingManager.GetBuildingName(buildingId, citizenInstance);
            home.TextTooltip = FavCimsLang.Text("CitizenHomeLabel") + home.DisplayName;
            home.TypeTooltip = home.DisplayName;
            home.DistrictId = Singleton<DistrictManager>.instance.GetDistrict(building.m_position);
            home.DistrictTooltip = GetDistrictTooltip(home.DistrictId);
            if (info.m_class.m_service == ItemClass.Service.Residential)
            {
                home.Icon = BuildingIconData.FromSprite(MyAtlas.FavCimsAtlas, info.m_class.m_level >= ItemClass.Level.Level3 ? "ResidentialHighIcon" : "ResidentialLowIcon");
                home.LevelTexture = GetLevelTexture(TextureDB.FavCimsResidentialLevel, info.m_class.m_level);
            }
            return home;
        }

        /// <summary>Reads workplace presentation, using one base image rather than two category icons.</summary>
        private static BuildingCellData ReadWorkplace(InstanceID citizenInstance, ushort buildingId, bool isStudent, BuildingManager buildingManager)
        {
            var workplace = EmptyBuildingCell();
            if (!TryReadBuilding(buildingId, buildingManager, out Building building, out BuildingInfo info)) return workplace;

            workplace.BuildingId = buildingId;
            workplace.DisplayName = buildingManager.GetBuildingName(buildingId, citizenInstance);
            workplace.TextTooltip = FavCimsLang.Text("CitizenGenericWorkplaceTooltip");
            workplace.TypeTooltip = workplace.DisplayName;
            workplace.DistrictId = Singleton<DistrictManager>.instance.GetDistrict(building.m_position);
            workplace.DistrictTooltip = GetDistrictTooltip(workplace.DistrictId);

            if (isStudent)
            {
                workplace.Icon = BuildingIconData.FromTexture(TextureDB.FavCimsWorkingPlaceTextureStudent);
                return workplace;
            }

            switch (info.m_class.m_service)
            {
                case ItemClass.Service.Commercial:
                    workplace.Icon = BuildingIconData.FromTexture(info.m_class.m_subService == ItemClass.SubService.CommercialHigh ? TextureDB.FavCimsCitizenCommercialHighTexture : TextureDB.FavCimsCitizenCommercialLowTexture);
                    workplace.TextColor = new Color32(0, 51, 153, 140);
                    workplace.LevelTexture = GetLevelTexture(TextureDB.FavCimsCommercialLevel, info.m_class.m_level);
                    break;
                case ItemClass.Service.Industrial:
                    workplace.Icon = BuildingIconData.FromTexture(TextureDB.FavCimsCitizenIndustrialGenericTexture);
                    workplace.TextColor = new Color32(102, 51, 0, 140);
                    workplace.LevelTexture = GetLevelTexture(TextureDB.FavCimsIndustrialLevel, info.m_class.m_level);
                    break;
                case ItemClass.Service.Office:
                    workplace.Icon = BuildingIconData.FromTexture(TextureDB.FavCimsCitizenOfficeTexture);
                    workplace.TextColor = new Color32(0, 102, 153, 140);
                    workplace.LevelTexture = GetLevelTexture(TextureDB.FavCimsOfficeLevel, info.m_class.m_level);
                    break;
                default:
                    workplace.Icon = BuildingIconData.FromTexture(TextureDB.FavCimsWorkingPlaceTexture);
                    break;
            }
            return workplace;
        }

        /// <summary>Validates an instance before reading its prefab or position.</summary>
        private static bool TryReadBuilding(ushort buildingId, BuildingManager manager, out Building building, out BuildingInfo info)
        {
            building = default;
            info = null;
            if (manager == null || buildingId == 0) return false;
            var buildings = manager.m_buildings.m_buffer;
            if (buildingId >= buildings.Length) return false;
            building = buildings[buildingId];
            if ((building.m_flags & Building.Flags.Created) == 0) return false;
            info = building.Info;
            return info != null && info.m_class != null;
        }

        /// <summary>Reads activity and vehicle controls independently of the building-cell refactor.</summary>
        private static void ReadActivity(InstanceID citizenInstance, uint citizenId, ref Citizen citizen, BuildingManager buildingManager, VehicleManager vehicleManager, ref CitizenRowData data)
        {
            data.ActivityText = string.Empty;
            data.ActivityEnabled = false;
            data.ActivityTooltip = string.Empty;
            data.TargetInstanceId = InstanceID.Empty;
            data.TargetName = string.Empty;

            SetNoVehicle(ref data);

            var citizenInfo = citizen.GetCitizenInfo(citizenId);

            if (citizenInfo == null || citizenInfo.m_citizenAI == null)
            { 
                return; 
            }

            data.Status = citizenInfo.m_citizenAI.GetLocalizedStatus(citizenId, ref citizen, out data.TargetInstanceId);

            if (!data.TargetInstanceId.IsEmpty && data.TargetInstanceId.Type == InstanceType.Building)
            { 
                data.TargetName = buildingManager.GetBuildingName(data.TargetInstanceId.Building, citizenInstance); 
            }

            if (data.IsArrested && data.IsCriminal)
            {
                data.ActivityText = FavCimsLang.Text("CitizenDetainedAtLabel") + " " + data.TargetName;
                data.ActivityTooltip = data.ActivityText;
                return;
            }

            data.ActivityText = data.Status + " " + data.TargetName;
            data.ActivityEnabled = !data.TargetInstanceId.IsEmpty;
            data.ActivityTooltip = data.ActivityText;

            if (data.Vehicle == 0)
            {
                SetWalkingVehicle(ref data);
                return;
            }

            var vehicles = vehicleManager.m_vehicles.m_buffer;
            if (data.Vehicle >= vehicles.Length)
            {
                return;
            }

            var vehicle = vehicles[data.Vehicle];
            var vehicleInfo = vehicle.Info;

            if ((vehicle.m_flags & Vehicle.Flags.Created) == 0 || vehicleInfo == null || vehicleInfo.m_class == null)
            {
                return;
            }

            data.VehicleInstanceId.Vehicle = data.Vehicle;

            string sprite = GetVehicleSprite(vehicleInfo.m_class.m_service, vehicleInfo.m_class.m_subService, vehicleManager.GetVehicleName(data.Vehicle), vehicleInfo.name);

            bool usesModAtlas = sprite == "IconScooterVehicle";
            bool usesTransportStates = sprite.StartsWith("SubBar", StringComparison.Ordinal);

            data.VehicleAtlas = usesModAtlas ? MyAtlas.FavCimsAtlas : UIView.GetAView().defaultAtlas;

            data.VehicleIcon = sprite;
            data.VehicleHoverIcon = usesTransportStates ? sprite + "Hovered" : sprite;
            data.VehicleDisabledIcon = "InfoIconPopulationDisabled";
            data.VehicleEnabled = true;
            data.VehicleTooltip = vehicleManager.GetVehicleName(data.Vehicle);
        }

        private static void SetWalkingVehicle(ref CitizenRowData data)
        {
            data.VehicleInstanceId = InstanceID.Empty;
            data.VehicleAtlas = UIView.GetAView().defaultAtlas;
            data.VehicleIcon = string.Empty;
            data.VehicleHoverIcon = string.Empty;
            data.VehicleDisabledIcon = "InfoIconPopulationDisabled";
            data.VehicleEnabled = false;
            data.VehicleTooltip = FavCimsLang.Text("CitizenWalkingTooltip");
        }

        private static string GetVehicleSprite(ItemClass.Service service, ItemClass.SubService subService, string vehicleName, string vehiclePrefabName)        
        {
            string names = vehicleName + " " + vehiclePrefabName;

            if (service == ItemClass.Service.Residential && names.IndexOf("Bicycle", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return "IconTouristBicycleVehicle";
            }

            if (service == ItemClass.Service.Residential && names.IndexOf("Scooter", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return "ScooterIcon";
            }

            if (service != ItemClass.Service.PublicTransport)
            {
                return "IconCitizenVehicle";
            }

            return subService switch
            {
                ItemClass.SubService.PublicTransportBus => "SubBarPublicTransportBus",
                ItemClass.SubService.PublicTransportMetro => "SubBarPublicTransportMetro",
                ItemClass.SubService.PublicTransportTrain => "SubBarPublicTransportTrain",
                ItemClass.SubService.PublicTransportShip => "SubBarPublicTransportShip",
                ItemClass.SubService.PublicTransportPlane => names.IndexOf("Blimp", StringComparison.OrdinalIgnoreCase) >= 0 ? "IconPolicyEducationalBlimps" : "SubBarPublicTransportPlane",
                ItemClass.SubService.PublicTransportTaxi => "SubBarPublicTransportTaxi",
                ItemClass.SubService.PublicTransportTram => "SubBarPublicTransportTram",
                ItemClass.SubService.PublicTransportMonorail => "SubBarPublicTransportMonorail",
                ItemClass.SubService.PublicTransportCableCar => "SubBarPublicTransportCableCar",
                ItemClass.SubService.PublicTransportTours => "SubBarPublicTransportTours",
                ItemClass.SubService.PublicTransportTrolleybus => "SubBarPublicTransportTrolleybus",
                _ => "IconCitizenVehicle",
            };
        }

        /// <summary>Preserves the existing death-state display behavior.</summary>
        private static void ReadDeathState(ref CitizenRowData data)
        {
            data.DeathRealAge = data.RealAge;
            data.DeathDate = GameTime.FavCimsDate(FavCimsLang.Text("DateDayFirstFormat"), "n/a");
            data.DeathTime = GameTime.FavCimsTime();
            data.ActivityText = string.Empty;
            data.ActivityEnabled = false;
            data.ActivityTooltip = string.Empty;
            SetNoVehicle(ref data);
        }

        /// <summary>Clears every vehicle presentation field and its navigation target together.</summary>
        private static void SetNoVehicle(ref CitizenRowData data)
        {
            data.VehicleInstanceId = InstanceID.Empty;
            data.VehicleAtlas = null;
            data.VehicleIcon = string.Empty;
            data.VehicleHoverIcon = string.Empty;
            data.VehicleDisabledIcon = string.Empty;
            data.VehicleEnabled = false;
            data.VehicleTooltip = string.Empty;
        }

        /// <summary>Formats the actual district ID recorded in the building cell.</summary>
        private static string GetDistrictTooltip(int districtId)
        {
            var districtManager = Singleton<DistrictManager>.instance;
            return FavCimsLang.Text("DistrictLabel") + (districtId == 0 ? FavCimsLang.Text("DistrictNoneText") : districtManager.GetDistrictName(districtId));
        }

        /// <summary>Safely selects an existing level-overlay asset.</summary>
        private static Texture GetLevelTexture(Texture[] textures, ItemClass.Level level)
        {
            if (textures == null || textures.Length == 0) return null;
            int index = (int)level;
            return index < 0 || index >= textures.Length ? null : textures[index];
        }
    }
}