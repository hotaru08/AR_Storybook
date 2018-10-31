namespace TestSpace.EventSystem
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "Events/Float", order = 4)]
	public class ES_Event_Float : ES_Event_Generic<float>
	{
		public float? tempVal;

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