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

		[Header("Item Collision Events")]
		[SerializeField] protected ES_Event_Abstract collisionEnterEvent;
		[SerializeField] protected ES_Event_Abstract collisionExitEvent;
		[SerializeField] protected ES_Event_Abstract collisionInsideEvent;

		#region Property Getters
		public string Name { get { return itemName; } }
		public string Description { get { return itemDescription; } }
		public int Weight { get { return itemWeight; } }
		public int Value { get { return itemValue; } }
		public CV_Enum Type { get { return itemType; } }
		public GameObject Model { get { return itemModel; } }
		public ES_Event_Abstract CollisionEnterEvent { get { return collisionEnterEvent; } }
		public ES_Event_Abstract CollisionExitEvent { get { return collisionExitEvent; } }
		public ES_Event_Abstract CollisionInsideEvent { get { return collisionInsideEvent; } }
		#endregion

		public abstract bool OnTriggerEnter(Collider collidingObject);

		public abstract bool OnTriggerExit(Collider collidingObject);

		public abstract bool OnTriggerStay(Collider collidingObject);

		public abstract void Enabled();

		public abstract void Disabled();
	}
}