using FavoriteCims.UI.Panels;
using System.Collections.Generic;

namespace FavoriteCims.UI.PanelsRows
{
    public class DriverPrivateVehiclePanelRow : PassengersVehiclePanelRow
    {
        public override bool Wait()
        {
            return FavCimsVehiclePanel.Wait;
        }

        public override Dictionary<uint, uint> GetCimsDict()
        {
            return FavCimsVehiclePanel.CimsOnVeh;
        }

    }
}

