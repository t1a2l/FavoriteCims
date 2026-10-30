using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.Math;

namespace FavoriteCims.Utils.Services
{
    public static class FamilyWorkplaceDataService
    {
        public static FamilyWorkplaceData Read(FamilyCitizenData citizen)
        {
            var result = new FamilyWorkplaceData();
            if (citizen == null) return result;

            result.BuildingId = citizen.WorkBuilding;
            result.HasAssignment = result.BuildingId != 0;
            result.IsStudent = citizen.IsStudent;
            result.IsRetired = !result.HasAssignment && !result.IsStudent && citizen.GameAge >= 180;
            if (!result.HasAssignment) return result;

            var buildings = Singleton<BuildingManager>.instance;
            var buffer = buildings.m_buildings.m_buffer;
            if (result.BuildingId >= buffer.Length) return result;
            var building = buffer[result.BuildingId];
            if ((building.m_flags & Building.Flags.Created) == 0) return result;
            var info = building.Info;
            if (info == null || info.m_class == null) return result;

            result.IsAvailable = true;
            result.InstanceId = new InstanceID { Building = result.BuildingId };
            result.Name = buildings.GetBuildingName(result.BuildingId, citizen.InstanceId) ?? string.Empty;
            result.Service = info.m_class.m_service;
            result.SubService = info.m_class.m_subService;
            result.Level = info.m_class.m_level;

            var districts = Singleton<DistrictManager>.instance;
            result.DistrictId = districts.GetDistrict(building.m_position);
            if (result.DistrictId != 0) result.DistrictName = districts.GetDistrictName(result.DistrictId) ?? string.Empty;

            if (result.IsStudent)
            {
                var citizens = Singleton<CitizenManager>.instance.m_citizens.m_buffer;
                if (citizen.CitizenId != 0 && citizen.CitizenId < (uint)citizens.Length) result.SchoolLevel = citizens[(int)citizen.CitizenId].GetCurrentSchoolLevel(citizen.CitizenId);
                return result;
            }

            var ai = info.m_buildingAI as CommonBuildingAI;
            if (ai != null)  result.Profession = ai.GetTitle(citizen.Gender, citizen.Education, result.BuildingId, citizen.CitizenId) ?? string.Empty;

            if (string.IsNullOrEmpty(result.Profession)) result.Profession = ReadFallbackProfession(citizen, result.BuildingId);
            return result;
        }

        private static string ReadFallbackProfession(FamilyCitizenData citizen, ushort buildingId)
        {
            string key;
            switch (citizen.Education)
            {
                case Citizen.Education.Uneducated:
                    key = "CITIZEN_OCCUPATION_PROFESSION_UNEDUCATED";
                    break;
                case Citizen.Education.OneSchool:
                    key = "CITIZEN_OCCUPATION_PROFESSION_EDUCATED";
                    break;
                case Citizen.Education.TwoSchools:
                    key = "CITIZEN_OCCUPATION_PROFESSION_WELLEDUCATED";
                    break;
                case Citizen.Education.ThreeSchools:
                    key = "CITIZEN_OCCUPATION_PROFESSION_HIGHLYEDUCATED";
                    break;
                default:
                    return string.Empty;
            }
            if (citizen.Gender == Citizen.Gender.Female) key += "_FEMALE";
            int number = new Randomizer(buildingId + citizen.CitizenId).Int32(1, 5);
            return Locale.Get(key, number.ToString()) + " " + Locale.Get("CITIZEN_OCCUPATION_LOCATIONPREPOSITION");
        }
    }
}