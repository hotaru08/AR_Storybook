using ATXK.ItemSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to change attack animation of germ
/// *** THIS IS VERY HARDCODED 
/// </summary>
public class Germ_AttackAnimation : MonoBehaviour
{
	public void ChangeAttackAnimation(Object _objReceived)
    {
        if (_objReceived.name.Equals("projectile_poop"))
            GetComponent<Animator>().SetTrigger("Shit");
        else
            GetComponent<Animator>().SetTrigger("Attack");
    }
}
