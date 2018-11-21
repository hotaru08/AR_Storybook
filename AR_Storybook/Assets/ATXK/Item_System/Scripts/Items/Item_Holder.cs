namespace ATXK.ItemSystem
{
	using UnityEngine;

	/// <summary>
	/// Component that holds a reference to an Item asset.
	/// </summary>
	public class Item_Holder : MonoBehaviour
	{
		[SerializeField] Item_Base item;
		Item_Base runtimeItem;

		IUpdateable updateable;

		public Item_Base Item { get { return item; } }

		/// <summary>
		/// Called once when this object is serialized.
		/// </summary>
		private void Awake()
		{
			runtimeItem = Instantiate(item);
			updateable = runtimeItem as IUpdateable;
		}

		/// <summary>
		/// Called every frame.
		/// </summary>
		private void Update()
		{
			if (updateable != null)
				updateable.UpdateItem(gameObject);
		}

		/// <summary>
		/// Called when a collider (with isTrigger set to true) enters this collider.
		/// </summary>
		/// <param name="collidingObject">Colliding object.</param>
		private void OnTriggerEnter(Collider collidingObject)
		{
			if (runtimeItem.OnTriggerEnter(collidingObject))
				Destroy(gameObject);
		}

		/// <summary>
		/// Called when a collider (with isTrigger set to true) exits this collider.
		/// </summary>
		/// <param name="collidingObject">Colliding object.</param>
		private void OnTriggerExit(Collider collidingObject)
		{
			if (runtimeItem.OnTriggerExit(collidingObject))
				Destroy(gameObject);
		}

		/// <summary>
		/// Called when a collider (with isTrigger set to true) is within this collider.
		/// </summary>
		/// <param name="collidingObject">Colliding object.</param>
		private void OnTriggerStay(Collider collidingObject)
		{
			if (runtimeItem.OnTriggerStay(collidingObject))
				Destroy(gameObject);
		}

		/// <summary>
		/// Called when this object becomes enabled.
		/// </summary>
		private void OnEnable()
		{
			runtimeItem.Enabled();
		}

		/// <summary>
		/// Called before this object becomes disabled.
		/// </summary>
		private void OnDisable()
		{
			runtimeItem.Disabled();
		}
	}
}