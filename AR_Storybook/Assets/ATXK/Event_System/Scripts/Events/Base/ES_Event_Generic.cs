namespace ATXK.EventSystem
{
	using UnityEngine;

	/// <summary>
	/// Base class for Events that hold data.
	/// </summary>
	/// <typeparam name="T">Type of data that will be represented.</typeparam>
	public abstract class ES_Event_Generic<T> : ES_Event_Abstract
	{
		[SerializeField] protected T value;

		public T Value { get { return value; } set { this.value = value; } }

		/// <summary>
		/// Calls OnEventRaised() on all listeners registered to this event.
		/// </summary>
		public abstract override void RaiseEvent();

		/// <summary>
		/// Calls OnEventRaised() on the registered listener with the same Instance ID.
		/// </summary>
		/// <param name="listenerInstanceID">InstanceID of the gameObject that will receive this event.</param>
		public abstract override void RaiseEvent(int? listenerInstanceID);

		/// <summary>
		/// Calls OnEventRaised() on all listeners registered to this event.
		/// </summary>
		/// <param name="value">New value of this event.</param>
		public abstract void RaiseEvent(T value);

		/// <summary>
		/// Calls OnEventRaised() on the registered listener with the same Instance ID.
		/// </summary>
		/// <param name="value">New value of this event.</param>
		/// <param name="listenerInstanceID">InstanceID of the gameObject that will receive this event.</param>
		public abstract void RaiseEvent(T value, int? listenerInstanceID);
	}
}