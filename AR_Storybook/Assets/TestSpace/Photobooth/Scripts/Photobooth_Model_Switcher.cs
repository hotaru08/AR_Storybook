namespace TestSpace
{
	using UnityEngine;
	using ATXK.EventSystem;

	public class Photobooth_Model_Switcher : Photobooth_Generic_Switcher<GameObject>
	{
		[SerializeField] ES_Event_UnityObject changeObjectEvent;

		private void Start()
		{
			Switch(0);
		}

		public override void NextObject()
		{
			base.NextObject();

			changeObjectEvent.Invoke(CurrentObject, null);
		}

		public override void PrevObject()
		{
			base.PrevObject();

			changeObjectEvent.Invoke(CurrentObject, null);
		}

		public override void Switch(int index)
		{
			base.Switch(index);

			changeObjectEvent.Invoke(CurrentObject, null);
		}
	}
}