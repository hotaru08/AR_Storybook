namespace ATXK.ItemSystem
{
	using UnityEngine;
	using CustomVariables;
	using EventSystem;
	
	public abstract class Item_Base : ScriptableObject
	{
		[Header("Item Statistics")]
		[SerializeField] protected string itemName;
		[SerializeField] protected string itemDescription;
		[SerializeField] protected int itemWeight;
		[SerializeField] protected int itemValue;
		[SerializeField] protected CV_Enum itemType;
		[SerializeField] protected GameObject itemModel;

		[Header("Item Events")]
		[SerializeField] protected ES_Event collisionEvent;

		#region Property Getters
		public string Name { get { return itemName; } }
		public string Description { get { return itemDescription; } }
		public int Weight { get { return itemWeight; } }
		public int Value { get { return itemValue; } }
		public CV_Enum Type { get { return itemType; } }
		public GameObject Model { get { return itemModel; } }
		public ES_Event CollisionEvent { get { return collisionEvent; } }
		#endregion

		public abstract bool OnCollide(GameObject collidingObject);

		public abstract void Enabled();

		public abstract void Disabled();
	}
}