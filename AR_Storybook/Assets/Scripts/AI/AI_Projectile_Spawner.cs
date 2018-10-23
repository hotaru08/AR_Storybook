using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.EventSystem;
using ATXK.ItemSystem;

public class AI_Projectile_Spawner : MonoBehaviour
{
	public void Fire(Item_Holder projectilePrefab)
	{
		Item_Projectile projectile = projectilePrefab.Item as Item_Projectile;
		if (projectile != null)
		{
			GameObject projectileObject = Instantiate(projectilePrefab).gameObject;
			projectileObject.transform.position = transform.position;
			projectileObject.transform.forward = transform.forward;

			Destroy(projectileObject, projectile.Duration);
		}
	}

	public void Fire(Object projectileObject)
	{
		GameObject projectile = projectileObject as GameObject;
		if(projectile != null && projectile.GetComponent<Item_Holder>())
		{
			Fire(projectile.GetComponent<Item_Holder>());
		}
	}
}
