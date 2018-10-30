namespace TestSpace.EventSystem
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "Events/Vector 2", order = 6)]
	public class ES_Event_Vector2 : ES_Event_Generic<Vector2>
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