namespace ATXK.ItemSystem
{
	using UnityEngine;
	
	/// <summary>
	/// Component that removes an Item from the scene.
	/// </summary>
	[RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Rigidbody))]
	public class Item_Despawner : MonoBehaviour
	{
		BoxCollider collider;

		/// <summary>
		/// Called once when this object is serialized.
		/// </summary>
		private void Awake()
		{
			collider = GetComponent<BoxCollider>();
		}

		/// <summary>
		/// Called when a collider (with isTrigger set to true) enters this collider.
		/// </summary>
		/// <param name="collidingObject">Colliding object.</param>
		private void OnTriggerEnter(Collider collidingObject)
		{
			if(collidingObject.gameObject.GetComponent<Item_Holder>())
			{
                if (collidingObject.gameObject.GetComponent<Item_Holder>().Item.CollisionExitEvent != null)
                {
					collidingObject.gameObject.GetComponent<Item_Holder>().Item.CollisionExitEvent.RaiseEvent();
                    Destroy(collidingObject.gameObject, 1f);
                    return;
                }

                Destroy(collidingObject.gameObject);
			}
		}
	}
}