using ICities;

namespace FavoriteCims
{
	public class FavoriteCimsModMain : IUserMod
	{
        public const string Version = "v2.0";

        public string Name
		{
			get
			{
				return "Favorite Cims v2.0";
			}
		}

		public string Description
		{
			get
			{
				return "Allows you to add and show favorite citizens in a list.";
			}
		}		
	}
}
