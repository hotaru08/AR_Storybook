namespace ATXK.AI
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "AI/Action", order = 2)]
	public abstract class AI_Action : ScriptableObject
	{
		public abstract void Act(AI_Controller controller);
	}
}
