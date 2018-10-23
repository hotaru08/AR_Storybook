using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.AI;
using ATXK.CustomVariables;
using ATXK.EventSystem;
using ATXK.ItemSystem;

[CreateAssetMenu(menuName = "AI/Action/Fire Projectile")]
public class AI_Action_Spit : AI_Action
{
	[SerializeField] ES_Event_UnityObject fireProjectileEvent;
	[SerializeField] Item_Holder projectile;

	public override void Act(AI_Controller controller)
	{
		Spit(controller);
	}

	private void Spit(AI_Controller controller)
	{
		// Call an event that tells a Projectile spawner to Instantiate the projectile
		fireProjectileEvent.Invoke(projectile.gameObject, controller.gameObject.GetInstanceID());
	}
}
