namespace ATXK.Helper
{
	using UnityEngine;

	public static class DebugLogger
	{
		/// <summary>
		/// Writes standardized debug logs to Unity console.
		/// </summary>
		/// <typeparam name="T">Type of the class.</typeparam>
		/// <param name="debugLog">Message to print to Unity console.</param>
		public static void Log<T>(string debugLog)
		{
			Debug.Log(typeof(T).ToString() + ": " + debugLog);
		}

		/// <summary>
		/// Writes standardized warning logs to Unity console.
		/// </summary>
		/// <typeparam name="T">Type of the class.</typeparam>
		/// <param name="debugLog">Message to print to Unity console.</param>
		public static void LogWarning<T>(string debugLog)
		{
			Debug.LogWarning(typeof(T).ToString() + ": " + debugLog);
		}

		/// <summary>
		/// Writes standardized error logs to Unity console.
		/// </summary>
		/// <typeparam name="T">Type of the class.</typeparam>
		/// <param name="debugLog">Message to print to Unity console.</param>
		public static void LogError<T>(string debugLog)
		{
			Debug.LogError(typeof(T).ToString() + ": " + debugLog);
		}

		/// <summary>
		/// Writes standardized assertion logs to Unity console.
		/// </summary>
		/// <typeparam name="T">Type of the class.</typeparam>
		/// <param name="debugLog">Message to print to Unity console.</param>
		public static void LogAssert<T>(string debugLog)
		{
			Debug.LogAssertion(typeof(T).ToString() + ": " + debugLog);
		}
	}
}