namespace ATXK.ItemSystem
{
	using UnityEngine;

	/// <summary>
	/// Component that holds a reference to an Inventory asset.
	/// </summary>
	public class Inventory_Holder : MonoBehaviour
	{
		[SerializeField] Inventory inventory;

		public Inventory Inventory { get { return inventory; } }

		/// <summary>
		/// Removes an item from the referenced inventory and instantiates the item in scene.
		/// </summary>
		/// <param name="item">Item to drop.</param>
		public void DropItem(Item_Base item)
		{
			if(inventory.RemoveItem(item))
			{
				GameObject spawn = Instantiate(item.Model);
				Vector3 pos = transform.position + (transform.forward * transform.localScale.z) + (transform.forward * item.Model.transform.localScale.z);
				spawn.transform.position = pos;
				spawn.SetActive(true);
			}
		}
	}
}