namespace ATXK.AI
{
	using UnityEngine;
	using System.Collections.Generic;

	/// <summary>
	/// State for the AI that holds a list of actions and transitions.
	/// </summary>
	[CreateAssetMenu(menuName = "AI/State")]
	public class AI_State : ScriptableObject
	{
		[SerializeField] List<AI_Action> actions;
		[SerializeField] List<AI_Transition> transitions;

		public void UpdateState(AI_Controller controller)
		{
			DoActions(controller);
			CheckTransitions(controller);
		}

		void DoActions(AI_Controller controller)
		{
			for(int i = actions.Count - 1; i >= 0; i--)
			{
				actions[i].Act(controller);
			}
		}

		void CheckTransitions(AI_Controller controller)
		{
			for (int i = transitions.Count - 1; i >= 0; i--)
			{
				if(transitions[i].decision.Decide(controller))
				{
					controller.ChangeState(transitions[i].stateToTransitionTo);
				}
			}
		}
	}
}