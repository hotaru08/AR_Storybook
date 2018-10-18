namespace ATXK.ItemSystem
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "Item/Item/Equipable")]
	public class Item_Equipable : Item_Pickable, IEquipable
	{
		public void OnEquip(Inventory inventory)
		{

		}

		public void OnUnequip(Inventory inventory)
		{

		}
	}
}