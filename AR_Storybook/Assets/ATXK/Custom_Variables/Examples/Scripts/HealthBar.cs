namespace ATXK.CustomVariables.Example
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class HealthBar : MonoBehaviour
	{
		[SerializeField] RectTransform healthBar;
		[SerializeField] CV_Int playerHealth;

		private void Update()
		{
			healthBar.sizeDelta = new Vector2(playerHealth.RuntimeValue, healthBar.sizeDelta.y);
		}
	}
}