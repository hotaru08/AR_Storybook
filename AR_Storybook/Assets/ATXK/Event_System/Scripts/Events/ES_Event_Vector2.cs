namespace ATXK.EventSystem
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "Event/Vector2", order = 7)]
	public class ES_Event_Vector2 : ES_Event_Generic<Vector2>
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
				if (listeners[i].ObjectInstanceID == listenerInstanceID || listenerInstanceID == null)
				{
					listeners[i].OnEventRaised(value);
				}
			}
		}

		public override void RaiseEvent(Vector2 value)
		{
            Value = value;
            for (int i = listeners.Count - 1; i >= 0; i--)
			{
				listeners[i].OnEventRaised(value);
			}
		}

		public override void RaiseEvent(Vector2 value, int? listenerInstanceID)
		{
            Value = value;
            for (int i = listeners.Count - 1; i >= 0; i--)
			{
				if (listeners[i].ObjectInstanceID == listenerInstanceID || listenerInstanceID == null)
				{
					listeners[i].OnEventRaised(value);
				}
			}
		}
	}
}