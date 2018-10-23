namespace ATXK.EventSystem
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "TestSpace/Event/Unity Object Event", order = 6)]
	public class ES_Event_UnityObject : ES_Event_Generic<Object>
	{
		public override void Invoke()
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
				listeners[i].OnEventRaised(value);
		}

		public override void Invoke(int? listenerInstanceID)
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
				if (listeners[i].gameObject.GetInstanceID() == listenerInstanceID || listenerInstanceID == null)
					listeners[i].OnEventRaised(value);
		}

		public override void Invoke(Object value, int? listenerInstanceID = null)
		{
			this.value = value;

			for (int i = listeners.Count - 1; i >= 0; i--)
				if (listeners[i].gameObject.GetInstanceID() == listenerInstanceID || listenerInstanceID == null)
					listeners[i].OnEventRaised(value);
		}
	}
}