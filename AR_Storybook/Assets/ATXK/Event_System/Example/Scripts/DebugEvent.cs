namespace ATXK.EventSystem.Examples
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using ATXK.Helper;

	public class DebugEvent : MonoBehaviour
	{
		public void EventReceived()
		{
			DebugLogger.Log<DebugEvent>("Event received" + " " + gameObject.name);
		}

		public void EventReceived(bool value)
		{
			DebugLogger.Log<DebugEvent>("Event received with bool value '" + value + "'" + " " + gameObject.name);
		}

		public void EventReceived(int value)
		{
			DebugLogger.Log<DebugEvent>("Event received with int value '" + value + "'" + " " + gameObject.name);
		}

		public void EventReceived(float value)
		{
			DebugLogger.Log<DebugEvent>("Event received with float value '" + value + "'" + " " + gameObject.name);
		}

		public void EventReceived(string value)
		{
			DebugLogger.Log<DebugEvent>("Event received with string value '" + value + "'" + " " + gameObject.name);
		}
	}
}