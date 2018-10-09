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
			playerHP.value -= damage;
		}
	}
}