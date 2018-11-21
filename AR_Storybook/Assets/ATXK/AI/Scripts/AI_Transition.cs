namespace ATXK.AI
{
	using UnityEngine;

	/// <summary>
	/// Holds a decision module and the states corresponding to the decision.
	/// </summary>
	[System.Serializable]
	public class AI_Transition
	{
		[SerializeField] AI_Decision decision;
		[SerializeField] AI_State trueState;
		[SerializeField] AI_State falseState;

		public AI_State TrueState { get { return trueState; } }
		public AI_State FalseState { get { return falseState; } }

		/// <summary>
		/// Invokes the referenced decision.
		/// </summary>
		/// <param name="controller">Controller that is calling this function.</param>
		public bool Decide(AI_Controller controller)
		{
			return decision.Decide(controller);
		}
	}
}