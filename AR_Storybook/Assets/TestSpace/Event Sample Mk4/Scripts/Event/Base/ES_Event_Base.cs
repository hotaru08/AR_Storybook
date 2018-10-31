namespace TestSpace.EventSystem
{
	using System.Collections.Generic;
	using UnityEngine;

	public abstract class ES_Event_Base : ScriptableObject
	{
		//xd I suck dick
		[SerializeField] protected List<ES_Listener> listeners = new List<ES_Listener>();

		public void AddListener(ES_Listener listener)
		{
			if (!listeners.Contains(listener))
				listeners.Add(listener);
		}

		public void RemoveListener(ES_Listener listener)
		{
			if (listeners.Contains(listener))
				listeners.Remove(listener);
		}

		public abstract void Invoke(int? listenerID = null);
	}
}