namespace ATXK.DialogueSystem.Examples
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class TestAnim : MonoBehaviour
	{
		int index = 0;

		public void CallAnimator()
		{
			Debug.Log("Index=" + index);
			index++;
		}
	}
}