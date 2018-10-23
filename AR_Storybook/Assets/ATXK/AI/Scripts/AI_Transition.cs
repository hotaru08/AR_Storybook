namespace ATXK.AI
{
	using UnityEngine;

	[System.Serializable]
	public class AI_Transition
	{
		[SerializeField] AI_Decision decision;
		[SerializeField] AI_State trueState;
		[SerializeField] AI_State falseState;

		public AI_State TrueState { get { return trueState; } }
		public AI_State FalseState { get { return falseState; } }

		public bool Decide(AI_Controller controller)
		{
			return decision.Decide(controller);
		}
	}
}