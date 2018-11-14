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
			ES_Event_Int dataEvent = collisionEnterEvent as ES_Event_Int;
			if (dataEvent != null)
				dataEvent.Value = -projectileDamage;

			return base.OnTriggerEnter(collidingObject);
		}

		public override bool OnTriggerStay(Collider collidingObject)
		{
			ES_Event_Int dataEvent = collisionEnterEvent as ES_Event_Int;
			if (dataEvent != null)
				dataEvent.Value = -projectileDamage;

			return base.OnTriggerStay(collidingObject);
		}

		public override bool OnTriggerExit(Collider collidingObject)
		{
			ES_Event_Int dataEvent = collisionEnterEvent as ES_Event_Int;
			if (dataEvent != null)
				dataEvent.Value = -projectileDamage;

			return base.OnTriggerExit(collidingObject);
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