namespace ATXK.EventSystem
{
	using UnityEngine;

	/// <summary>
	/// Event based on ScriptableObjects with bool value.
	/// </summary>
	[CreateAssetMenu(menuName = "Events/Bool Event", order = 2)]
	public class ES_Event_Bool : ES_Event
	{
		public bool value;

		public override void Invoke()
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
			{
				eventListeners[i].OnEventRaised(value);
			}
		}
	}
}