namespace ATXK.EventSystem
{
	using UnityEngine;
	using UnityEngine.Events;
	using System.Collections.Generic;

	/// <summary>
	/// Event Listener using UnityEvent as a response.
	/// </summary>
	public class ES_EventListener : MonoBehaviour
	{
		public List<ES_Event_Abstract> eventsToListenFor;

		public UnityEvent responseToDefaultEvent;
		public BoolEvent responseToBoolEvent;
		public IntEvent responseToIntEvent;
		public FloatEvent responseToFloatEvent;
		public StringEvent responseToStringEvent;
		public ObjectEvent responseToObjectEvent;

		#region Unity Methods
		/// <summary>
		/// Unity OnEnable function.
		/// </summary>
		private void OnEnable()
		{
			foreach (ES_Event_Abstract gameEvent in eventsToListenFor)
			{
				gameEvent.AddListener(this);
			}
		}

		/// <summary>
		/// Unity OnDisable function.
		/// </summary>
		private void OnDisable()
		{
			foreach (ES_Event_Abstract gameEvent in eventsToListenFor)
			{
				gameEvent.RemoveListener(this);
			}
		}
		#endregion

		#region Class Methods
		/// <summary>
		/// Calls the UnityEvent reponse when listener receives an event with no value.
		/// </summary>
		public void OnEventRaised()
		{
			if (responseToDefaultEvent != null)
				responseToDefaultEvent.Invoke();
		}

		/// <summary>
		/// Calls the UnityEvent reponse when listener receives an event with a bool value.
		/// </summary>
		public void OnEventRaised(bool value)
		{
			if (responseToBoolEvent != null)
				responseToBoolEvent.Invoke(value);
		}

		/// <summary>
		/// Calls the UnityEvent reponse when listener receives an event with a int value.
		/// </summary>
		public void OnEventRaised(int value)
		{
			if (responseToIntEvent != null)
				responseToIntEvent.Invoke(value);
		}

		/// <summary>
		/// Calls the UnityEvent reponse when listener receives an event with a float value.
		/// </summary>
		public void OnEventRaised(float value)
		{
			if (responseToFloatEvent != null)
				responseToFloatEvent.Invoke(value);
		}

		/// <summary>
		/// Calls the UnityEvent reponse when listener receives an event with a float value.
		/// </summary>
		public void OnEventRaised(string value)
		{
			if (responseToStringEvent != null)
				responseToStringEvent.Invoke(value);
		}

		/// <summary>
		/// Calls the UnityEvent reponse when listener receives an event with a Unity Object value.
		/// </summary>
		public void OnEventRaised(Object value)
		{
			if (responseToObjectEvent != null)
				responseToObjectEvent.Invoke(value);
		}
		#endregion
	}
}