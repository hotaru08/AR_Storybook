namespace ATXK.AI
{
	using UnityEngine;
	using EventSystem;

	[CreateAssetMenu(menuName = "AI/Action/Debug Ping")]
	public class AI_Action_Ping : AI_Action
	{
		[SerializeField] string debugString;
		[SerializeField] ES_Event_String debugEvent;

		public override void Act(AI_Controller controller)
		{
			string finalString = debugString + " from " + controller.gameObject.name;
			debugEvent.Invoke(finalString, controller.gameObject.GetInstanceID());
		}
	}
}