namespace ATXK.ItemSystem
{
	using UnityEngine;
	using EventSystem;

	public class Item_Projectile : Item_Base, IUpdateable
	{
		[SerializeField] float projectileSpeed;
		[SerializeField] float projectileDuration;
		[SerializeField] int projectileDamage;

		float elapsedTime;

		#region Property Getters
		public float Speed { get { return projectileSpeed; } }
		public float Duration { get { return projectileDuration; } }
		public int Damage { get { return projectileDamage; } }
		#endregion

		public override bool OnCollide(GameObject collidingObject)
		{
			// deal damage to collidingObject IF it has a health component
			Helper.DebugLogger.Log<Item_Projectile>("OnCollide() with " + collidingObject.name);
			return true;
		}

		public void UpdateItem(GameObject projectile)
		{
			projectile.transform.position += projectile.transform.forward * projectileSpeed * Time.deltaTime;
		}
	}
}