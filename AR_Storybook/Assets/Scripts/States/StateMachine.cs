using ATXK.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State Machine that handles States 
/// *** This is a ref to another script done by someone else
/// </summary>
public class StateMachine
{
    // Dictionary storing stateID, which indicates the states
    private Dictionary<string, IStateBase> m_statesDictionary;

    // Current and Next Pending State of the object
    private IStateBase m_currState, m_nextState;

    /// <summary>
    /// Constructor to initialise
    /// </summary>
    public StateMachine()
    {
        m_statesDictionary = new Dictionary<string, IStateBase>();
        m_currState = m_nextState = null;
    }

    /// <summary>
    /// Adds a new state to the Dictionary of states of a Gameobject
    /// </summary>
    /// <param name="_state">New State to be added into dictionary</param>
    public void AddState(IStateBase _state)
    {
        // _state is null
        if (_state == null)
        {
            DebugLogger.LogWarning<StateMachine>("State to be added is null");
            return;
        }
        // _state already in dictionary
        if (m_statesDictionary.ContainsKey(_state.GetStateName))
        {
            DebugLogger.LogWarning<StateMachine>("State has been added before");
            return;
        }
        // if current state is empty, set new state to be current and next state
        if (m_currState == null)
        {
            m_currState = m_nextState = _state;
            DebugLogger.Log<StateMachine>("New state added, current state is now " + _state.GetStateName);
        }

        // Add new states to Dictionary
        m_statesDictionary.Add(_state.GetStateName, _state);
        //DebugLogger.Log<StateMachine>("New state added : " + _state.GetStateName);
    }

    /// <summary>
    /// Set the next state of Gameobject
    /// </summary>
    /// <param name="_stateName">Name of the next state</param>
    public void SetNextState(string _nextStateName)
    {
        // No such state in Dictionary
        if (!m_statesDictionary.ContainsKey(_nextStateName))
        {
            DebugLogger.Log<StateMachine>("There is no such state");
            return;
        }
        // null is entered
        if (_nextStateName == null)
        {
            DebugLogger.Log<StateMachine>("NULL is entered as a name, please enter a proper name");
            return;
        }

        // Set next state
        m_nextState = m_statesDictionary[_nextStateName];
        //DebugLogger.Log<StateMachine>("Next state is set to be " + m_nextState.GetStateName);
    }

    /// <summary>
    /// Get the current State of a GameObject
    /// </summary>
    /// <returns>Name of the current state</returns>
    public string GetCurrentState()
    {
        if (m_currState == null) return "NO STATE";
        return m_currState.GetStateName;
    }

    /// <summary>
    /// This is where the updating of changing in states takes place
    /// </summary>
    public void Update()
    {
        // if next state != current state, change current to be next state
        if (m_nextState != m_currState)
        {
            m_currState.ExitState();
            m_currState = m_nextState;
            m_currState.EnterState();
        }
        // Update the curr state
        m_currState.UpdateState();
        //DebugLogger.Log<StateMachine>("Curr State: " + m_currState + " Next State: " + m_nextState);
    }
}
