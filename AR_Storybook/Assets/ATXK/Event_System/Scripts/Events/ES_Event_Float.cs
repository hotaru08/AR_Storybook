namespace ATXK.EventSystem
{
	using UnityEngine;

	/// <summary>
	/// Event based on ScriptableObjects with float value.
	/// </summary>
	[CreateAssetMenu(menuName = "Events/Float Event", order = 4)]
	public class ES_Event_Float : ES_Event
	{
		public float value;

		public override void Invoke()
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
			{
				eventListeners[i].OnEventRaised(value);
			}
		}

		public void Invoke(float newValue)
		{
			value = newValue;
			Invoke();
		}
	}
}