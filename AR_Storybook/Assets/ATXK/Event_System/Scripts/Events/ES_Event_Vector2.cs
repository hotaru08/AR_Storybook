namespace ATXK.EventSystem
{
	using UnityEngine;

	/// <summary>
	/// Scene-independent event that holds a Vector2 value.
	/// </summary>
	[CreateAssetMenu(menuName = "Event/Vector2", order = 7)]
	public class ES_Event_Vector2 : ES_Event_Generic<Vector2>
	{
		/// <summary>
		/// Calls OnEventRaised() on all listeners registered to this event.
		/// </summary>
		public override void RaiseEvent()
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
			{
				listeners[i].OnEventRaised(value);
			}
		}

		/// <summary>
		/// Calls OnEventRaised() on the registered listener with the same Instance ID.
		/// </summary>
		/// <param name="listenerInstanceID">InstanceID of the gameObject that will receive this event.</param>
		public override void RaiseEvent(int? listenerInstanceID)
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
			{
				if (listeners[i].ObjectInstanceID == listenerInstanceID || listenerInstanceID == null)
				{
					listeners[i].OnEventRaised(value);
				}
			}
		}

		/// <summary>
		/// Calls OnEventRaised() on all listeners registered to this event.
		/// </summary>
		/// <param name="value">New value of this event.</param>
		public override void RaiseEvent(Vector2 value)
		{
            Value = value;
            for (int i = listeners.Count - 1; i >= 0; i--)
			{
				listeners[i].OnEventRaised(value);
			}
		}

		/// <summary>
		/// Calls OnEventRaised() on the registered listener with the same Instance ID.
		/// </summary>
		/// <param name="value">New value of this event.</param>
		/// <param name="listenerInstanceID">InstanceID of the gameObject that will receive this event.</param>
		public override void RaiseEvent(Vector2 value, int? listenerInstanceID)
		{
            Value = value;
            for (int i = listeners.Count - 1; i >= 0; i--)
			{
				if (listeners[i].ObjectInstanceID == listenerInstanceID || listenerInstanceID == null)
				{
					listeners[i].OnEventRaised(value);
				}
			}
		}
	}
}