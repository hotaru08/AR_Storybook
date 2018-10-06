namespace ATXK.EventSystem
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "Event System/ScriptableObject Event", order = 6)]
	public class ES_Event_ScriptableObject : ES_Event
	{
		public ScriptableObject value;

		public override void Invoke()
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
			{
				eventListeners[i].OnEventRaised(value);
			}
		}

		public void Invoke(ScriptableObject newValue)
		{
			value = newValue;
			Invoke();
		}
	}
}