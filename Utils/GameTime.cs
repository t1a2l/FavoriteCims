using ColossalFramework;

namespace FavoriteCims
{
	public static class GameTime
	{
		public static string FavCimsDate(string format, string oldformat)
		{
			bool exists = Singleton<SimulationManager>.exists;
			string text4;
			if (exists)
			{
				string text = Singleton<SimulationManager>.instance.m_currentGameTime.Date.Day.ToString();
				string text2 = Singleton<SimulationManager>.instance.m_currentGameTime.Date.Month.ToString();
				string text3 = Singleton<SimulationManager>.instance.m_currentGameTime.Date.Year.ToString();
				bool flag = oldformat != "n/a";
				if (flag)
				{
					string[] array = oldformat.Split(['/']);
					bool flag2 = array[0] != null && array[1] != null && array[2] != null;
					if (flag2)
					{
						text4 = string.Concat(
                        [
                            array[1],
							"/",
							array[0],
							"/",
							array[2]
						]);
					}
					else
					{
						text4 = oldformat;
					}
				}
				else
				{
					bool flag3 = format == "dd-mm-yyyy";
					if (flag3)
					{
						text4 = string.Concat([text, "/", text2, "/", text3]);
					}
					else
					{
						text4 = string.Concat([text2, "/", text, "/", text3]);
					}
				}
			}
			else
			{
				text4 = format;
			}
			return text4;
		}

		public static string FavCimsTime()
		{
			string text = Singleton<SimulationManager>.instance.m_currentGameTime.Hour.ToString();
			string text2 = Singleton<SimulationManager>.instance.m_currentGameTime.Minute.ToString();
			bool flag = text.Length == 1;
			if (flag)
			{
				text = "0" + text;
			}
			bool flag2 = text2.Length == 1;
			if (flag2)
			{
				text2 = "0" + text2;
			}
			return text + ":" + text2;
		}
	}
}
