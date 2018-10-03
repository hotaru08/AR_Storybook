using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.Helper;

/// <summary>
/// Manager to handle Sol eg. Animation, States etc
/// </summary>
public class SolManager : SingletonBehaviour<SolManager>
{
    /// <summary>
    /// Statemachine to handle the states of Sol
    /// </summary>
    private StateMachine m_stateMachine;

    /// <summary>
    /// Unity Start Function
    /// </summary>
    private void Start()
    {
        // Create Statemachine and Add States
        m_stateMachine = new StateMachine();
        m_stateMachine.AddState(new StateSolMove("SolMove", this.gameObject));
        m_stateMachine.AddState(new StateSolIdle("SolIdle", this.gameObject));

        // Set default state
        m_stateMachine.SetNextState("SolIdle");
        DebugLogger.Log<SolManager>("Current State: " + m_stateMachine.GetCurrentState());
    }

    /// <summary>
    /// Unity Update Function
    /// </summary>
    private void Update()
    {
        // Update StateMachine
        m_stateMachine.Update();
    }

    public void EventReceived(bool _value)
    {
        //DebugLogger.Log<SolManager>("Event received with value " + _value);
        if (_value)
            m_stateMachine.SetNextState("SolMove");
        else
            m_stateMachine.SetNextState("SolIdle");
    }
}
