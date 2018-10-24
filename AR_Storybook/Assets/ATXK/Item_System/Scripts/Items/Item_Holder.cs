namespace ATXK.ItemSystem
{
	using UnityEngine;

	public class Item_Holder : MonoBehaviour
	{
		[SerializeField] Item_Base item;
		Item_Base runtimeItem;

		IUpdateable updateable;

		public Item_Base Item { get { return item; } }

		private void Awake()
		{
			runtimeItem = Instantiate(item);
			updateable = runtimeItem as IUpdateable;
		}

		private void Update()
		{
			if (updateable != null)
				updateable.UpdateItem(gameObject);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (runtimeItem.OnTriggerEnter(other))
				Destroy(gameObject);
		}

		private void OnTriggerExit(Collider other)
		{
			if (runtimeItem.OnTriggerExit(other))
				Destroy(gameObject);
		}

		private void OnTriggerStay(Collider other)
		{
			if (runtimeItem.OnTriggerStay(other))
				Destroy(gameObject);
		}

		private void OnEnable()
		{
			runtimeItem.Enabled();
		}

		private void OnDisable()
		{
			runtimeItem.Disabled();
		}
	}
}