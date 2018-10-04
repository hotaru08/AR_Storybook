namespace ATXK.EventSystem
{
	using UnityEngine;

	/// <summary>
	/// Event based on ScriptableObjects with int value.
	/// </summary>
	[CreateAssetMenu(menuName = "Event System/Event/Int Event", order = 3)]
	public class ES_Event_Int : ES_Event
	{
		public int value;

		public override void Invoke()
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
			{
				eventListeners[i].OnEventRaised(value);
			}
		}

		public void Invoke(int newValue)
		{
			value = newValue;
			Invoke();
		}
	}
}