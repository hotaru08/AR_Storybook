namespace ATXK.EventSystem
{
	using UnityEngine;
	using System.Collections.Generic;

	public abstract class ES_Event_Abstract : ScriptableObject
	{
		public List<ES_EventListener> listeners = new List<ES_EventListener>();

		public abstract void Invoke();

		public abstract void Invoke(int? listenerInstanceID = null);

		public abstract void AddListener(ES_EventListener listener);

		public abstract void RemoveListener(ES_EventListener listener);

		public abstract void RemoveAllListeners();
	}
}