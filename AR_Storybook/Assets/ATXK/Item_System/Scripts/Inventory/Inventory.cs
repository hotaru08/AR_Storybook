namespace ATXK.ItemSystem
{
	using UnityEngine;
	using System.Collections.Generic;

	/// <summary>
	/// Scene-independent inventory that contains a list of items.
	/// </summary>
	[CreateAssetMenu(menuName = "Item/Inventory")]
	public class Inventory : ScriptableObject
	{
		[SerializeField] List<Item_Base> items;
		[SerializeField] bool clearOnLoad;

		#region Property Getters
		public List<Item_Base> Items { get { return items; } }
		#endregion

		/// <summary>
		/// Called when the object becomes enabled.
		/// </summary>
		private void OnEnable()
		{
			if(clearOnLoad)
			{
				for(int i = items.Count - 1; i >= 0; i--)
				{
					RemoveItem(items[i]);
				}
			}
		}

		/// <summary>
		/// Adds an item to this inventory.
		/// </summary>
		/// <param name="item">Item to add.</param>
		public void AddItem(Item_Base item)
		{
			items.Add(item);
		}

		/// <summary>
		/// Removes an item from this inventory.
		/// </summary>
		/// <param name="item">Item to remove.</param>
		public bool RemoveItem(Item_Base item)
		{
			return items.Remove(item);
		}
	}
}