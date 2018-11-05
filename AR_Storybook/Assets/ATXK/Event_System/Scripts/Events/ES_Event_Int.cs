namespace ATXK.EventSystem
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "Event/Int", order = 3)]
	public class ES_Event_Int : ES_Event_Generic<int>
	{
		public override void RaiseEvent()
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
			{
                listeners[i].OnEventRaised(value);
			}
		}

		public override void RaiseEvent(int? listenerInstanceID)
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
			{
				if (listeners[i].ObjectInstanceID == listenerInstanceID)
				{
					listeners[i].OnEventRaised(value);
				}
			}
		}

		public override void RaiseEvent(int value)
		{
            Value = value;
            for (int i = listeners.Count - 1; i >= 0; i--)
			{
				listeners[i].OnEventRaised(value);
			}
		}

		public override void RaiseEvent(int value, int? listenerInstanceID)
		{
            Value = value;
            for (int i = listeners.Count - 1; i >= 0; i--)
			{
				if (listeners[i].ObjectInstanceID == listenerInstanceID)
				{
					listeners[i].OnEventRaised(value);
				}
			}
		}
	}
}