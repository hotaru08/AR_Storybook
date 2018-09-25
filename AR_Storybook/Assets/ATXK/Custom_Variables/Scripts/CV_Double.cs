namespace ATXK.CustomVariables
{
	using UnityEngine;

	/// <summary>
	/// Double variable using ScriptableObject. Use this for scene-independant data storage or as a reference value for multiple classes.
	/// </summary>
	[CreateAssetMenu(menuName = "ATXK/Custom Variable/Double")]
	public class CV_Double : CV_Base
	{
		/// <summary>
		/// Value of this object that other classes cannot view/edit.
		/// </summary>
		[SerializeField] double initialValue;

		/// <summary>
		/// Value of this object that other classes can view/edit.
		/// </summary>
		public double RuntimeValue;

		/// <summary>
		/// Resets the runtime value to the initial value.
		/// </summary>
		public override void Reset()
		{
			RuntimeValue = initialValue;
		}

		/// <summary>
		/// ISerializationCallbackReceiver function.
		/// </summary>
		public override void OnBeforeSerialize()
		{

		}

		/// <summary>
		/// ISerializationCallbackReceiver function.
		/// </summary>
		public override void OnAfterDeserialize()
		{
			RuntimeValue = initialValue;
		}
	}
}