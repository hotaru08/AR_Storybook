namespace ATXK.EventSystem
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "TestSpace/Event/Float Event", order = 4)]
	public class ES_Event_Float : ES_Event_Generic<float>
	{
		public override void RaiseEvent()
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
			{
				listeners[i].OnEventRaised();
			}
		}

		public override void RaiseEvent(int? listenerInstanceID)
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
			{
				if (listeners[i].ObjectInstanceID == listenerInstanceID)
				{
					listeners[i].OnEventRaised();
				}
			}
		}

		public override void RaiseEvent(float value)
		{
            Value = value;
            for (int i = listeners.Count - 1; i >= 0; i--)
			{
				listeners[i].OnEventRaised(value);
			}
		}

		public override void RaiseEvent(float value, int? listenerInstanceID)
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