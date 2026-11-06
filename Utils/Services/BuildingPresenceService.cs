using ColossalFramework;

namespace FavoriteCims.Utils.Services
{
    public static class BuildingPresenceService
    {
        /// <summary>
        /// Determines whether a citizen is present, heading to the selected
        /// building, or elsewhere. Returns false when presence cannot be resolved.
        /// </summary>
        public static bool TryRead(uint citizenId, ushort buildingId, out BuildingPresenceState presence)
        {
            presence = BuildingPresenceState.Unknown;

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
                else
                {
                    presence = BuildingPresenceState.Away;
                }
                return true;
            }

            ushort instanceId = citizen.m_instance;
            var instances = citizenManager.m_instances.m_buffer;

            if (instanceId == 0 || instanceId >= instances.Length)
            {
                // Moving, but there is no usable instance to establish
                // the destination. Do not classify this as definitely away.
                return false;
            }

            var instance = instances[instanceId];

            if (instance.m_citizen != citizenId)
            {
                return false;
            }

            if (instance.m_targetBuilding == buildingId)
            {
                presence = BuildingPresenceState.HeadingHere;
            }
            else
            {
                presence = BuildingPresenceState.Away;
            }

            return true;
        }

        /// <summary>
        /// Shows an arrival arrow when heading here.
        /// Persistent rosters can also request a red arrow when away.
        /// </summary>
        public static string GetArrowSprite(BuildingPresenceState presence, bool showAwayArrow = false)
        {
            return presence switch
            {
                BuildingPresenceState.HeadingHere => "GreenArrowIcon",
                BuildingPresenceState.Away when showAwayArrow => "RedArrowIcon",
                _ => string.Empty
            };
        }
    }
}
