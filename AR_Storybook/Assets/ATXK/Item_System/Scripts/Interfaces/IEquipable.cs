namespace ATXK.ItemSystem
{
	public interface IEquipable
	{
		void OnEquip(Inventory inventory);

		void OnUnequip(Inventory inventory);
	}
}
