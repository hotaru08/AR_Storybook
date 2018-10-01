namespace ATXK.EventSystem
{
	using UnityEngine;

	/// <summary>
	/// Event based on ScriptableObjects with string value.
	/// </summary>
	[CreateAssetMenu(menuName = "Events/String Event", order = 5)]
	public class ES_Event_String : ES_Event
	{
		public string value;

		public override void Invoke()
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
			{
				eventListeners[i].OnEventRaised(value);
			}
		}
	}
}