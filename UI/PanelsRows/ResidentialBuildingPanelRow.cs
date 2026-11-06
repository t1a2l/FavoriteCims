using FavoriteCims.Utils.Services;

namespace FavoriteCims.UI.PanelsRows
{
    public class ResidentialBuildingPanelRow : CompactCitizenPanelRow
    {
        public ushort OnBuilding;
        public Citizen.Location LocType;

        public virtual void DecreaseWorkersCount() { }

        public virtual void DecreaseGuestsCount() { }

        /// <summary>
        /// Residents and workers and students and hotel guests remain listed while away.
        /// Other row types can disable the away indicator.
        /// </summary>
        protected virtual bool ShowAwayArrow => true;

        protected override bool IsRowStillValid()
        {
            return citizen != 0 && OnBuilding != 0 && GetCimsDict().TryGetValue(citizen, out uint buildingId) && buildingId == OnBuilding;
        }

        protected override void ReleaseRow()
        {
            /// Away occupants remain roster members until the panel reconciles them.
            Hide();
            citizen = 0;
            OnBuilding = 0;
            LocType = Citizen.Location.Home;
            firstRun = true;
        }

        protected override void UpdatePresenceArrow()
        {
            if (!BuildingPresenceService.TryRead(citizen, OnBuilding, out BuildingPresenceState presence))
            {
                Gender.normalFgSprite = string.Empty;
                return;
            }
            
            Gender.normalFgSprite = BuildingPresenceService.GetArrowSprite(presence, showAwayArrow: ShowAwayArrow);
        }
    }
}