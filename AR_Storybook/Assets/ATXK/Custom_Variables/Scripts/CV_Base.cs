namespace ATXK.CustomVariables
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public abstract class CV_Base<T> : ScriptableObject
	{
		public T value;
	}
}