namespace ATXK.AI
{
	using UnityEngine;
	using System.Collections.Generic;

	public class AI_State : ScriptableObject
	{
		List<AI_Action> actions;
		List<AI_Transition> transitions;

		public void UpdateState(AI_Controller controller)
		{
			DoActions(controller);
			CheckTransitions(controller);
		}

		void DoActions(AI_Controller controller)
		{
			for(int i = actions.Count - 1; i >= 0; i--)
			{

			}
		}

		void CheckTransitions(AI_Controller controller)
		{
			for (int i = transitions.Count - 1; i >= 0; i--)
			{
				if(transitions[i].decision.Decide(controller))
				{

				}
			}
		}
	}
}