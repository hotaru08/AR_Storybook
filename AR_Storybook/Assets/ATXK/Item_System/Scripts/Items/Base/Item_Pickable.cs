namespace ATXK.ItemSystem
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "Item/Item/Pickable")]
	public class Item_Pickable : Item_Base, IPickable
	{
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
			return true;
		}

		public override void Enabled()
		{
			
		}

		public override void Disabled()
		{

		}

		public void OnPickup(Inventory inventory)
		{
			inventory.AddItem(this);
		}

		public bool OnDrop(Inventory inventory)
		{
			return inventory.RemoveItem(this);
		}
	}
}