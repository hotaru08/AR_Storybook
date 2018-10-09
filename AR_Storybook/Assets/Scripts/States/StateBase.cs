using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base Interface for all state scripts to inherit
/// </summary>
public interface IStateBase
{
    /// <summary>
    /// State ID used to identify states
    /// </summary>
    string GetStateName { get; }

    /// <summary>
    /// EnterState() : Initialising variables, called once when enetering new state
    /// </summary>
    void EnterState();

    /// <summary>
    /// UpdateState() : Called every frame, to update any logic in the current state
    /// </summary>
    void UpdateState();

    /// <summary>
    /// ExitState() : Reset any variables before leaving current state, called once when leaving current state
    /// </summary>
    void ExitState();
}