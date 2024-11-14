using FavoriteCims.UI.Panels;
using System.Collections.Generic;

namespace FavoriteCims.UI.PanelsRows
{
	public class WorkersServiceBuildingPanelRow : ResidentialBuildingPanelRow
	{
		public override bool Wait()
		{
			return PeopleInsideBuildingsPanel.Wait;
		}

		public override Dictionary<uint, uint> GetCimsDict()
		{
			return PeopleInsideBuildingsPanel.CimsOnBuilding;
		}

		public override void DecreaseWorkersCount()
		{
            PeopleInsideBuildingsPanel.WorkersCount--;
		}

		public override void DecreaseGuestsCount()
		{
            PeopleInsideBuildingsPanel.GuestsCount--;
		}
	}
}
