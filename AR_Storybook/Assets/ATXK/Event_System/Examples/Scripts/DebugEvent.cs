namespace ATXK.EventSystem.Examples
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using ATXK.Helper;

	public class DebugEvent : MonoBehaviour
	{
		string eventName;

		private void Start()
		{
			eventName = GetComponent<ES_GameEventListener>().gameEvent.name;
		}

		public void EventReceived()
		{
			DebugLogger.Log<DebugEvent>("Event received from: " + eventName);
		}
	}
}