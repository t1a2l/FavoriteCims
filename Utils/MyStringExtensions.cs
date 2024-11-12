using System.Text.RegularExpressions;

namespace FavoriteCims.Utils
{
	public static class MyStringExtensions
	{
		public static bool Like(this string toSearch, string toFind)
		{
			return new Regex("\\A" + new Regex("\\.|\\$|\\^|\\{|\\[|\\(|\\||\\)|\\*|\\+|\\?|\\\\").Replace(toFind, (Match ch) => "\\" + ((ch != null) ? ch.ToString() : null)).Replace('_', '.').Replace("%", ".*") + "\\z", RegexOptions.Singleline).IsMatch(toSearch);
		}
	}
}
