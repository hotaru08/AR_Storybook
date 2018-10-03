using ATXK.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sol's Idle State
/// </summary>
public class StateSolIdle : IStateBase
{
    /// <summary>
    /// Name of this state
    /// </summary>
    private readonly string m_stateName;

    /// <summary>
    /// Reference to gameobject using this state
    /// </summary>
    private readonly GameObject m_object;

    /// <summary>
    /// Original Position of ref Gameobject
    /// </summary>
    private Vector3 m_originalPosition;
    private float m_originalOffset;

    /// <summary>
    /// Parent object to hover near it
    /// </summary>
    private GameObject m_parentObject;
    private Vector3 m_offset;

    /// <summary>
    /// Movement speed of Sol
    /// </summary>
    public float m_moveSpeed = 3.0f;

    /// <summary>
    /// Constructor to initialise
    /// </summary>
    public StateSolIdle(string _name, GameObject _go)
    {
        m_stateName = _name;
        m_object = _go;
    }

    public string GetStateName
    {
        get { return m_stateName; }
    }

    public void EnterState()
    {
        //DebugLogger.Log<StateSolIdle>("Entered " + m_stateName + " state");

        // Get Player ( parent )
        m_parentObject = m_object.transform.parent.gameObject;
        
        // Set Original Pos to be near Player ( parent )
        m_offset = new Vector3(0.3f, 0.4f, -0.3f);
        m_originalPosition = m_parentObject.transform.position + m_offset;
        
        DebugLogger.Log<StateSolIdle>("Original Pos: " + m_originalPosition);

        // Play Idle Animation
    }

    public void UpdateState()
    {
        if (m_object.transform.position == m_parentObject.transform.position)
        {
            DebugLogger.Log<StateSolIdle>("At original pos");
            return;
        }

        // Set Target to Original Position and move back
        m_object.transform.LookAt(m_originalPosition);
        m_object.transform.position += new Vector3(m_object.transform.forward.x, 0.0f, m_object.transform.forward.z) * m_moveSpeed * Time.deltaTime;
        
        DebugLogger.Log<StateSolIdle>("Object Pos: " + m_object.transform.position);
    }

    public void ExitState()
    {
        DebugLogger.Log<StateSolMove>("Exiting State " + m_stateName);

        // Stop Idle Animation
    }
}
