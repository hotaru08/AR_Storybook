namespace ATXK.AI
{
	using UnityEngine;

	/// <summary>
	/// Base class for all AI decision modules.
	/// </summary>
	[CreateAssetMenu(menuName = "AI/Decision", order = 3)]
	public abstract class AI_Decision : ScriptableObject
	{
		/// <summary>
		/// Implements the decision that the affect the current state of an AI.
		/// </summary>
		/// <param name="controller">Return value will be determined by attributes of this controller.</param>
		public abstract bool Decide(AI_Controller controller);
	}
}