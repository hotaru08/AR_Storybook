namespace ATXK.ItemSystem
{
	using UnityEngine;
	using CustomVariables;
	using EventSystem;
	using System.Collections.Generic;

	/// <summary>
	/// Base class for all Items that holds basic information about itself.
	/// </summary>
	public abstract class Item_Base : ScriptableObject
	{
		[Header("Statistics")]
		[SerializeField] protected string itemName;
		[SerializeField] protected string itemDescription;
		[SerializeField] protected int itemWeight;
		[SerializeField] protected int itemValue;
		[SerializeField] protected CV_Enum itemType;
		[SerializeField] protected GameObject itemModel;

		[Header("Collision Events")]
		[SerializeField] protected ES_Event_Abstract collisionEnterEvent;
		[SerializeField] protected ES_Event_Abstract collisionExitEvent;
		[SerializeField] protected ES_Event_Abstract collisionInsideEvent;

		[Header("Collision Event Settings")]
		[SerializeField] protected bool targettedBroadcast;
		[SerializeField] protected List<string> tagsToIgnore = new List<string>();

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

		/// <summary>
		/// Called when a collider (with isTrigger set to true) enters this collider.
		/// </summary>
		/// <param name="collidingObject">Colliding object.</param>
		public virtual bool OnTriggerEnter(Collider collidingObject)
		{
			if (tagsToIgnore.Contains(collidingObject.gameObject.tag) && tagsToIgnore.Count > 0)
				return false;

			// Check if events are targetted.
			if(collisionEnterEvent != null && targettedBroadcast)
				collisionEnterEvent.RaiseEvent(collidingObject.gameObject.GetInstanceID());
			else if (collisionEnterEvent != null && !targettedBroadcast)
				collisionEnterEvent.RaiseEvent();

			// Only return true if there are no other event cases.
			if (collisionExitEvent == null && collisionInsideEvent == null)
				return true;

			return false;
		}

		/// <summary>
		/// Called when a collider (with isTrigger set to true) exits this collider.
		/// </summary>
		/// <param name="collidingObject">Colliding object.</param>
		public virtual bool OnTriggerExit(Collider collidingObject)
		{
			if (tagsToIgnore.Contains(collidingObject.gameObject.tag))
				return false;

			// Check if events are targetted.
			if (collisionExitEvent != null && targettedBroadcast)
				collisionExitEvent.RaiseEvent(collidingObject.gameObject.GetInstanceID());
			else if (collisionEnterEvent != null && !targettedBroadcast)
				collisionExitEvent.RaiseEvent();

			return true;
		}

		/// <summary>
		/// Called when a collider (with isTrigger set to true) is within this collider.
		/// </summary>
		/// <param name="collidingObject">Colliding object.</param>
		public virtual bool OnTriggerStay(Collider collidingObject)
		{
			if (tagsToIgnore.Contains(collidingObject.gameObject.tag))
				return false;

			// Check if events are targetted.
			if (collisionInsideEvent != null && targettedBroadcast)
				collisionInsideEvent.RaiseEvent(collidingObject.gameObject.GetInstanceID());
			else if (collisionInsideEvent != null && !targettedBroadcast)
				collisionInsideEvent.RaiseEvent();

			// Only return true if there are no other event cases.
			if (collisionEnterEvent == null && collisionExitEvent == null)
				return true;

			return false;
		}

		/// <summary>
		/// Called when the object becomes enabled.
		/// </summary>
		public abstract void Enabled();

		/// <summary>
		/// Called when the object becomes disabled.
		/// </summary>
		public abstract void Disabled();
	}
}