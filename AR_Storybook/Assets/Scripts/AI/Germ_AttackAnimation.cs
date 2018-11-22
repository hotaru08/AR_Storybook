using ATXK.ItemSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to change attack animation of germ
/// </summary>
public class Germ_AttackAnimation : MonoBehaviour
{
	public void ChangeAttackAnimation(Object _objReceived)
    {
        if (_objReceived.name.Equals("projectile_long_shit"))
            GetComponent<Animator>().SetTrigger("Shit");
        else
            GetComponent<Animator>().SetTrigger("Attack");
    }
}
