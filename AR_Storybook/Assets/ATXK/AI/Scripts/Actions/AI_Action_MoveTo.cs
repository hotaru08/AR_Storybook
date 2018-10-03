namespace ATXK.AI
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "AI/Action/Move To Target")]
	public class AI_Action_MoveTo : AI_Action
	{
		public override void Act(AI_Controller controller)
		{
			Move(controller);
		}

		private void Move(AI_Controller controller)
		{
			if(controller.target != null)
			{
				controller.aiNavMeshAgent.SetDestination(controller.GetTargetPosition());
				controller.aiNavMeshAgent.isStopped = false;
			}
		}
	}
}