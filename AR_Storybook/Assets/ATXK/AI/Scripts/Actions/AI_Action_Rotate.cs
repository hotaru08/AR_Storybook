namespace ATXK.AI
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "AI/Action/Rotate AI")]
	public class AI_Action_Rotate : AI_Action
	{
		[SerializeField] float rotateSpeed;

		public override void Act(AI_Controller controller)
		{
			Rotate(controller);
		}

		private void Rotate(AI_Controller controller)
		{
			controller.transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
		}
	}
}