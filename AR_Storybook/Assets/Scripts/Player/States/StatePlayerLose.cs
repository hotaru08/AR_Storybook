﻿using ATXK.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerLose : IStateBase
{
    /// <summary>
    /// Name of this state
    /// </summary>
    private readonly string m_stateName;

    /// <summary>
    /// Reference to gameobject using this state
    /// </summary>
    private readonly GameObject m_object;
    private PlayerManager m_player;
    private Animator m_animator;

    /// <summary>
    /// Constructor to create new state
    /// </summary>
    /// <param name="_name">Name of state</param>
    /// <param name="_go">Gameobject linked to this state</param>
    public StatePlayerLose(string _name, GameObject _go)
    {
        m_stateName = _name;
        m_object = _go;
    }

    public string GetStateName { get { return m_stateName; } }

    public void EnterState()
    {
        // Set inactive skipping rope
        if (m_object.transform.childCount > 2)
            m_object.transform.GetChild(1).gameObject.SetActive(false);

        // Play Lose Animation
        m_animator = m_object.GetComponent<Animator>();
        m_animator.SetBool("Lose", true);

        // Set Event 
        m_player = m_object.GetComponent<PlayerManager>();
        m_player.GetGameMode.GetSpawnerEvent.RaiseEvent(false);
        m_player.m_timeScaleEvent.RaiseEvent(0.0f);
    }

    public void ExitState()
    {
        //DebugLogger.Log<StateSolMove>("Exiting State " + m_stateName);
        m_animator.SetBool("Lose", false);
    }

    public void UpdateState()
    {
        //if (m_object.GetComponent<PlayerManager>().m_playerHealth.value > 0)
        //    m_animator.SetBool("Lose", false);
    }
}
