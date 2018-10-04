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
		public List<ES_Event> eventsToListenFor;

		public UnityEvent responseToDefault;
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
			foreach (ES_Event gameEvent in eventsToListenFor)
			{
				gameEvent.AddListener(this);
			}
		}

		/// <summary>
		/// Unity OnDisable function.
		/// </summary>
		private void OnDisable()
		{
			foreach (ES_Event gameEvent in eventsToListenFor)
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
			if (responseToDefault != null)
				responseToDefault.Invoke();
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
		/// Calls the UnityEvent reponse when listener receives an event with a ScriptableObject value.
		/// </summary>
		public void OnEventRaised(ScriptableObject value)
		{
			if (responseToObjectEvent != null)
				responseToObjectEvent.Invoke(value);
		}
		#endregion
	}
}