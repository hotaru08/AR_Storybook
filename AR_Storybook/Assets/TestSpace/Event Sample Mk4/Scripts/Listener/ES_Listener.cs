namespace TestSpace.EventSystem
{
	using System.Collections.Generic;
	using UnityEngine;

	public class ES_Listener : MonoBehaviour
	{
		[SerializeField] List<ES_EventAndResponses> eventAndReponses = new List<ES_EventAndResponses>();

		#region Properties
		public int InstanceID { get { return gameObject.GetInstanceID(); } }
		#endregion

		private void OnEnable()
		{
			foreach(ES_EventAndResponses ear in eventAndReponses)
			{
				ear.EventToRespondTo.AddListener(this);
			}
		}

		private void OnDisable()
		{
			foreach (ES_EventAndResponses ear in eventAndReponses)
			{
				ear.EventToRespondTo.RemoveListener(this);
			}
		}

		public void OnEventRaised(ES_Event_Default callingEvent)
		{
			Debug.Log("OnEventRaised().");
			for (int i = eventAndReponses.Count - 1; i >= 0; i--)
			{
				if(eventAndReponses[i].EventToRespondTo == callingEvent)
				{
					Debug.Log("OnEventRaised(). Event matches CallingEvent.");
					eventAndReponses[i].DefaultResponse.Invoke();
				}
			}
		}

		public void OnEventRaised<T>(ES_Event_Generic<T> callingEvent)
		{
			Debug.LogError("OnEventRaised() for Generic Event of type T. This should NEVER be called.");
		}

		public void OnEventRaised(ES_Event_Generic<bool> callingEvent)
		{
			Debug.Log("OnEventRaised(bool).");
			for (int i = eventAndReponses.Count - 1; i >= 0; i--)
			{
				if (eventAndReponses[i].EventToRespondTo == callingEvent)
				{
					Debug.Log("OnEventRaised(bool). Event matches CallingEvent.");
					eventAndReponses[i].BoolResponse.Invoke(callingEvent.Value);
				}
			}
		}

		public void OnEventRaised(ES_Event_Generic<int> callingEvent)
		{
			Debug.Log("OnEventRaised(int).");
			for (int i = eventAndReponses.Count - 1; i >= 0; i--)
			{
				if (eventAndReponses[i].EventToRespondTo == callingEvent)
				{
					Debug.Log("OnEventRaised(int). Event matches CallingEvent.");
					eventAndReponses[i].IntResponse.Invoke(callingEvent.Value);
				}
			}
		}

		public void OnEventRaised(ES_Event_Generic<float> callingEvent)
		{
			Debug.Log("OnEventRaised(float).");
			for (int i = eventAndReponses.Count - 1; i >= 0; i--)
			{
				if (eventAndReponses[i].EventToRespondTo == callingEvent)
				{
					Debug.Log("OnEventRaised(float). Event matches CallingEvent.");
					eventAndReponses[i].FloatResponse.Invoke(callingEvent.Value);
				}
			}
		}

		public void OnEventRaised(ES_Event_Generic<string> callingEvent)
		{
			Debug.Log("OnEventRaised(string).");
			for (int i = eventAndReponses.Count - 1; i >= 0; i--)
			{
				if (eventAndReponses[i].EventToRespondTo == callingEvent)
				{
					Debug.Log("OnEventRaised(string). Event matches CallingEvent.");
					eventAndReponses[i].StringResponse.Invoke(callingEvent.Value);
				}
			}
		}

		public void OnEventRaised(ES_Event_Generic<Vector2> callingEvent)
		{
			Debug.Log("OnEventRaised(Vector2).");
			for (int i = eventAndReponses.Count - 1; i >= 0; i--)
			{
				if (eventAndReponses[i].EventToRespondTo == callingEvent)
				{
					Debug.Log("OnEventRaised(Vector2). Event matches CallingEvent.");
					eventAndReponses[i].Vector2Response.Invoke(callingEvent.Value);
				}
			}
		}

		public void OnEventRaised(ES_Event_Generic<Vector3> callingEvent)
		{
			Debug.Log("OnEventRaised(Vector3).");
			for (int i = eventAndReponses.Count - 1; i >= 0; i--)
			{
				if (eventAndReponses[i].EventToRespondTo == callingEvent)
				{
					Debug.Log("OnEventRaised(Vector3). Event matches CallingEvent.");
					eventAndReponses[i].Vector3Response.Invoke(callingEvent.Value);
				}
			}
		}

		public void OnEventRaised(ES_Event_Generic<Vector4> callingEvent)
		{
			Debug.Log("OnEventRaised(Vector4).");
			for (int i = eventAndReponses.Count - 1; i >= 0; i--)
			{
				if (eventAndReponses[i].EventToRespondTo == callingEvent)
				{
					Debug.Log("OnEventRaised(Vector4). Event matches CallingEvent.");
					eventAndReponses[i].Vector4Response.Invoke(callingEvent.Value);
				}
			}
		}

		public void OnEventRaised(ES_Event_Generic<Quaternion> callingEvent)
		{
			Debug.Log("OnEventRaised(Quaternion).");
			for (int i = eventAndReponses.Count - 1; i >= 0; i--)
			{
				if (eventAndReponses[i].EventToRespondTo == callingEvent)
				{
					Debug.Log("OnEventRaised(Quaternion). Event matches CallingEvent.");
					eventAndReponses[i].QuaternionResponse.Invoke(callingEvent.Value);
				}
			}
		}

		public void OnEventRaised(ES_Event_Generic<Object> callingEvent)
		{
			Debug.Log("OnEventRaised(Object).");
			for (int i = eventAndReponses.Count - 1; i >= 0; i--)
			{
				if (eventAndReponses[i].EventToRespondTo == callingEvent)
				{
					Debug.Log("OnEventRaised(Object). Event matches CallingEvent.");
					eventAndReponses[i].ObjectResponse.Invoke(callingEvent.Value);
				}
			}
		}
	}
}