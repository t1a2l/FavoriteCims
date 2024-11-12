using System.Collections.Generic;

namespace FavoriteCims
{
	public class DriverPrivateVehiclePanelRow : PassengersVehiclePanelRow
	{
        public override bool Wait()
		{
			return FavCimsVechiclePanel.Wait;
		}

		public override Dictionary<uint, uint> GetCimsDict()
		{
			return FavCimsVechiclePanel.CimsOnVeh;
		}

	}
}

