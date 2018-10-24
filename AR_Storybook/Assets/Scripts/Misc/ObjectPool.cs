using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds a pool of objects that can be reused.
/// </summary>
public static class ObjectPool
{
	private static class Pool<T>
	{
		/// <summary>
		/// Pool of objects with type T.
		/// </summary>
		private static readonly Stack<T> pool = new Stack<T>();

		/// <summary>
		/// Adds object of type T to pool.
		/// </summary>
		/// <param name="obj">Object to add.</param>
		public static void Push(T obj)
		{
			lock(pool)
			{
				pool.Push(obj);
			}
		}

		/// <summary>
		/// Attempts to remove object from pool.
		/// </summary>
		/// <param name="obj">Referenced object.</param>
		/// <returns>True if pop is succesful.</returns>
		public static bool TryPop(out T obj)
		{
			lock(pool)
			{
				if(pool.Count > 0)
				{
					//Set reference to popped object
					obj = pool.Pop();
					return true;
				}
			}
			//Pool is empty, therefore object will be of default value (ie NULL for class object, false for boolean, etc)
			obj = default(T);
			return false;
		}

		/// <summary>
		/// Attempt to get the size of the pool.
		/// </summary>
		/// <returns>The size of the pool, or -1 if pool does not exist.</returns>
		public static int TryGetSize()
		{
			lock (pool)
			{
				if(pool.Count > 0)
				{
					return pool.Count;
				}
			}
			return -1;
		}
	}

	/// <summary>
	/// Adds object to pool of the same type. Will create a new pool automatically if one does not exist.
	/// </summary>
	/// <typeparam name="T">Type of object to add.</typeparam>
	/// <param name="obj">Object to add to pool.</param>
	public static void Add<T>(T obj)
	{
		Pool<T>.Push(obj);
	}

	/// <summary>
	/// Attempts to get object from pool.
	/// </summary>
	/// <typeparam name="T">Type of object.</typeparam>
	/// <param name="obj">Object reference from pool.</param>
	/// <returns>True if pop is successful, false otherwise.</returns>
	private static bool TryGet<T>(out T obj)
	{
		return Pool<T>.TryPop(out obj);
	}

	/// <summary>
	/// Attempts to get object from pool. Failure will return a new instance of type T.
	/// </summary>
	/// <typeparam name="T">Type of object.</typeparam>
	/// <returns>Object from pool OR new instance of object.</returns>
	public static T GetExistingOrNew<T>() where T : new()
	{
		T returnObject;
		return TryGet(out returnObject) ? returnObject : new T();
	}

	/// <summary>
	/// Attempts to get object from pool. Failure will return a default instance of type T.
	/// </summary>
	/// <typeparam name="T">Type of object.</typeparam>
	/// <returns>Object from pool OR default instance of object.</returns>
	public static T GetExisting<T>()
	{
		T returnObject;
		return TryGet(out returnObject) ? returnObject : default(T);
	}

	/// <summary>
	/// Attempts to get the size of the pool with the same type as specified. Failure will return -1 as the size.
	/// </summary>
	/// <typeparam name="T">Type of object.</typeparam>
	/// <returns>The size of the pool. -1 if pool does not exist.</returns>
	public static int GetPoolSize<T>()
	{
		return Pool<T>.TryGetSize();
	}
}
