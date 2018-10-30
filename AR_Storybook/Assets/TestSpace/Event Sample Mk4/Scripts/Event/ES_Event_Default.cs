namespace TestSpace.EventSystem
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "Events/Default", order = 1)]
	public class ES_Event_Default : ES_Event_Base
	{
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