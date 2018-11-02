namespace ATXK.ItemSystem
{
	using UnityEngine;
	using EventSystem;

	[CreateAssetMenu(menuName = "Item/Item/Projectile/Flat Projectile")]
	public class Item_Projectile : Item_Base, IUpdateable
	{
		[Header("Projectile Settings")]
		[SerializeField] protected float projectileSpeed;
		[SerializeField] protected int projectileDamage;
		[SerializeField] protected float projectileDuration;

		#region Property Getters
		public float Speed { get { return projectileSpeed; } }
		public float Duration { get { return projectileDuration; } }
		public int Damage { get { return projectileDamage; } }
		#endregion

		public override bool OnTriggerEnter(Collider collidingObject)
		{
			if(collisionEnterEvent != null)
				collisionEnterEvent.RaiseEvent();
			if (collisionExitEvent == null && collisionInsideEvent == null)
				return true;
			return false;
		}

		public override bool OnTriggerExit(Collider collidingObject)
		{
			if (collisionExitEvent != null)
				collisionExitEvent.RaiseEvent();
			return true;
		}

		public override bool OnTriggerStay(Collider collidingObject)
		{
			if (collisionInsideEvent != null)
				collisionInsideEvent.RaiseEvent();
			if (collisionEnterEvent == null && collisionExitEvent == null)
				return true;
			return false;
		}

		public override void Enabled()
		{
			
		}

		public override void Disabled()
		{ 
			
		}

		public virtual void UpdateItem(GameObject projectile)
		{
			projectile.transform.position += projectile.transform.forward * projectileSpeed * Time.deltaTime;
		}
	}
}