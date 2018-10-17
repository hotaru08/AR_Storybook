﻿using ATXK.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerIdle : IStateBase
{
    /// <summary>
    /// Name of this state
    /// </summary>
    private readonly string m_stateName;

    /// <summary>
    /// Reference to gameobject using this state
    /// </summary>
    private readonly GameObject m_object;
    private Animator m_animator;

    /// <summary>
    /// Constructor to create new state
    /// </summary>
    /// <param name="_name">Name of state</param>
    /// <param name="_go">Gameobject linked to this state</param>
    public StatePlayerIdle(string _name, GameObject _go)
    {
        m_stateName = _name;
        m_object = _go;
    }

    public string GetStateName { get { return m_stateName; } }

    public void EnterState()
    {
        DebugLogger.Log<StatePlayerIdle>("Entered " + m_stateName + " state");
        m_animator = m_object.GetComponent<Animator>();
        m_animator.Play("Idle");
    }

    public void ExitState()
    {
        DebugLogger.Log<StateSolMove>("Exiting State " + m_stateName);
    }

    public void UpdateState()
    {
        
    }
}