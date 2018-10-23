namespace ATXK.EventSystem
{
	using Helper;

	public class ES_Event_Base : ES_Event_Abstract
	{
		public override void Invoke()
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
				listeners[i].OnEventRaised();
		}

		public override void Invoke(int? listenerInstanceID = null)
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
				if(listeners[i].gameObject.GetInstanceID() == listenerInstanceID || listenerInstanceID == null)
					listeners[i].OnEventRaised();
		}

		public override void AddListener(ES_EventListener listener)
		{
			if (!listeners.Contains(listener))
				listeners.Add(listener);
			else
				DebugLogger.LogWarning<ES_Event_Abstract>("AddListener(ES_EventListener): Listener is already registered.");
		}

		public override void RemoveListener(ES_EventListener listener)
		{
			if (listeners.Contains(listener))
				listeners.Remove(listener);
			else
				DebugLogger.LogWarning<ES_Event_Abstract>("RemoveListener(ES_EventListener): Listener is not registered.");
		}

		public override void RemoveAllListeners()
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
				listeners.RemoveAt(i);
		}
	}
}