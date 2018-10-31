namespace TestSpace.EventSystem
{
	using UnityEngine;

	public abstract class ES_Event_Generic<T> : ES_Event_Base
	{
		//xd I suck dick
		[SerializeField] protected T value;

		#region Properties
		public T Value { get { return value; } set { this.value = value; } }
		#endregion

		public abstract override void Invoke(int? listenerID = null);

		public void Invoke(int? listenerID, T value)
		{
			this.value = value;
			Invoke(listenerID);
		}
	}
}