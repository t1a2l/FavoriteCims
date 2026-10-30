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
				return "Keep track of your favorite citizens, explore their families, and see who lives, works, or visits your city's buildings.";
            }
		}		
	}
}
