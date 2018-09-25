EventSystem
========================================
Includes:
- ES_GameEvent.cs
- ES_GameEventListener.cs
- ES_GameEventInspector.cs
- ES_EventManager.cs
- DebugEvent.cs

ES_GameEvent
- Is a custom implementation of events using ScriptableObjects and UnityEvents to allow for quick creation of event assets.
- Has a custom inspector script to allow for easy testing/debugging with the "Raise Event" button when viewing the event .asset file in Inspector.
- The "Raise Event" button calls the event's Invoke() function, which all registered ES_GameEventListeners are listening out for

ES_GameEventListener
- Is a custom implementation of event listeners for ES_GameEvents that is easy to understand without any prior coding knowledge.
- Just drag attach a ES_GameEventListener component to any GameObejct that you want to react to a ES_GameEvent and drag the ES_GameEvent asset file to the Response's object field and select the appropriate function to call.

ES_EventManager
- Holds all ES_GameEvents that are created at runtime.
- Use the 'StartListening()' or 'AddEvent()' functions to create ES_GameEvents during runtime. 
	- The StartListening(string eventName, UnityAction function) function adds a new ES_GameEventListener to a registered ES_GameEvent, or registers a new ES_GameEvent if none are found.
	- The AddEvent(string eventName) function registers a new ES_GameEvent to the EventManager. Use the 'StartListening()' function to add listeners to this event.
- Use the 'RemoveListener()' or 'RemoveAllListeners()' functions to remove all ES_GameEventListeners from a particular event.
- Use the 'RemoveAllEvents()' function to remove all ES_GameEvents and their respective listeners from the EventManager.

DebugEvent
- Sample script to show the functionality of ES_GameEvents
- Writes a debug log to the Unity Console when the registered ES_GameEvent is triggered.