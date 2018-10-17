using UnityEngine;
using ATXK.Helper;
using ATXK.EventSystem;
using ATXK.CustomVariables;

/// <summary>
/// Player Manager to handle Player eg. Animation, States etc
/// </summary>
public class PlayerManager : MonoBehaviour
{
    /// <summary>
    /// Health of Player
    /// </summary>
    public CV_Int m_playerHealth;
    public const int m_playerMaxHealth = 3;

    /// <summary>
    /// Health of AI
    /// </summary>
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
    private float m_verticalVelocity;
    private float m_gravity;
    [SerializeField]
    private float m_jumpForce;

    /// <summary>
    /// State Machine to control 
    /// </summary>
    public StateMachine m_stateMachine;

    [HideInInspector]
    public Animator m_Animator;

    /// <summary>
    /// Events to send 
    /// </summary>
    [SerializeField]
    private ES_Event[] m_eventsToSend;

	[SerializeField] ES_Event m_PlayerDamagedEvent;
	[SerializeField] ES_Event m_PlayerDiedEvent;

    /// <summary>
    /// Reversing the controls of Player
    /// </summary>
    private bool m_bReverseControls;
    public bool ReverseControls { get { return m_bReverseControls; } }

    /// <summary>
    /// Various interactions when colliding with projectiles
    /// </summary>
    public enum GAME_MODE
    {
        NONE,
        GOOD_DEALDAMAGE,
        TIMEDAMAGE
    }
    public GAME_MODE m_mode;

    /// <summary>
    /// Unity Start Function ( initialise variables )
    /// </summary>
    private void Start()
    {
        m_playerHealth.value = m_playerMaxHealth;
        m_bTriggerJump = false;
        m_Animator = GetComponent<Animator>();
        m_gravity = Physics.gravity.y;

        // Send Player Obj 
        ES_Event_Object temp = m_eventsToSend[1] as ES_Event_Object;
        temp.Invoke(this.gameObject);

        // Initialising Player States
        m_stateMachine = new StateMachine();
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
        // Player Win / Lose
        if (m_playerHealth.value <= 0)
        {
            if (m_stateMachine.GetCurrentState().Equals("PlayerLose")) return;

            m_stateMachine.SetNextState("PlayerLose");
            // Raise SpawnReload event
            m_eventsToSend[2].Invoke();

			m_PlayerDiedEvent.Invoke();
        }
        else if (m_AIHealth.value <= 0.0f)
        {
            if (m_stateMachine.GetCurrentState().Equals("PlayerVictory")) return;

            m_stateMachine.SetNextState("PlayerVictory");
            // Raise SpawnReload event
            m_eventsToSend[0].Invoke();
        }
        
        m_stateMachine.Update();
    }

    /// <summary>
    /// Update every Physics frame
    /// </summary>
    private void FixedUpdate()
    {
        // Check if player is on ground
        if (IsGrounded())
        {
            // Set velocity to be a small force so that player remains close to the ground
            m_verticalVelocity = m_gravity * Time.deltaTime;

            if (m_bTriggerJump)
            {
                m_bTriggerJump = false;
                // Set the velocity to be jump force ( force that pushes player off ground )
                m_verticalVelocity = m_jumpForce;
            }
            //DebugLogger.LogWarning<PlayerManager>("Velocity Up: " + m_verticalVelocity); // in mobile, force gets reset to default force
            //DebugLogger.LogWarning<PlayerManager>("IsGrounded: + " + IsGrounded());
        }
        else
        {
            m_verticalVelocity += m_gravity * Time.deltaTime;
            DebugLogger.LogWarning<PlayerManager>("Velocity falling: " + m_verticalVelocity);
            DebugLogger.LogWarning<PlayerManager>("IsGrounded: + " + IsGrounded());
        }

        Debug.DrawLine(transform.position, transform.position + new Vector3(0.0f, -0.01f, 0.0f), Color.blue);
        transform.localPosition += new Vector3(0.0f, m_verticalVelocity * Time.deltaTime, 0.0f);
    }

    /// <summary>
    /// Function to respond to events listened by Player's listener
    /// </summary>
    public void EventRaised()
    {
        if (IsGrounded())
            m_bTriggerJump = true;
    }
    public void EventRaised(bool _value)
    {
        m_bReverseControls = _value;
    }

    /// <summary>
    /// Function to check if Player is on the ground
    /// </summary>
    /// <returns>True when player is on ground</returns>
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
            m_Animator.SetTrigger("Damaged");

			m_PlayerDamagedEvent.Invoke();
        }
        else if (_other.tag.Equals("GoodProjectiles"))
        {
            if (m_playerHealth.value >= m_playerMaxHealth) return;

            m_playerHealth.value++;
        }
    }
}