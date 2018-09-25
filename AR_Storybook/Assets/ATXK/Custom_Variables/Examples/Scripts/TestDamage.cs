namespace ATXK.CustomVariables.Example
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class TestDamage : MonoBehaviour
	{
		[SerializeField] CV_Int damage;
		[SerializeField] Player player;

		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.Backspace))
			{
				player.TakeDamage(damage.RuntimeValue);
			}
			if(Input.GetKeyDown(KeyCode.R))
			{
				player.ResetHP();
			}
		}
	}
}