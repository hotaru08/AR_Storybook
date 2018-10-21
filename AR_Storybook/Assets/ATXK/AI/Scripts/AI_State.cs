namespace ATXK.AI
{
	using UnityEngine;
	using System.Collections.Generic;
	using EventSystem;

	/// <summary>
	/// State for the AI that holds a list of actions and transitions.
	/// </summary>
	[CreateAssetMenu(menuName = "AI/State")]
	public class AI_State : ScriptableObject
	{
		[SerializeField] List<AI_Action> actions;
		[SerializeField] List<AI_Transition> transitions;

		#region Property Getters
		public List<AI_Transition> Transitions { get { return transitions; } }
		#endregion

		public void UpdateState(AI_Controller controller)
		{
			Debug.Log(name + " UpdateState().");
			DoActions(controller);
			CheckTransitions(controller);
		}

		private void DoActions(AI_Controller controller)
		{
			for(int i = actions.Count - 1; i >= 0; i--)
			{
				actions[i].Act(controller);
			}
		}

		private void CheckTransitions(AI_Controller controller)
		{
			for (int i = transitions.Count - 1; i >= 0; i--)
			{
				if(transitions[i].runtimeDecision.Decide(controller))
                {
					controller.ChangeState(transitions[i].decisionTrueState);
                    break;
                }
				else
					controller.ChangeState(transitions[i].decisionFalseState);
			}
		}

		public void EnterState()
		{
			Debug.Log(name + " EnterState().");
			foreach (AI_Transition transition in transitions)
			{
				transition.Initialise();
			}
		}

		public void ExitState()
		{
			Debug.Log(name + " ExitState().");
		}
	}
}