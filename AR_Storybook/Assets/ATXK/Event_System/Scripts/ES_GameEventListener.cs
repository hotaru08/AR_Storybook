namespace ATXK.EventSystem
{
	using UnityEngine;
	using UnityEngine.Events;
	using System;
	using System.Collections.Generic;

	[Serializable] public class BoolEvent : UnityEvent<bool> { }
	[Serializable] public class IntEvent : UnityEvent<int> { }
	[Serializable] public class FloatEvent : UnityEvent<float> { }
	[Serializable] public class StringEvent : UnityEvent<string> { }

	/// <summary>
	/// Event Listener using UnityEvent as a response.
	/// </summary>
	public class ES_GameEventListener : MonoBehaviour
	{
		public List<ES_Event> listeningForGameEvents;

		public UnityEvent defaultResponse;
		public BoolEvent responseToBoolEvent;
		public IntEvent responseToIntEvent;
		public FloatEvent responseToFloatEvent;
		public StringEvent responseToStringEvent;

		#region Unity Methods
		/// <summary>
		/// Unity OnEnable function.
		/// </summary>
		private void OnEnable()
		{
			foreach (ES_Event gameEvent in listeningForGameEvents)
			{
				gameEvent.AddListener(this);
			}
		}

		/// <summary>
		/// Unity OnDisable function.
		/// </summary>
		private void OnDisable()
		{
			foreach (ES_Event gameEvent in listeningForGameEvents)
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
			if (defaultResponse != null)
				defaultResponse.Invoke();
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
		#endregion
	}
}