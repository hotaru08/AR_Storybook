namespace ATXK.AI
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "AI/Decision", order = 3)]
	public abstract class AI_Decision : ScriptableObject
	{
		public abstract bool Decide(AI_Controller controller);
	}
}