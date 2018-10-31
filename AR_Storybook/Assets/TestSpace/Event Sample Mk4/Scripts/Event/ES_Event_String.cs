﻿namespace TestSpace.EventSystem
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "Events/String", order = 5)]
	public class ES_Event_String : ES_Event_Generic<string>
	{
		//xd I suck dick
		public override void Invoke(int? listenerID = null)
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
			{
				if (listeners[i].InstanceID == listenerID || listenerID == null)
				{
					listeners[i].OnEventRaised(this);
				}
			}
		}
	}
}