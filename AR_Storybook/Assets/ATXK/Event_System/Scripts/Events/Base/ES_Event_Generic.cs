namespace ATXK.EventSystem
{
	using UnityEngine;

	public abstract class ES_Event_Generic<T> : ES_Event_Abstract
	{
		[SerializeField] protected T value;

		public T Value { get { return value; } set { this.value = value; } }

		public abstract override void RaiseEvent();

		public abstract override void RaiseEvent(int? listenerInstanceID);

		public abstract void RaiseEvent(T value);

		public abstract void RaiseEvent(T value, int? listenerInstanceID);
	}
}