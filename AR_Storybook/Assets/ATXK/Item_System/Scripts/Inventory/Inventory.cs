namespace ATXK.ItemSystem
{
	using UnityEngine;
	using System.Collections.Generic;

	[CreateAssetMenu(menuName = "Item/Inventory")]
	public class Inventory : ScriptableObject
	{
		[SerializeField] List<Item_Base> items;
		[SerializeField] bool clearOnLoad;

		#region Property Getters
		public List<Item_Base> Items { get { return items; } }
		#endregion

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

		public void AddItem(Item_Base item)
		{
			items.Add(item);
		}

		public bool RemoveItem(Item_Base item)
		{
			return items.Remove(item);
		}
	}
}