namespace ATXK.EventSystem
{
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Events;

	/// <summary>
	/// Custom game event that utilises ScriptableObjects.
	/// </summary>
	[CreateAssetMenu(menuName = "ATXK/Events/GameEvent")]
	public class ES_GameEvent : ScriptableObject
	{
		/// <summary>
		/// List of all the listeners for this event.
		/// </summary>
		[SerializeField] List<ES_GameEventListener> eventListeners = new List<ES_GameEventListener>();

		/// <summary>
		/// Invokes the event.
		/// </summary>
		public void Invoke()
		{
			for(int i = eventListeners.Count - 1; i >= 0; i--)
			{
				eventListeners[i].OnEventRaised();
			}
		}

		/// <summary>
		/// Adds the listener to this event.
		/// </summary>
		/// <param name="listener"></param>
		public void AddListener(ES_GameEventListener listener)
		{
			eventListeners.Add(listener);
		}

		/// <summary>
		/// Removes the listener from this event.
		/// </summary>
		/// <param name="listener"></param>
		public void RemoveListener(ES_GameEventListener listener)
		{
			eventListeners.Remove(listener);
		}

		/// <summary>
		/// Removes all listeners from this event.
		/// </summary>
		public void RemoveAllListeners()
		{
			for(int i = eventListeners.Count - 1; i >= 0; i--)
			{
				eventListeners.Remove(eventListeners[i]);
			}
		}
	}
}