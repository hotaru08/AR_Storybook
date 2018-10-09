namespace ATXK.CustomVariables.Example
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class Player : MonoBehaviour
	{
		[SerializeField] CV_Int playerHP;

		public void TakeDamage(int damage)
		{
			Debug.Log("TakeDamage with int = " + damage + " CurrHP = " + playerHP.value);
			playerHP.value -= damage;
			Debug.Log("CurrHP = " + playerHP.value);
		}
	}
}