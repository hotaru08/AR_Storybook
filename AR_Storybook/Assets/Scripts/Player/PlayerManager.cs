using UnityEngine;
using ATXK.Helper;
using ATXK.EventSystem;
using ATXK.CustomVariables;
using ATXK.ItemSystem;

/// <summary>
/// Player Manager to handle Player eg. Animation, States etc
/// </summary>
public class PlayerManager : MonoBehaviour
{
    /// <summary>
    /// AI Variables
    /// </summary>
    [Header("Enemies Section")]
    [SerializeField] private CV_Float m_AIHealth;

    /// <summary>
    /// Player Variables
    /// </summary>
    [Header("Player Section")]
    [SerializeField] private CV_Int m_playerHealth;
    [Range(0.0f,1.0f)] public float m_playerSpeed;

    /// <summary>
    /// Lane Movement of Player
    /// </summary>
    private int m_playerLaneIndex;
    public int PlayerIndex { set { m_playerLaneIndex = value; } get { return m_playerLaneIndex; } }
    private int m_numLanes; 
    public int NumberOfLanes { set { m_numLanes = value; } }

    /// <summary>
    /// To Trigger Jump of Player
    /// </summary>
    private bool m_bTriggerJump;
    public bool TriggerJump { set { m_bTriggerJump = value; } }
    private float m_verticalVelocity;
    private float m_gravity;
    [SerializeField] private float m_jumpForce;

    /// <summary>
    /// Reversing the controls of Player
    /// </summary>
    private bool m_bReverseControls;
    public bool ReverseControls { get { return m_bReverseControls; } }

    /// <summary>
    /// State Machine to control 
    /// </summary>
    public StateMachine m_stateMachine;

    /// <summary>
    /// Other Variables
    /// </summary>
    [Header("Other Variables")]
    [SerializeField] private GameObject m_swipeHorzInstruct;
    [SerializeField] private GameObject m_swipeVertInstruct;
    private Animator m_Animator;
    private Touch_Swipe m_swipeComponent;

    [Tooltip("Event to determine the direction of swipe")]
    [SerializeField] private ES_Event_Int m_swipeDirection;
    [Tooltip("Bool to determine if player has chosen Button Mode")]
    [SerializeField] private CV_Bool m_bButtonMode;

    /// <summary>
    /// Events to Send
    /// </summary>
    [Header("Events to be Send")]
    [SerializeField] private ES_Event m_spawnWinScreen;
    [SerializeField] private ES_Event m_spawnLoseScreen;
    [SerializeField] private ES_Event_Int m_nextInstruction;
    [SerializeField] private ES_Event_Bool m_startGame;
    [SerializeField] private ES_Event m_PlayerDamagedEvent;
    [SerializeField] private ES_Event m_PlayerDiedEvent;

    [Tooltip("Player object to send to Camera for detecting reverse movements")]
    [SerializeField] private ES_Event_Object m_cameraPlayer;

    /// <summary>
    /// Unity Start Function ( initialise variables )
    /// </summary>
    private void Start()
    {
        // Initialising Variables
        m_gravity = Physics.gravity.y;
        m_Animator = GetComponent<Animator>();
        m_swipeComponent = GetComponent<Touch_Swipe>();
        m_swipeDirection.value = (int)m_swipeComponent.SwipeDirection;

        // Send Player Obj 
        ES_Event_Object temp = m_cameraPlayer as ES_Event_Object;
        temp.Invoke(this.gameObject);

        // Initialising Player States
        m_stateMachine = new StateMachine();
        m_stateMachine.AddState(new StatePlayerIdle("Idle", gameObject));
        m_stateMachine.AddState(new StatePlayerLose("PlayerLose", gameObject));
        m_stateMachine.AddState(new StatePlayerVictory("PlayerVictory", gameObject));
        m_stateMachine.SetNextState("Idle");
    }

    /// <summary>
    /// Unity Update Function
    /// </summary>
    private void Update()
    {
        /* If there is no swipe component, do not update movement */
        if (m_swipeComponent == null) return;

        // Update State Machine
        m_stateMachine.Update();

        // If player is not idle, dont update any movements
        if (!m_stateMachine.GetCurrentState().Equals("Idle")) return;

        // ---------- Player Damaged 
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Damaged"))
        {
            //m_PlayerDamagedEvent.Invoke();
            return;
        }

        // ---------- Player Win / Lose
        if (m_playerHealth.value <= 0)
        {
            if (m_stateMachine.GetCurrentState().Equals("PlayerLose")) return;

            m_stateMachine.SetNextState("PlayerLose");
            m_spawnLoseScreen.Invoke();
            //m_PlayerDiedEvent.Invoke();
        }
        else if (m_AIHealth.value <= 0.0f)
        {
            if (m_stateMachine.GetCurrentState().Equals("PlayerVictory")) return;

            m_stateMachine.SetNextState("PlayerVictory");
            m_spawnWinScreen.Invoke();
        }

        // ---------- Player Movement
        if (m_bButtonMode.value)
        {
            PlayerMovement((int)m_swipeComponent.SwipeDirection);

        }
        else
        {
            PlayerMovement(m_swipeDirection.value);
            // Reset Swipe Direction to NONE, to prevent continuous updates
            m_swipeDirection.value = 0;
        }
    }

    /// <summary>
    /// Update every Physics frame
    /// </summary>
    private void FixedUpdate()
    {
        // Check if player is on ground
        //if (IsGrounded())
        //{
        //    // Set velocity to be a small force so that player remains close to the ground
        //    m_verticalVelocity = m_gravity * Time.deltaTime;

        //    if (m_bTriggerJump)
        //    {
        //        m_bTriggerJump = false;
        //        // Set the velocity to be jump force ( force that pushes player off ground )
        //        m_verticalVelocity = m_jumpForce;
        //    }
        //    //DebugLogger.LogWarning<PlayerManager>("Velocity Up: " + m_verticalVelocity); // in mobile, force gets reset to default force
        //    //DebugLogger.LogWarning<PlayerManager>("IsGrounded: + " + IsGrounded());
        //}
        //else
        //{
        //    m_verticalVelocity += m_gravity * Time.deltaTime;
        //    DebugLogger.LogWarning<PlayerManager>("Velocity falling: " + m_verticalVelocity);
        //    DebugLogger.LogWarning<PlayerManager>("IsGrounded: + " + IsGrounded());
        //}

        //Debug.DrawLine(transform.position, transform.position + new Vector3(0.0f, -0.01f, 0.0f), Color.blue);
        //transform.localPosition += new Vector3(0.0f, m_verticalVelocity * Time.deltaTime, 0.0f);
    }

    /// <summary>
    /// Player Movements according to swipe direction
    /// </summary>
    /// <param name="_swipeDirection">Direction of swipe</param>
    public void PlayerMovement(int _swipeDirection)
    {
        switch (_swipeDirection)
        {
            case (int)Touch_Swipe.SWIPE_DIRECTION.UP:
                {
                    // Check if instruct index is this instruction, then raise event
                    //if (m_nextInstruction.value < m_swipeVertInstruct.transform.GetSiblingIndex()) return;
                    if (m_nextInstruction.value.Equals(m_swipeVertInstruct.transform.GetSiblingIndex()))
                    {
                        m_startGame.Invoke(true);
                    }
                    m_bTriggerJump = true;
                }
                break;
            case (int)Touch_Swipe.SWIPE_DIRECTION.DOWN:
                break;
            case (int)Touch_Swipe.SWIPE_DIRECTION.LEFT:
                {
                    // Check if instruct index is this instruction, then raise event
                    //if (m_nextInstruction.value < m_swipeHorzInstruct.transform.GetSiblingIndex()) return;
                    if (m_nextInstruction.value.Equals(m_swipeHorzInstruct.transform.GetSiblingIndex()))
                    {
                        m_nextInstruction.Invoke(m_nextInstruction.value + 1);
                    }

                    // Adjust Player index according to camera view
                    if (m_bReverseControls)
                    {
                        if (m_playerLaneIndex >= m_numLanes - 1) return;
                        m_playerLaneIndex++;
                    }
                    else
                    {
                        if (m_playerLaneIndex <= 0) return;
                        m_playerLaneIndex--;
                    }
                }
                break;
            case (int)Touch_Swipe.SWIPE_DIRECTION.RIGHT:
                {
                    // Check if instruct index is this instruction, then raise event
                    //if (m_nextInstruction.value < m_swipeHorzInstruct.transform.GetSiblingIndex()) return;
                    if (m_nextInstruction.value.Equals(m_swipeHorzInstruct.transform.GetSiblingIndex()))
                    {
                        m_nextInstruction.Invoke(m_nextInstruction.value + 1);
                    }

                    // Adjust Player index according to camera view
                    if (m_bReverseControls)
                    {
                        if (m_playerLaneIndex <= 0) return;
                        m_playerLaneIndex--;
                    }
                    else
                    {
                        if (m_playerLaneIndex >= m_numLanes - 1) return;
                        m_playerLaneIndex++;
                    }
                }
                break;
        }
    }

    /// <summary>
    /// Function to respond to events listened by Player's listener
    /// </summary>
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
    /// Set the State of Player
    /// </summary>
    /// <param name="_stateName">Name of State</param>
    public void SetState(string _stateName)
    {
        m_stateMachine.SetNextState(_stateName);
    }
}