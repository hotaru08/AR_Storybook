namespace ATXK.ItemSystem.Examples
{
	using UnityEngine;

	public class TestAddRemoveItems : MonoBehaviour
	{
		Inventory_Holder inventoryHolder;

		private void Start()
		{
			inventoryHolder = GetComponent<Inventory_Holder>();
		}

		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.R))
			{
				inventoryHolder.DropItem(inventoryHolder.Inventory.Items[inventoryHolder.Inventory.Items.Count - 1]);
			}
		}
	}
}