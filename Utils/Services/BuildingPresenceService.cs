using ColossalFramework;

namespace FavoriteCims.Utils.Services
{
    public static class BuildingPresenceService
    {
        public static bool TryRead(uint citizenId, ushort buildingId, out BuildingPresenceState presence)
        {
            presence = BuildingPresenceState.Away;

            if (citizenId == 0 || buildingId == 0)
            {
                return false;
            }

            var citizenManager = Singleton<CitizenManager>.instance;
            var buildingManager = Singleton<BuildingManager>.instance;

            var citizens = citizenManager.m_citizens.m_buffer;
            var buildings = buildingManager.m_buildings.m_buffer;

            if (citizenId >= (uint)citizens.Length || buildingId >= buildings.Length)
            {
                return false;
            }

            if ((buildings[buildingId].m_flags & Building.Flags.Created) == 0)
            {
                return false;
            }

            ref Citizen citizen = ref citizens[(int)citizenId];

            if ((citizen.m_flags & Citizen.Flags.Created) == 0)
            {
                return false;
            }

            if (citizen.CurrentLocation != Citizen.Location.Moving)
            {
                if (citizen.GetBuildingByLocation() == buildingId)
                {
                    presence = BuildingPresenceState.Present;
                }

                return true;
            }

            ushort instanceId = citizen.m_instance;
            var instances = citizenManager.m_instances.m_buffer;

            if (instanceId == 0 || instanceId >= instances.Length)
            {
                // The citizen is valid, but an incoming trip cannot
                // currently be established.
                return true;
            }

            var instance = instances[instanceId];

            if (instance.m_citizen != citizenId)
            {
                return true;
            }

            if (instance.m_targetBuilding == buildingId)
            {
                presence = BuildingPresenceState.HeadingHere;
            }

            return true;
        }

        public static string GetArrowSprite(BuildingPresenceState presence)
        {
            return presence == BuildingPresenceState.Present ? "GreenArrowIcon" : "RedArrowIcon";
        }
    }
}
