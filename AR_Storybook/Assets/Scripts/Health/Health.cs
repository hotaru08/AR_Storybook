using UnityEngine;
using ATXK.CustomVariables;

/// <summary>
/// Health based on scene-independent variables.
/// </summary>
public class Health : MonoBehaviour
{
	[SerializeField] CV_Int health;
	[SerializeField] CV_Int maxHealth;

	/// <summary>
	/// Modifies health variable by the given amount, clamped between 0 and the max health value.
	/// </summary>
	/// <param name="modifier"></param>
	public void ModifyHealth(int modifier)
	{
		health.value += modifier;
		health.value = Mathf.Clamp(health.value, 0, maxHealth.value);
	}

	/// <summary>
	/// Resets the current health to the max value.
	/// </summary>
	public void ResetHealth()
	{
		health.value = maxHealth.value;
	}

	/// <summary>
	/// Flag if health is equal or below 0.
	/// </summary>
	public bool IsDead()
	{
		return health.value <= 0;
	}
}
