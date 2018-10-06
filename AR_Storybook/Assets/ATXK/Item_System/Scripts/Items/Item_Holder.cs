namespace ATXK.ItemSystem
{
	using UnityEngine;

	public class Item_Holder : MonoBehaviour
	{
		[SerializeField] Item_Base item;

		IUpdateable updateable;

		private void Start()
		{
			updateable = item as IUpdateable;
		}

		private void Update()
		{
			if (updateable != null)
				updateable.UpdateItem(gameObject);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (item.OnCollide(other.gameObject))
				Destroy(gameObject);
		}
	}
}