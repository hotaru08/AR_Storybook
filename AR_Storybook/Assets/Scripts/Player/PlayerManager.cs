using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.Helper;
using ATXK.EventSystem;

/// <summary>
/// Player Manager to handle Player eg. Animation, States etc
/// </summary>
public class PlayerManager : SingletonBehaviour<PlayerManager>
{
    /// <summary>
    /// Health of Player
    /// </summary>
    private int m_playerHealth;
    public const int m_playerMaxHealth = 3;
    public int PlayerHealth { get { return m_playerHealth; } }

    /// <summary>
    /// Speed of Player
    /// </summary>
    [SerializeField]
    private float m_playerSpeed;

    /// <summary>
    /// To Trigger Jump of Player
    /// </summary>
    private bool m_bTriggerJump;
    private bool m_bAbleToJump;
    private float m_originalPosY;

    /// <summary>
    /// Events that Player sends
    /// </summary>
    public ES_Event_Object m_eventObjToSend;

    /// <summary>
    /// Unity Start Function ( initialise variables )
    /// </summary>
    private void Start()
    {
        m_playerHealth = m_playerMaxHealth;
        m_bTriggerJump = false;
        m_bAbleToJump = true;
        m_originalPosY = transform.position.y;

        // Send Player object to UI
        m_eventObjToSend.Invoke(gameObject);
    }

    /// <summary>
    /// Unity Update Function
    /// </summary>
    private void Update()
    {
        //if (transform.position.y != m_originalPosY && !m_bTriggerJump)
        //{
        //    m_originalPosY = transform.position.y;
        //    DebugLogger.Log<PlayerManager>("Player Pos: " + transform.position.y);
        //    DebugLogger.Log<PlayerManager>("Player Original Pos: " + m_originalPosY);
        //}

        // Jumping
        if (transform.position.y < m_playerSpeed * 0.15f && m_bTriggerJump)
        {
            transform.position += Vector3.up * Time.deltaTime * m_playerSpeed;
            m_bAbleToJump = false;
        }
        else
        {
            // TODO : Figure out the booleans
            m_bTriggerJump = false;
            m_bAbleToJump = true;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (m_playerHealth <= 0) return;

            m_playerHealth--;
        }
    }

    /// <summary>
    /// Function to respond to events listened by Player's listener
    /// </summary>
    public void EventRaised(bool _value)
    {
        if (m_bAbleToJump)
            m_bTriggerJump = _value;
    }

}
