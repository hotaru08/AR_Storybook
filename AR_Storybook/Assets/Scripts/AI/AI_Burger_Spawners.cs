using ARStorybook.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Idk what i am doing but this is supposed to
/// Script to handle spawners of the Boss, King Burger the II
/// </summary>
public class AI_Burger_Spawners : MonoBehaviour
{
    /// <summary>
    /// Spawn Spawners at position received through event
    /// </summary>
	public void SpawnSpawners(string _pos)
    {
        Debug.Log("Entered here");
        Vector3 tempPos = Serialization.DeserialiseVector3(_pos);
    }
}
