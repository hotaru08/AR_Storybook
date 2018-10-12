namespace ATXK.ItemSystem
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "Item/Item/Equipable")]
	public class Item_Equipable : Item_Base, IPickable, IEquipable
	{
		public override bool OnCollide(GameObject collidingObject)
		{
			return false;
		}

		public void OnPickup(Inventory inventory)
		{
			inventory.AddItem(this);
		}

		public bool OnDrop(Inventory inventory)
		{
			return inventory.RemoveItem(this);
		}

		public void OnEquip(Inventory inventory)
		{

		}

		public void OnUnequip(Inventory inventory)
		{

		}
	}
}