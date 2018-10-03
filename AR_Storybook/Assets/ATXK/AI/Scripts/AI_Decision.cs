namespace ATXK.AI
{
	using UnityEngine;

	public abstract class AI_Decision : ScriptableObject
	{
		public abstract bool Decide(AI_Controller controller);
	}
}