namespace ARStorybook.LaneItems
{
	using UnityEngine;
	using ATXK.ItemSystem;

	[RequireComponent(typeof(BoxCollider))]
	public class ConveyorBelt : MonoBehaviour
	{
		[SerializeField] float conveyorSpeed;

		BoxCollider collider;

		private void Awake()
		{
			collider = GetComponent<BoxCollider>();
			collider.isTrigger = true;
		}

		private void OnTriggerStay(Collider other)
		{
			// Only affect Items
			// Moves items along the conveyor in the direction of the conveyor's forward
			if(other.GetComponent<Item_Holder>())
			{
				other.transform.position += (transform.forward * conveyorSpeed) * Time.deltaTime;
			}
		}
	}
}
