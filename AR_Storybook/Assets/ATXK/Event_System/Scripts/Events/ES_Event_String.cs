namespace ATXK.EventSystem
{
	using UnityEngine;

	/// <summary>
	/// Event based on ScriptableObjects with string value.
	/// </summary>
	[CreateAssetMenu(menuName = "Event/String Event", order = 5)]
	public class ES_Event_String : ES_Event
	{
		public string value;

		public override void Invoke()
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
			{
				//if (listenerObject == null || listener == listenerObject)
					eventListeners[i].OnEventRaised(value);
			}
		}

		public void Invoke(string newValue, GameObject listener = null)
		{
			value = newValue;
			Invoke();
		}
	}
}