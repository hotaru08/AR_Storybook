using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.EventSystem;
using ATXK.CustomVariables;

public class Health : MonoBehaviour
{
	[SerializeField] CV_Int health;
	[SerializeField] CV_Int maxHealth;

	public void ModifyHealth(int modifier)
	{
		health.value += modifier;
		health.value = Mathf.Clamp(health.value, 0, maxHealth.value);
	}

	public void ResetHealth()
	{
		health.value = maxHealth.value;
	}

	public bool IsDead()
	{
		return health.value <= 0;
	}
}
