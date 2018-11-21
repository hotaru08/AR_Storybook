namespace ATXK.ItemSystem
{
	using UnityEngine;
	using EventSystem;

	/// <summary>
	/// Projectile item that can cause damage.
	/// </summary>
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

		/// <summary>
		/// Called when a collider (with isTrigger set to true) enters this collider.
		/// </summary>
		/// <param name="collidingObject">Colliding object.</param>
		public override bool OnTriggerEnter(Collider collidingObject)
		{
			ES_Event_Int dataEvent = collisionEnterEvent as ES_Event_Int;
			if (dataEvent != null)
				dataEvent.Value = -projectileDamage;

			return base.OnTriggerEnter(collidingObject);
		}

		/// <summary>
		/// Called when a collider (with isTrigger set to true) exits this collider.
		/// </summary>
		/// <param name="collidingObject">Colliding object.</param>
		public override bool OnTriggerStay(Collider collidingObject)
		{
			ES_Event_Int dataEvent = collisionEnterEvent as ES_Event_Int;
			if (dataEvent != null)
				dataEvent.Value = -projectileDamage;

			return base.OnTriggerStay(collidingObject);
		}

		/// <summary>
		/// Called when a collider (with isTrigger set to true) is within this collider.
		/// </summary>
		/// <param name="collidingObject">Colliding object.</param>
		public override bool OnTriggerExit(Collider collidingObject)
		{
			ES_Event_Int dataEvent = collisionEnterEvent as ES_Event_Int;
			if (dataEvent != null)
				dataEvent.Value = -projectileDamage;

			return base.OnTriggerExit(collidingObject);
		}

		/// <summary>
		/// Called when this object becomes enabled.
		/// </summary>
		public override void Enabled()
		{
			
		}

		/// <summary>
		/// Called when this object becomes disabled.
		/// </summary>
		public override void Disabled()
		{ 
			
		}

		/// <summary>
		/// Updates the gameObject holding this item asset.
		/// </summary>
		/// <param name="projectile">GameObject holding this item.</param>
		public virtual void UpdateItem(GameObject projectile)
		{
			projectile.transform.position += projectile.transform.forward * projectileSpeed * Time.deltaTime;
		}
	}
}