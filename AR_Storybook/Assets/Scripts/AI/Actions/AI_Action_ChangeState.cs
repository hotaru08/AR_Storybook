using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.AI;
using ATXK.EventSystem;

[CreateAssetMenu(menuName = "AI/Action/Change State")]
public class AI_Action_ChangeState : AI_Action
{
	[SerializeField] ES_Event_UnityObject eventToInvoke;
	[SerializeField] AI_State changeState;

	public override void Act(AI_Controller controller)
	{
		if (eventToInvoke != null)
			eventToInvoke.Invoke(changeState);
	}
}
