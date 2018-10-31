namespace TestSpace.EventSystem
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "Events/Bool", order = 2)]
	public class ES_Event_Bool : ES_Event_Generic<bool>
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