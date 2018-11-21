namespace ATXK.ItemSystem
{
	/// <summary>
	/// Interface for items that can be picked up and added to an Inventory.
	/// </summary>
	public interface IPickable
	{
		/// <summary>
		/// Adds this item to the provided inventory.
		/// </summary>
		/// <param name="inventory">Inventory to add this item to.</param>
		void OnPickup(Inventory inventory);

		/// <summary>
		/// Removes this item to the provided inventory.
		/// </summary>
		/// <param name="inventory">Inventory to remove this item from.</param>
		bool OnDrop(Inventory inventory);
	}
}