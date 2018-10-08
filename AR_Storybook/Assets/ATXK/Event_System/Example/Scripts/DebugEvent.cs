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
			DebugLogger.Log<DebugEvent>("Event received");
		}

		public void EventReceived(bool value)
		{
			DebugLogger.Log<DebugEvent>("Event received with bool value '" + value + "'");
		}

		public void EventReceived(int value)
		{
			DebugLogger.Log<DebugEvent>("Event received with int value '" + value + "'");
		}

		public void EventReceived(float value)
		{
			DebugLogger.Log<DebugEvent>("Event received with float value '" + value + "'");
		}

		public void EventReceived(string value)
		{
			DebugLogger.Log<DebugEvent>("Event received with string value '" + value + "'");
		}
	}
}