namespace ATXK.Helper
{
	/// <summary>
	/// Singleton bass class for classes that do not require to be in the Unity Hierarchy.
	/// </summary>
	/// <typeparam name="T">The inheriting class.</typeparam>
	public abstract class SingletonBehaviourless<T> where T : SingletonBehaviourless<T>, new()
	{
		private static T _instance = new T();

		public static T Instance
		{
			get
			{
				return _instance;
			}
		}
	}
}