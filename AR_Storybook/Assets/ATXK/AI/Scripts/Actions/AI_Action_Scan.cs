namespace ATXK.AI
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "AI/Action/Scan for Target")]
	public class AI_Action_Scan : AI_Action
	{
		public override void Act(AI_Controller controller)
		{
			Scan(controller);
		}

		private void Scan(AI_Controller controller)
		{
			controller.aiNavMeshAgent.isStopped = true;
			controller.transform.Rotate(0, controller.aiStats.scanSpeed * Time.deltaTime, 0);
		}
	}
}