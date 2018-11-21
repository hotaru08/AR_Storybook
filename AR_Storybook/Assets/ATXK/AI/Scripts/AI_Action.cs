namespace ATXK.AI
{
	using UnityEngine;

	/// <summary>
	/// Base class for all AI action modules.
	/// </summary>
	[CreateAssetMenu(menuName = "AI/Action", order = 2)]
	public abstract class AI_Action : ScriptableObject
	{
		/// <summary>
		/// Implements the action that the AI will make.
		/// </summary>
		/// <param name="controller">Controller that will be affected by this action.</param>
		public abstract void Act(AI_Controller controller);
	}
}
