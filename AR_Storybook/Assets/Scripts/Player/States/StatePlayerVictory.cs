using ATXK.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerVictory : IStateBase
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
    public StatePlayerVictory(string _name, GameObject _go)
    {
        m_stateName = _name;
        m_object = _go;
    }

    public string GetStateName { get { return m_stateName; } }

    public void EnterState()
    {
        //DebugLogger.Log<StatePlayerVictory>("Entered " + m_stateName + " state");

        // Play Victory Animation
        m_animator = m_object.GetComponent<Animator>();
        m_animator.SetBool("Victory", true);

        // Set Event 
        m_player = m_object.GetComponent<PlayerManager>();
        m_player.GetGameMode.GetSpawnerEvent.RaiseEvent(false);
        m_player.m_timeScaleEvent.RaiseEvent(0.0f);

        // Set inactive skipping rope
        if (m_object.transform.childCount > 2)
            m_object.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void ExitState()
    {
        //DebugLogger.Log<StateSolMove>("Exiting State " + m_stateName);
        m_animator.SetBool("Victory", false);
    }

    public void UpdateState()
    {

    }
}
