namespace ATXK.EventSystem
{
	using UnityEngine;

	/// <summary>
	/// Event based on ScriptableObjects with float value.
	/// </summary>
	[CreateAssetMenu(menuName = "Event/Float Event", order = 4)]
	public class ES_Event_Float : ES_Event
	{
		public float value;

		public override void Invoke()
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
			{
				//if (listenerObject == null || listener == listenerObject)
					eventListeners[i].OnEventRaised(value);
			}
		}

		public void Invoke(float newValue, GameObject listener = null)
		{
			value = newValue;
			Invoke();
		}
	}
}