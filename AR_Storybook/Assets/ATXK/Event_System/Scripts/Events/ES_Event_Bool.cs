namespace ATXK.EventSystem
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "Event/Bool Event", order = 2)]
	public class ES_Event_Bool : ES_Event_Generic<bool>
	{
		public override void Invoke()
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
				listeners[i].OnEventRaised(value);
		}

		public override void Invoke(int? listenerInstanceID = null)
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
				if (listeners[i].gameObject.GetInstanceID() == listenerInstanceID || listenerInstanceID == null)
					listeners[i].OnEventRaised(value);
		}

		public override void Invoke(bool value, int? listenerInstanceID = null)
		{
			this.value = value;

			for (int i = listeners.Count - 1; i >= 0; i--)
				if (listeners[i].gameObject.GetInstanceID() == listenerInstanceID || listenerInstanceID == null)
					listeners[i].OnEventRaised(value);
		}
	}
}