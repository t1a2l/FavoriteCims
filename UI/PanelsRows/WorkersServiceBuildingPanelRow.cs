using System.Collections.Generic;

namespace FavoriteCims
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

		public WorkersServiceBuildingPanelRow()
		{
		}
	}
}
