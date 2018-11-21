namespace ATXK.ItemSystem
{
	using UnityEngine;

	/// <summary>
	/// Item that can be picked up.
	/// </summary>
	[CreateAssetMenu(menuName = "Item/Item/Pickable")]
	public class Item_Pickable : Item_Base, IPickable
	{
		/// <summary>
		/// Called when a collider (with isTrigger set to true) enters this collider.
		/// </summary>
		/// <param name="collidingObject">Colliding object.</param>
		public override bool OnTriggerEnter(Collider collidingObject)
		{
			Inventory_Holder inventoryHolder = collidingObject.gameObject.GetComponent<Inventory_Holder>();
			if(inventoryHolder != null)
			{
				OnPickup(inventoryHolder.Inventory);

				return true;
			}

			if (collisionEnterEvent != null)
				collisionEnterEvent.RaiseEvent();

			return false;
		}

		/// <summary>
		/// Called when a collider (with isTrigger set to true) exits this collider.
		/// </summary>
		/// <param name="collidingObject">Colliding object.</param>
		public override bool OnTriggerExit(Collider collidingObject)
		{
			if (collisionExitEvent != null)
				collisionExitEvent.RaiseEvent();
			return true;
		}

		/// <summary>
		/// Called when a collider (with isTrigger set to true) is within this collider.
		/// </summary>
		/// <param name="collidingObject">Colliding object.</param>
		public override bool OnTriggerStay(Collider collidingObject)
		{
			if (collisionInsideEvent != null)
				collisionInsideEvent.RaiseEvent();
			return true;
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
		/// Adds this item to the provided inventory.
		/// </summary>
		/// <param name="inventory">Inventory to add this item to.</param>
		public void OnPickup(Inventory inventory)
		{
			inventory.AddItem(this);
		}

		/// <summary>
		/// Removes this item to the provided inventory.
		/// </summary>
		/// <param name="inventory">Inventory to remove this item from.</param>
		public bool OnDrop(Inventory inventory)
		{
			return inventory.RemoveItem(this);
		}
	}
}