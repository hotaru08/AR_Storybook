namespace ATXK.ItemSystem
{
	/// <summary>
	/// Interface for items that can be equipped from within an Inventory.
	/// </summary>
	public interface IEquipable
	{
		/// <summary>
		/// Equips this item in the provided inventory.
		/// </summary>
		/// <param name="inventory">Inventory should equip this item.</param>
		void OnEquip(Inventory inventory);

		/// <summary>
		/// Un-equips this item from the provided inventory.
		/// </summary>
		/// <param name="inventory">Inventory should un-equip this item.</param>
		void OnUnequip(Inventory inventory);
	}
}
