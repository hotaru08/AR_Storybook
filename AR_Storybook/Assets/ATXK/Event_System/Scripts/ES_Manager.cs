namespace ATXK.EventSystem
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using ATXK.Helper;
	using UnityEngine.Events;

	public class ES_Manager : SingletonBehaviourless<ES_Manager>
	{
		Dictionary<string, ES_GameEvent> events = new Dictionary<string, ES_GameEvent>();

		/// <summary>
		/// Registers a new GameEvent without any registered listeners.
		/// </summary>
		/// <param name="eventName">Name of the GameEvent.</param>
		/// <returns>True if operation is successful.</returns>
		public static bool AddEvent(string eventName)
		{
			if(!Instance.events.ContainsKey(eventName))
			{
				ES_GameEvent gameEvent = new ES_GameEvent();
				Instance.events.Add(eventName, gameEvent);

				DebugLogger.Log<ES_Manager>("Succesfully registered new ES_GameEvent with name '" + eventName + "'.");
				return true;
			}

			DebugLogger.Log<ES_Manager>("Failed to register new ES_GameEvent with name '" + eventName + "'. Event already exists within dictionary.");
			return false;
		}

		/// <summary>
		/// Unregisters the specified GameEvent.
		/// </summary>
		/// <param name="eventName">Name of the GameEvent.</param>
		/// <returns>True if operation is successful.</returns>
		public static bool RemoveEvent(string eventName)
		{
			if (Instance.events.ContainsKey(eventName))
			{
				Instance.events.Remove(eventName);

				DebugLogger.Log<ES_Manager>("Succesfully unregistered ES_GameEvent with name '" + eventName + "'.");
				return true;
			}

			DebugLogger.Log<ES_Manager>("Failed to unregister new ES_GameEvent with name '" + eventName + "'. Event does not exist within dictionary.");
			return false;
		}

		/// <summary>
		/// Removes all listeners from all registered GameEvents, then deregisters all GameEvents.
		/// </summary>
		public static void RemoveAllEvents()
		{
			//Loop through all registered UnityEvents and remove all listeners
			foreach (KeyValuePair<string, ES_GameEvent> gameEvent in Instance.events)
			{
				//Removes all listeners from the UnityEvent
				gameEvent.Value.RemoveAllListeners();

				DebugLogger.Log<ES_Manager>("Removed all Listeners from ES_GameEvent '" + gameEvent + "'.");
			}

			//Clear the dictionary
			Instance.events.Clear();

			DebugLogger.Log<ES_Manager>("Removed all ES_GameEvent from EventManager registry.");
		}

		/// <summary>
		/// Adds listener to the specified GameEvent, if the event has been registered.
		/// </summary>
		/// <param name="eventName">Name of the GameEvent.</param>
		/// <param name="listener">Listener that will listen for the specified GameEvent.</param>
		public static void StartListening(string eventName, ES_GameEventListener listener)
		{
			//Try to get event from dictionary
			ES_GameEvent gameEvent = null;
			if (Instance.events.TryGetValue(eventName, out gameEvent))
			{
				//Register listening function
				gameEvent.AddListener(listener);

				DebugLogger.Log<ES_Manager>("Succesfully registered new listener to ES_GameEvent with name '" + eventName + "'.");
				return;
			}
			//Create new event and register listening function
			AddEvent(eventName);
			//Recurse this function
			StartListening(eventName, listener);
		}

		/// <summary>
		/// Adds listener to the specified GameEvent, if the event has been registered.
		/// </summary>
		/// <param name="eventName">Name of the GameEvent.</param>
		/// <param name="function">Function that will listen for the specified GameEvent.</param>
		public static void StartListening(string eventName, UnityAction function)
		{
			ES_GameEventListener listener = new ES_GameEventListener();
			listener.response.AddListener(function);

			StartListening(eventName, listener);
		}

		/// <summary>
		/// Removes listener from the specified GameEvent, if the event has been registered.
		/// </summary>
		/// <param name="eventName">Name of the GameEvent.</param>
		/// <param name="listener">Listener that will listen for the specified GameEvent.</param>
		public static void StopListening(string eventName, ES_GameEventListener listener)
		{
			//Try to get event from dictionary
			ES_GameEvent gameEvent = null;
			if (Instance.events.TryGetValue(eventName, out gameEvent))
			{
				//Unregister listening function
				gameEvent.RemoveListener(listener);

				DebugLogger.Log<ES_Manager>("Succesfully unregistered listener to ES_GameEvent with name '" + eventName + "'.");
				return;
			}
		}

		/// <summary>
		/// Removes listener from the specified GameEvent, if the event has been registered.
		/// </summary>
		/// <param name="eventName">Name of the GameEvent.</param>
		/// <param name="function">Function that will listen for the specified GameEvent.</param>
		public static void StopListening(string eventName, UnityAction function)
		{
			ES_GameEventListener listener = new ES_GameEventListener();
			listener.response.AddListener(function);

			StopListening(eventName, listener);
		}

		/// <summary>
		/// Calls the specified GameEvent, if the event has been registered.
		/// </summary>
		/// <param name="eventName">Name of the GameEvent.</param>
		public static void CallEvent(string eventName)
		{
			//Try to get event from dictionary
			ES_GameEvent gameEvent = null;
			if (Instance.events.TryGetValue(eventName, out gameEvent))
			{
				//Invokes the UnityEvent
				gameEvent.Invoke();

				DebugLogger.Log<ES_Manager>("Invoked the ES_GameEvent with name '" + eventName + "'.");
				return;
			}
			DebugLogger.Log<ES_Manager>("ES_GameEvent with name '" + eventName + "' does not exist.");
		}
	}
}