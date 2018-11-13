namespace ATXK.ItemSystem
{
	using UnityEngine;
	
	[RequireComponent(typeof(BoxCollider))]
	[RequireComponent(typeof(Rigidbody))]
	public class Item_Despawner : MonoBehaviour
	{
		BoxCollider collider;

		private void Awake()
		{
			collider = GetComponent<BoxCollider>();
		}

		private void OnTriggerEnter(Collider other)
		{
			if(other.gameObject.GetComponent<Item_Holder>())
			{
				Destroy(other.gameObject);
			}
		}
	}
}