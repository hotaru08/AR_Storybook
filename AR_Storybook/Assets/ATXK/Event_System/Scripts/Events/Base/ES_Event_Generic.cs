namespace ATXK.EventSystem
{
	using UnityEngine;

	public abstract class ES_Event_Generic<T> : ES_Event_Base
	{
		[SerializeField] protected T value;

		public T Value { get { return value; } set { this.value = value; } }

		public abstract override void Invoke();

		public abstract override void Invoke(int? listenerInstanceID = null);

		public abstract void Invoke(T value, int? listenerInstanceID = null);
	}
}