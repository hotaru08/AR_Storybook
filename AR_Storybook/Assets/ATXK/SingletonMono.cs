namespace ATXK
{
	using UnityEngine;

	public abstract class SingletonMono<T> : MonoBehaviour where T : Component
	{
		private static GameObject _instanceObject;
		private static T _instance;

		public static T Instance
		{
			get
			{
				//Try to find an existing instance if necessary
				if (!_instance)
				{
					_instance = FindObjectOfType(typeof(T)) as T;
				}

				//If the instance is still null, create a new instance of object
				if (!_instance)
				{
					GameObject go = new GameObject();
					_instance = go.AddComponent<T>();
				}

				return _instance;
			}
		}
	}
}
