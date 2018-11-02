namespace ATXK.EventSystem.Examples
{
	using UnityEngine;

	public class ButtonInput : MonoBehaviour
	{
		[SerializeField] ES_Event_String stringEvent;
		[SerializeField] ES_Event_Int intEvent;

		[SerializeField] string newString;
		[SerializeField] int newInt;

		[SerializeField] GameObject target;

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.A))
			{
				if(target != null)
					stringEvent.RaiseEvent(newString, target.GetInstanceID());
				else
					stringEvent.RaiseEvent(newString);
			}
			if (Input.GetKeyDown(KeyCode.S))
			{
				if (target != null)
					intEvent.RaiseEvent(newInt, target.GetInstanceID());
				else
					intEvent.RaiseEvent(newInt);
			}
		}
	}
}