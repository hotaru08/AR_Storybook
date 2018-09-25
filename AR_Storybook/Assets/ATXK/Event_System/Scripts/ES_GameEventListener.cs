namespace ATXK.EventSystem
{
	using UnityEngine;
	using UnityEngine.Events;

	/// <summary>
	/// Custom event using UnityEvent as a response.
	/// </summary>
	public class ES_GameEventListener : MonoBehaviour
	{
		/// <summary>
		/// Game event to listen for.
		/// </summary>
		public ES_GameEvent gameEvent;
		/// <summary>
		/// Respone to the event when received.
		/// </summary>
		public UnityEvent response;

		/// <summary>
		/// Default Constructor.
		/// </summary>
		public ES_GameEventListener()
		{
			response = new UnityEvent();
		}

		/// <summary>
		/// Unity OnEnable function.
		/// </summary>
		private void OnEnable()
		{
			if (gameEvent != null)
				gameEvent.AddListener(this);
		}

		/// <summary>
		/// Unity OnDisable function.
		/// </summary>
		private void OnDisable()
		{
			if (gameEvent != null)
				gameEvent.RemoveListener(this);
		}

		/// <summary>
		/// Calls the UnityEvent reponse when listener receives an event.
		/// </summary>
		public void OnEventRaised()
		{
			if (response != null)
				response.Invoke();
		}
	}
}