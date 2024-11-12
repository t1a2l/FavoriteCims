using ICities;

namespace FavoriteCims
{
	public class FavoriteCimsModMain : IUserMod
	{
        public const string Version = "v0.5";

        public string Name
		{
			get
			{
				return "Favorite Cims v0.5";
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
