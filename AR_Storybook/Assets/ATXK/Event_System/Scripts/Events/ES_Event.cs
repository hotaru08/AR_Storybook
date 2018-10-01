namespace ATXK.EventSystem
{
	using System.Collections.Generic;
	using UnityEngine;

	/// <summary>
	/// Event based on ScriptableObjects.
	/// </summary>
	[CreateAssetMenu(menuName = "Events/Default Event", order = 1)]
	public class ES_Event : ScriptableObject
	{
		[SerializeField] protected List<ES_GameEventListener> eventListeners = new List<ES_GameEventListener>();

		#region Class Methods
		public virtual void Invoke()
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
			{
				eventListeners[i].OnEventRaised();
			}
		}

		public virtual void AddListener(ES_GameEventListener listener)
		{
			eventListeners.Add(listener);
		}

		public virtual void RemoveListener(ES_GameEventListener listener)
		{
			eventListeners.Remove(listener);
		}

		public virtual void RemoveAllListeners()
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
			{
				eventListeners.Remove(eventListeners[i]);
			}
			eventListeners.Clear();
		}
		#endregion
	}
}