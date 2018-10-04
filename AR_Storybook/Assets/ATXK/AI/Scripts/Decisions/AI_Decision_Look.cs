namespace ATXK.AI
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "AI/Decision/Look")]
	public class AI_Decision_Look : AI_Decision
	{
		[SerializeField] string tagToLookFor = "";

		public override bool Decide(AI_Controller controller)
		{
			return Look(controller);
		}

		private bool Look(AI_Controller controller)
		{
			RaycastHit hit;
			if(Physics.SphereCast(controller.transform.position, 1f, controller.transform.forward, out hit, controller.aiStats.scanRange))
			{
				if(hit.collider.CompareTag(tagToLookFor))
				{
					controller.target = hit.transform.gameObject;
					return true;
				}
			}

			return false;
		}
	}
}