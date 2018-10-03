namespace ATXK.AI
{
	using UnityEngine;

	/// <summary>
	/// Base class for all AI actions.
	/// </summary>
	public abstract class AI_Action : ScriptableObject
	{
		public abstract void Act(AI_Controller controller);
	}
}