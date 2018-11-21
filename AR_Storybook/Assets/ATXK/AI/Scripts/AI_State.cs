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

		public List<AI_Transition> Transitions { get { return transitions; } }
                
		/// <summary>
		/// Update call to this state.
		/// </summary>
		/// <param name="controller">Controller that is calling this update.</param>
		public void UpdateState(AI_Controller controller)
		{
			DoActions(controller);
			CheckTransitions(controller);
		}

		/// <summary>
		/// Runs through and invokes every action registered to this state.
		/// </summary>
		/// <param name="controller">Controller that will be affected.</param>
		private void DoActions(AI_Controller controller)
		{
			for(int i = actions.Count - 1; i >= 0; i--)
			{
				actions[i].Act(controller);
			}
		}

		/// <summary>
		/// Runs through all transitions registered and changes the calling AI's current state.
		/// </summary>
		/// <param name="controller">Controller that will be affected.</param>
		private void CheckTransitions(AI_Controller controller)
		{
			for (int i = transitions.Count - 1; i >= 0; i--)
			{
				if(transitions[i].Decide(controller))
                {
					controller.ChangeState(transitions[i].TrueState);
                    break;
                }
				else
					controller.ChangeState(transitions[i].FalseState);
			}
		}
	}
}