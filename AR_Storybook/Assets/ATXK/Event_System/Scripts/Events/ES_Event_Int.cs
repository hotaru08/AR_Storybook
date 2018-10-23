namespace ATXK.EventSystem
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "TestSpace/Event/Int Event", order = 3)]
	public class ES_Event_Int : ES_Event_Generic<int>
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

		public override void Invoke(int value, int? listenerInstanceID = null)
		{
			this.value = value;

			for (int i = listeners.Count - 1; i >= 0; i--)
				if (listeners[i].gameObject.GetInstanceID() == listenerInstanceID || listenerInstanceID == null)
					listeners[i].OnEventRaised(value);
		}
	}
}