using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.Helper;
using ATXK.EventSystem;
using ATXK.CustomVariables;

/// <summary>
/// Player Manager to handle Player eg. Animation, States etc
/// </summary>
public class PlayerManager : SingletonBehaviour<PlayerManager>
{
    /// <summary>
    /// Health of Player
    /// </summary>
    public CV_Int m_playerHealth;
    public const int m_playerMaxHealth = 3;

    [SerializeField]
    private CV_Float m_AIHealth;

    /// <summary>
    /// Speed of Player
    /// </summary>
    [SerializeField]
    private float m_playerSpeed = 1;

    /// <summary>
    /// To Trigger Jump of Player
    /// </summary>
    private bool m_bTriggerJump;

    /// <summary>
    /// State Machine to control 
    /// </summary>
    public StateMachine m_stateMachine;
    private Animator m_Animator;

    public ES_Event[] m_eventsToSend;

    /// <summary>
    /// Unity Start Function ( initialise variables )
    /// </summary>
    private void Start()
    {
        m_playerHealth.value = m_playerMaxHealth;
        m_bTriggerJump = false;
        m_Animator = GetComponent<Animator>();

        // Initialising Player States
        m_stateMachine = new StateMachine();
        //m_stateMachine.AddState(new StatePlayerDamaged("PlayerDamaged", gameObject));
        m_stateMachine.AddState(new StatePlayerIdle("PlayerIdle", gameObject));
        m_stateMachine.AddState(new StatePlayerLose("PlayerLose", gameObject));
        m_stateMachine.AddState(new StatePlayerVictory("PlayerVictory", gameObject));
        m_stateMachine.SetNextState("PlayerIdle");
    }

    /// <summary>
    /// Unity Update Function
    /// </summary>
    private void Update()
    {
        if (m_playerHealth.value <= 0)
        {
            if (m_stateMachine.GetCurrentState().Equals("PlayerLose")) return;

            m_stateMachine.SetNextState("PlayerLose");
            // Raise SpawnReload event
            m_eventsToSend[0].Invoke();
        }
        else if (m_AIHealth.value <= 0.0f)
        {
            if (m_stateMachine.GetCurrentState().Equals("PlayerVictory")) return;

            m_stateMachine.SetNextState("PlayerVictory");
            // Raise SpawnReload event
            m_eventsToSend[0].Invoke();
        }


        // Jumping : TODO** Use a distance offset instead ( eg. a certain number of steps to jump )
        if (transform.position.y < m_playerSpeed * 0.001f && m_bTriggerJump 
            && m_stateMachine.GetCurrentState().Equals("PlayerIdle"))
        {
            transform.position += Vector3.up * Time.deltaTime * m_playerSpeed;
            //DebugLogger.LogWarning<PlayerManager>("Player Pos: " + transform.position);
        }
        else
        {
            m_bTriggerJump = false;
        }

        m_stateMachine.Update();
        Debug.DrawLine(transform.position, transform.position + new Vector3(0.0f, -0.01f, 0.0f), Color.blue);
    }

    /// <summary>
    /// Function to respond to events listened by Player's listener
    /// </summary>
    public void EventRaised(bool _value)
    {
        DebugLogger.Log<PlayerManager>("IsGrounded(): " + IsGrounded());
        if (IsGrounded())
            m_bTriggerJump = _value;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.01f);
    }

    /// <summary>
    /// Upon entering collider of this gameObject, do something
    /// </summary>
    private void OnTriggerEnter(Collider _other)
    {
        if (m_stateMachine.GetCurrentState() != "PlayerIdle") return;

        if (_other.tag.Equals("BadProjectiles"))
        {
            if (m_playerHealth.value <= 0) return;
            m_playerHealth.value--;
            m_Animator.SetTrigger("Damaged"); // state will still be idle
        }
        else if (_other.tag.Equals("GoodProjectiles"))
        {
            if (m_playerHealth.value >= m_playerMaxHealth) return;

            m_playerHealth.value++;
        }
        DebugLogger.Log<PlayerManager>("Health: " + m_playerHealth.value);
    }

}
