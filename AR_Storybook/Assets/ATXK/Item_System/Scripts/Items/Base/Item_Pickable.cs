namespace ATXK.ItemSystem
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "Item/Item/Pickable")]
	public class Item_Pickable : Item_Base, IPickable
	{
		public override bool OnCollide(GameObject collidingObject)
		{
			Inventory_Holder inventoryHolder = collidingObject.GetComponent<Inventory_Holder>();
			if(inventoryHolder != null)
			{
				OnPickup(inventoryHolder.Inventory);

				return true;
			}

			if (collisionEvent != null)
				collisionEvent.Invoke();

			return false;
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