namespace ATXK.AI
{
	using UnityEngine;

	/// <summary>
	/// Holds data regarding the state's transition.
	/// </summary>
	[System.Serializable]
	public class AI_Transition
	{
		[SerializeField] AI_Decision decision;
		public AI_State decisionTrueState;
		public AI_State decisionFalseState;

		[HideInInspector] public AI_Decision runtimeDecision;

		public void Initialise()
		{
			Debug.Log("AI_Transition Initialise().");

			Object.DestroyImmediate(runtimeDecision);

			runtimeDecision = Object.Instantiate(decision);
			runtimeDecision.Reset();
		}
	}
}