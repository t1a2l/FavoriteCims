using FavoriteCims.Utils.Services;

namespace FavoriteCims.UI.PanelsRows
{
    public class ResidentialBuildingPanelRow : CompactCitizenPanelRow
    {
        public ushort OnBuilding;
        public Citizen.Location LocType;

        // Retained for compatibility; panel snapshots own all counts.
        public virtual void DecreaseWorkersCount() { }
        public virtual void DecreaseGuestsCount() { }

        protected override bool IsRowStillValid()
        {
            uint buildingId;
            return citizen != 0 && OnBuilding != 0 && GetCimsDict().TryGetValue(citizen, out buildingId) && buildingId == OnBuilding;
        }

        protected override void ReleaseRow()
        {
            // Away occupants remain roster members until the panel reconciles them.
            Hide();
            citizen = 0;
            OnBuilding = 0;
            LocType = Citizen.Location.Home;
            firstRun = true;
        }

        protected override void UpdatePresenceArrow()
        {
            Gender.normalFgSprite = BuildingPresenceService.TryRead(citizen, OnBuilding, out BuildingPresenceState presence) ? BuildingPresenceService.GetArrowSprite(presence) : string.Empty;
        }
    }
}