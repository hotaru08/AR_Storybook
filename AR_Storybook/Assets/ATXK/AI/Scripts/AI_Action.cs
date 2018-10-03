namespace ATXK.AI
{
	using UnityEngine;

	public abstract class AI_Action : ScriptableObject
	{
		public abstract void Act(AI_Controller controller);
	}
}