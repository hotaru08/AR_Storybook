using UnityEngine;
using ATXK.ItemSystem;

/// <summary>
/// Spawns projectile items.
/// </summary>
public class AI_Projectile_Spawner : MonoBehaviour
{
	/// <summary>
	/// Spawns an instance of the given projectile.
	/// </summary>
	/// <param name="projectilePrefab">Projectile to create a copy of.</param>
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

	/// <summary>
	/// Spawns an instance of the given projectile, if the Object given is type of GameObject and has the Item_Holder component.
	/// </summary>
	/// <param name="projectilePrefab">Projectile to create a copy of.</param>
	public void Fire(Object projectileObject)
	{
		GameObject projectile = projectileObject as GameObject;
		if(projectile != null && projectile.GetComponent<Item_Holder>())
		{
			Fire(projectile.GetComponent<Item_Holder>());
		}
	}
}
