using FavoriteCims.Utils;
using FavoriteCims.Utils.Services;

namespace FavoriteCims.UI.Panels
{
    public partial class FavCimsVehiclePanelPT
    {
        private void RefreshOccupants()
        {
            if (!VehicleOccupantsService.TryRead(VehicleID, out VehicleOccupantsSnapshot snapshot))
            {
                ClosePanel();
                return;
            }

            // Called inside RunWithRowsPaused; rows keep the same dictionary reference.
            CimsOnPTVeh.Clear();
            int usedRows = 0;
            foreach (var occupant in snapshot.Occupants)
            {
                if (usedRows >= passengerRows.Length) break;
                AssignPassenger(usedRows, occupant.CitizenId);
                usedRows++;
            }
            for (int i = usedRows; i < passengerRows.Length; i++)
            {
                var row = passengerRows[i];
                if (row != null && (row.citizen != 0 || row.isVisible)) ResetPassengerRow(row);
            }
            passengersPanel.isVisible = usedRows > 0;
            passengersPanelText.text = FavCimsLang.Text(usedRows > 0 ? "VehiclePassengersTitle" : "VehicleNoPassengersStatus");
        }
    }
}