namespace ATXK.EventSystem
{
	using UnityEngine;
	using System.Collections.Generic;

	public abstract class ES_Event_Abstract : ScriptableObject
	{
		public List<ES_EventListener> listeners = new List<ES_EventListener>();

		/// <summary>
		/// Calls OnEventRaised() on all listeners registered to this event.
		/// </summary>
		public virtual void RaiseEvent()
		{
			for(int i = listeners.Count - 1; i >= 0 ; i--)
			{
				listeners[i].OnEventRaised();
			}
		}

		/// <summary>
		/// Calls OnEventRaised() on the registered listener with the same Instance ID.
		/// </summary>
		public virtual void RaiseEvent(int? listenerInstanceID)
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
			{
				if(listeners[i].ObjectInstanceID == listenerInstanceID)
					listeners[i].OnEventRaised();
			}
		}

		/// <summary>
		/// Registers a new listener to this event. A listener cannot subscribe to an event more than once.
		/// </summary>
		/// <param name="listener">The event listener that wants to listen to this event.</param>
		public virtual void AddListener(ES_EventListener listener)
		{
			if (!listeners.Contains(listener))
				listeners.Add(listener);
		}

		/// <summary>
		/// Unregisters a listener to this event.
		/// </summary>
		/// <param name="listener">The event listener that will be removed.</param>
		public virtual void RemoveListener(ES_EventListener listener)
		{
			if (listeners.Contains(listener))
				listeners.Remove(listener);
		}
	}
}