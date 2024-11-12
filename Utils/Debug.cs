namespace FavoriteCims
{
	public static class Debug
	{
        private const string prefix = "FavoriteCimsMod: ";

        public static void Log(string message)
		{
			UnityEngine.Debug.Log(prefix + message);
		}

		public static void Error(string message)
		{
            UnityEngine.Debug.LogError(prefix + message);
		}

		public static void Warning(string message)
		{
            UnityEngine.Debug.LogWarning(prefix + message);
		}

	}
}
