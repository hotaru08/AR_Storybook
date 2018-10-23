namespace ATXK.AI
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "AI/State")]
	public class AI_State : ScriptableObject
	{
		[SerializeField] AI_Action[] actions;
		[SerializeField] AI_Transition[] transitions;

		public void UpdateState(AI_Controller controller)
		{
			DoActions(controller);
			DoTranstions(controller);
		}

		private void DoActions(AI_Controller controller)
		{
			for (int i = actions.Length - 1; i >= 0; i--)
				actions[i].Act(controller);
		}

		private void DoTranstions(AI_Controller controller)
		{
			for (int i = transitions.Length - 1; i >= 0; i--)
			{
				if (transitions[i].Decide(controller))
					controller.ChangeState(transitions[i].TrueState);
				else
					controller.ChangeState(transitions[i].FalseState);
			}
		}
	}
}