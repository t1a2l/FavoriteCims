using System.Collections.Generic;

namespace FavoriteCims
{
	public class DriverPrivateVehiclePanelRow : PassengersVehiclePanelRow
	{
        private const float Run = 0.1f;

        public override bool Wait()
		{
			return FavCimsVechiclePanel.Wait;
		}

		public override Dictionary<uint, uint> GetCimsDict()
		{
			return FavCimsVechiclePanel.CimsOnVeh;
		}

		public DriverPrivateVehiclePanelRow()
		{
		}

	}
}

