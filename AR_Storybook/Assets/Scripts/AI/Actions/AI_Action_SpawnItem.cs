using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.AI;
using ATXK.ItemSystem;
using ATXK.EventSystem;

[CreateAssetMenu(menuName = "AI/Action/Spawn Item")]
public class AI_Action_SpawnItem : AI_Action
{
	[System.Serializable]
	class ProjectileAndChance
	{
		public Item_Holder item;
		[Range(0f, 1f)]public float chance;
	}

	[SerializeField] bool fireOnAll = false;
	[SerializeField] ES_Event_UnityObject fireProjectileEvent;
	[SerializeField] List<ProjectileAndChance> itemList = new List<ProjectileAndChance>();

	public override void Act(AI_Controller controller)
	{
		Fire(controller);
	}

	private void Fire(AI_Controller controller)
	{
		float total = 0f;

		foreach(ProjectileAndChance elem in itemList)
		{
			total += elem.chance;
		}

		float randomPoint = Random.value * total;

		for(int i = 0; i < itemList.Count; i++)
		{
			if (randomPoint <= itemList[i].chance)
			{
				int? laneID = null;
				if (!fireOnAll)
					laneID = controller.gameObject.GetInstanceID();

				fireProjectileEvent.Invoke(itemList[i].item.gameObject, laneID);
				return;
			}
			else
			{
				randomPoint -= itemList[i].chance;
			}
		}
	}
}
