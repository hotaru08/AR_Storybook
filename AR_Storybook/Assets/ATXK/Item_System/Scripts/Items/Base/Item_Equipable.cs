namespace ATXK.ItemSystem
{
	using UnityEngine;

	/// <summary>
	/// Item that can be picked up and equipped.
	/// </summary>
	[CreateAssetMenu(menuName = "Item/Item/Equipable")]
	public class Item_Equipable : Item_Pickable, IEquipable
	{
		/// <summary>
		/// Equips this item in the provided inventory.
		/// </summary>
		/// <param name="inventory">Inventory should equip this item.</param>
		public void OnEquip(Inventory inventory)
		{

		}

		/// <summary>
		/// Un-equips this item from the provided inventory.
		/// </summary>
		/// <param name="inventory">Inventory should un-equip this item.</param>
		public void OnUnequip(Inventory inventory)
		{

		}
	}
}