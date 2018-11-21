namespace ATXK.CustomVariables
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	/// <summary>
	/// Base class for all data-containing custom variables.
	/// </summary>
	/// <typeparam name="T">Type of data that will be represented.</typeparam>
	public abstract class CV_Base<T> : ScriptableObject
	{
		public T value;
	}
}