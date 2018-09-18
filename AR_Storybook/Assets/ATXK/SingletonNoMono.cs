namespace ATXK
{
	public abstract class SingletonNoMono<T> where T : SingletonNoMono<T>, new()
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