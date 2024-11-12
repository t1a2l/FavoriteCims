using ColossalFramework;

namespace FavoriteCims.Utils
{
	public static class GameTime
	{
		public static string FavCimsDate(string format, string oldformat)
		{
			if (Singleton<SimulationManager>.exists)
			{
				string d = Singleton<SimulationManager>.instance.m_currentGameTime.Date.Day.ToString();
				string m = Singleton<SimulationManager>.instance.m_currentGameTime.Date.Month.ToString();
				string y = Singleton<SimulationManager>.instance.m_currentGameTime.Date.Year.ToString();
				if (oldformat != "n/a")
				{
					string[] elements = oldformat.Split(['/']);
					if (elements[0] != null && elements[1] != null && elements[2] != null)
					{
						return string.Concat(
                        [
                            elements[1],
							"/",
                            elements[0],
							"/",
                            elements[2]
						]);
					}
                    return oldformat;
                }
				else
				{
					if (format == "dd-mm-yyyy")
					{
                        return string.Concat([d, "/", m, "/", y]);
					}
					else
					{
                        return string.Concat([m, "/", d, "/", y]);
					}
				}
			}
            return format;
        }

		public static string FavCimsTime()
		{
			string h = Singleton<SimulationManager>.instance.m_currentGameTime.Hour.ToString();
			string m = Singleton<SimulationManager>.instance.m_currentGameTime.Minute.ToString();
			if (h.Length == 1)
			{
				h = "0" + h;
			}
			if (m.Length == 1)
			{
				m = "0" + m;
			}
			return h + ":" + m;
		}
	}
}
