namespace ATXK.EventSystem
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "Event/Object Event", order = 7)]
	public class ES_Event_Object : ES_Event
	{
		public Object value;

		public override void Invoke()
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
			{
				eventListeners[i].OnEventRaised(value);
			}
		}

		public void Invoke(Object newValue)
		{
			value = newValue;
			Invoke();
		}
	}
}