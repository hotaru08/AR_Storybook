namespace ATXK.EventSystem
{
	using System.Collections.Generic;
	using UnityEngine;

	/// <summary>
	/// Event based on ScriptableObjects.
	/// </summary>
	[CreateAssetMenu(menuName = "Event/Default Event", order = 1)]
	public class ES_Event : ScriptableObject
	{
		[SerializeField] protected List<ES_EventListener> eventListeners = new List<ES_EventListener>();
		[SerializeField] protected GameObject listenerObject;

		public virtual void Invoke()
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
			{
				//if(listenerObject == null || listener == listenerObject)
					eventListeners[i].OnEventRaised();
			}
		}

		public virtual void AddListener(ES_EventListener listener)
		{
			eventListeners.Add(listener);
		}

		public virtual void RemoveListener(ES_EventListener listener)
		{
			eventListeners.Remove(listener);
		}

		public virtual void RemoveAllListeners()
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
			{
				eventListeners.Remove(eventListeners[i]);
			}
			eventListeners.Clear();
		}
	}
}