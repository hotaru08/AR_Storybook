namespace ATXK.ItemSystem
{
	using UnityEngine;

	public class Inventory_Holder : MonoBehaviour
	{
		[SerializeField] Inventory inventory;

		public Inventory Inventory { get { return inventory; } }

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