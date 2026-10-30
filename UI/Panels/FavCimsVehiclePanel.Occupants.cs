using FavoriteCims.Utils;
using FavoriteCims.Utils.Services;

namespace FavoriteCims.UI.Panels
{
    public partial class FavCimsVehiclePanel
    {
        private void RefreshOccupants()
        {
            if (!VehicleOccupantsService.TryRead(VehicleID, out VehicleOccupantsSnapshot snapshot))
            {
                ClosePanel();
                return;
            }

            // Called inside RunWithRowsPaused; preserve the shared dictionary object.
            CimsOnVeh.Clear();
            uint driverId = snapshot.FirstUnitSlotZeroCitizen;
            bool hasDriver = driverId != 0;
            if (hasDriver) AssignDriver(driverId);
            else ResetDriverRow();

            int passengerSlot = 1;
            foreach (var occupant in snapshot.Occupants)
            {
                if (occupant.CitizenId == driverId) continue;
                if (passengerSlot >= PassengerSlotCount) break;
                AssignPassenger(passengerSlot, occupant.CitizenId);
                passengerSlot++;
            }
            for (int i = passengerSlot; i < PassengerSlotCount; i++)
            {
                var row = passengerRows[i];
                if (row != null && (row.citizen != 0 || row.isVisible)) ResetPassengerRow(row);
            }
            driverPanel.isVisible = hasDriver;
            passengersPanel.isVisible = passengerSlot > 1;
            driverPanelText.text = FavCimsLang.Text("VehicleDriverIconText");
            passengersPanelText.text = FavCimsLang.Text(passengerSlot > 1 ? "VehiclePassengerIconText" : "ViewNoPassengers");
        }
    }
}