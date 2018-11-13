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
                if (other.gameObject.GetComponent<Item_Holder>().Item.CollisionExitEvent)
                {
                    //Debug.Log("Entered : " + other.gameObject.GetComponent<Item_Holder>().Item.CollisionExitEvent);
                    other.gameObject.GetComponent<Item_Holder>().Item.CollisionExitEvent.RaiseEvent();
                    Destroy(other.gameObject, 1f);
                    return;
                }

                Destroy(other.gameObject);
			}
		}
	}
}