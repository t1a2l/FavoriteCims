using FavoriteCims.UI.Panels;
using System.Collections.Generic;

namespace FavoriteCims.UI.PanelsRows
{
	public class WorkersServiceBuildingPanelRow : ResidentialBuildingPanelRow
	{
		public override bool Wait()
		{
			return PeopleInsideServiceBuildingsPanel.Wait;
		}

		public override Dictionary<uint, uint> GetCimsDict()
		{
			return PeopleInsideServiceBuildingsPanel.CimsOnBuilding;
		}

		public override void DecreaseWorkersCount()
		{
			PeopleInsideServiceBuildingsPanel.WorkersCount--;
		}

		public override void DecreaseGuestsCount()
		{
			PeopleInsideServiceBuildingsPanel.GuestsCount--;
		}
	}
}
