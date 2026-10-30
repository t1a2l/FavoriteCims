namespace FavoriteCims.UI.PanelsRows
{
    public class PassengersVehiclePanelRow : CompactCitizenPanelRow
    {
        public ushort OnVehicle;

        protected override bool IsRowStillValid()
        {
            return citizen != 0 && OnVehicle != 0 && GetCimsDict().TryGetValue(citizen, out uint vehicleId) && vehicleId == OnVehicle;
        }

        protected override void ReleaseRow()
        {
            // The panel owns roster reconciliation; a row only releases its binding.
            Hide();
            citizen = 0;
            OnVehicle = 0;
            firstRun = true;
        }

        protected override void UpdatePresenceArrow()
        {
            var vehicles = MyVehicle.m_vehicles.m_buffer;
            if (OnVehicle == 0 || OnVehicle >= vehicles.Length)
            {
                Gender.normalFgSprite = string.Empty;
                return;
            }
            var info = vehicles[OnVehicle].Info;
            bool publicTransport = info != null && info.m_class != null && info.m_class.m_service == ItemClass.Service.PublicTransport;
            Gender.normalFgSprite = publicTransport ? "GreenArrowIcon" : string.Empty;
        }
    }
}