namespace TestSpace
{
	using UnityEngine;

	public abstract class Photobooth_Generic_Switcher<T> : MonoBehaviour
	{
		[SerializeField] protected int currIndex;
		[SerializeField] protected T[] listOfSwitchableObject;

		public int CurrentIndex { get { return currIndex; } }
		public T CurrentObject { get { return listOfSwitchableObject[currIndex]; } }

		public virtual void NextObject()
		{
			currIndex++;
			if (currIndex > listOfSwitchableObject.Length - 1)
				currIndex = 0;
		}

		public virtual void PrevObject()
		{
			currIndex--;
			if (currIndex < 0)
				currIndex = listOfSwitchableObject.Length - 1;
		}

		public virtual void Switch(int index)
		{
			if (index > 0 && index < listOfSwitchableObject.Length - 1)
				currIndex = index;
		}
	}
}