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
		public ES_Event_Abstract listeningToEvent;

		public UnityEvent defaultResponse;
		public BoolEvent boolResponse;
		public IntEvent intResponse;
		public FloatEvent floatResponse;
		public StringEvent stringResponse;
		public ObjectEvent objectResponse;
		public Vector2Event vector2Response;
		public Vector3Event vector3Response;
		public Vector4Event vector4Response;
		public QuaternionEvent quaternionResponse;

		#region Properties
		public int ObjectInstanceID { get { return gameObject.GetInstanceID(); } }
		public ES_Event_Abstract ListeningToEvent { get { return listeningToEvent; } }
		#endregion

		#region Unity Methods
		/// <summary>
		/// Unity OnEnable function.
		/// </summary>
		private void OnEnable()
		{
			if(listeningToEvent != null)
				listeningToEvent.AddListener(this);
		}

		/// <summary>
		/// Unity OnDisable function.
		/// </summary>
		private void OnDisable()
		{
			if (listeningToEvent != null)
				listeningToEvent.RemoveListener(this);
		}
		#endregion

		#region Class Methods
		/// <summary>
		/// Calls the UnityEvent reponse when listener receives an event with no value.
		/// </summary>
		public void OnEventRaised()
		{
			defaultResponse.Invoke();
		}

		/// <summary>
		/// Calls the UnityEvent reponse when listener receives an event with a bool value.
		/// </summary>
		public void OnEventRaised(bool value)
		{
			boolResponse.Invoke(value);
		}

		/// <summary>
		/// Calls the UnityEvent reponse when listener receives an event with a int value.
		/// </summary>
		public void OnEventRaised(int value)
		{
			intResponse.Invoke(value);
		}

		/// <summary>
		/// Calls the UnityEvent reponse when listener receives an event with a float value.
		/// </summary>
		public void OnEventRaised(float value)
		{
			floatResponse.Invoke(value);
		}

		/// <summary>
		/// Calls the UnityEvent reponse when listener receives an event with a string value.
		/// </summary>
		public void OnEventRaised(string value)
		{
			stringResponse.Invoke(value);
		}

		/// <summary>
		/// Calls the UnityEvent reponse when listener receives an event with a Unity Object value.
		/// </summary>
		public void OnEventRaised(Object value)
		{
			objectResponse.Invoke(value);
		}

		/// <summary>
		/// Calls the UnityEvent reponse when listener receives an event with a Unity Vector2 value.
		/// </summary>
		public void OnEventRaised(Vector2 value)
		{
			vector2Response.Invoke(value);
		}

		/// <summary>
		/// Calls the UnityEvent reponse when listener receives an event with a Unity Vector3 value.
		/// </summary>
		public void OnEventRaised(Vector3 value)
		{
			vector3Response.Invoke(value);
		}

		/// <summary>
		/// Calls the UnityEvent reponse when listener receives an event with a Unity Vector4 value.
		/// </summary>
		public void OnEventRaised(Vector4 value)
		{
			vector4Response.Invoke(value);
		}

		/// <summary>
		/// Calls the UnityEvent reponse when listener receives an event with a Unity Quaternion value.
		/// </summary>
		public void OnEventRaised(Quaternion value)
		{
			quaternionResponse.Invoke(value);
		}
		#endregion
	}
}