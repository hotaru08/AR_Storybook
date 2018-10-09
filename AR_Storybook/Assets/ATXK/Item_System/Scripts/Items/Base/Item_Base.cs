namespace ATXK.ItemSystem
{
	using UnityEngine;
	using CustomVariables;
	
	public abstract class Item_Base : ScriptableObject
	{
		[Header("Item Statistics")]
		[SerializeField] string itemName;
		[SerializeField] string itemDescription;
		[SerializeField] int itemWeight;
		[SerializeField] int itemValue;
		[SerializeField] CV_Enum itemType;
		[SerializeField] GameObject itemModel;

		#region Property Getters
		public string Name { get { return itemName; } }
		public string Description { get { return itemDescription; } }
		public int Weight { get { return itemWeight; } }
		public int Value { get { return itemValue; } }
		public CV_Enum Type { get { return itemType; } }
		public GameObject Model { get { return itemModel; } }
		#endregion

		public abstract bool OnCollide(GameObject collidingObject);
	}
}