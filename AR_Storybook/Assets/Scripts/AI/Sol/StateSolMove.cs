using ATXK.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sol's Movement State
/// </summary>
public class StateSolMove : IStateBase
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
    /// Target to move to ( defualt Player )
    /// </summary>
    private GameObject m_targetObject;
    private Vector3 m_directionVector;

    /// <summary>
    /// Movement speed of Sol
    /// </summary>
    public float m_moveSpeed = 3.0f;

    /// <summary>
    /// Constructor to initialise
    /// </summary>
    public StateSolMove(string _name, GameObject _go)
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
        //DebugLogger.Log<StateSolMove>("Entered " + m_stateName + " state");
        m_targetObject = m_object.transform.parent.gameObject;

        // Play Move Animation
    }

    public void UpdateState()
    {
        // Move this Gameobject to Target ( if not at the position )
        if (m_object.transform.position == m_targetObject.transform.position)
        {
            DebugLogger.Log<StateSolMove>("Object has reached target");
            return;
        }

        // Calculate Direction Vector from this GameObject to Target
        m_directionVector = (m_targetObject.transform.position - m_object.transform.position).normalized;

        // Set to LookAt Target
        m_object.transform.LookAt(m_targetObject.transform.position);

        // Move to Target
        //m_object.transform.position = m_targetObject.transform.position;
        m_object.transform.position += new Vector3(m_directionVector.x, 0.0f, m_directionVector.z) * m_moveSpeed * Time.deltaTime;


        //m_object.transform.position += m_object.transform.forward * m_moveSpeed * Time.deltaTime;
        DebugLogger.Log<StateSolMove>("Moving to target : " + m_targetObject);

    }

    public void ExitState()
    {
        DebugLogger.Log<StateSolMove>("Exiting State " + m_stateName);

        // Stop Move Animation
    }
}
