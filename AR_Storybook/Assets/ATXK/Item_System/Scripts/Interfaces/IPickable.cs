namespace ATXK.ItemSystem
{
	public interface IPickable
	{
		void OnPickup(Inventory inventory);

		bool OnDrop(Inventory inventory);
	}
}