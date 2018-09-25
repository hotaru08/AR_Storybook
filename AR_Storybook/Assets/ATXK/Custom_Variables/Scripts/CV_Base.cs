namespace ATXK.CustomVariables
{
	using UnityEngine;

	/// <summary>
	/// Base class for all custom variables.
	/// </summary>
	public abstract class CV_Base : ScriptableObject, ISerializationCallbackReceiver
	{
		/// <summary>
		/// Resets the runtime value to the initial value.
		/// </summary>
		public abstract void Reset();

		/// <summary>
		/// ISerializationCallbackReceiver OnAfterDeserialize function.
		/// </summary>
		public abstract void OnAfterDeserialize();

		/// <summary>
		/// ISerializationCallbackReceiver OnBeforeSerialize function.
		/// </summary>
		public abstract void OnBeforeSerialize();
	}
}