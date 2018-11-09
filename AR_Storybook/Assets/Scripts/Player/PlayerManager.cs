using UnityEngine;
using ATXK.EventSystem;
using ATXK.CustomVariables;

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
    private GameModes m_gameMode;
    public GameModes GetGameMode { get { return m_gameMode; } }

    /// <summary>
    /// Lane Movement of Player
    /// </summary>
    private int m_playerLaneIndex;
    [HideInInspector] public int m_originalPlayerIndex;
    public int PlayerIndex { set { m_playerLaneIndex = value; } get { return m_playerLaneIndex; } }
    private int m_numLanes; 
    public int NumberOfLanes { set { m_numLanes = value; } }
    [HideInInspector] public int m_laneStyle;

    /// <summary>
    /// To Trigger Jump of Player
    /// </summary>
    private Rigidbody m_rigidBody;
    private bool m_bTriggerJump;
    public bool TriggerJump { set { m_bTriggerJump = value; } }
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

    private bool m_bSpawn; //for particles, to prevent spawning more than once
    public GameObject ParticlePrefab; //Particles
    private GameObject ParticleParent; //parent of particles
	private bool m_bStunned;

    /// <summary>
    /// Events to Send
    /// </summary>
    [Header("Events to be Send")]
    [SerializeField] private ES_Event_Abstract m_spawnWinScreen;
    [SerializeField] private ES_Event_Abstract m_spawnLoseScreen;
    [SerializeField] private ES_Event_Int m_nextInstruction;
    //[SerializeField] private ES_Event m_PlayerDamagedEvent;
    //[SerializeField] private ES_Event m_PlayerDiedEvent;

    [Tooltip("Player object to send to Camera for detecting reverse movements")]
    [SerializeField] private ES_Event_Object m_cameraPlayer;

    /// <summary>
    /// Unity Start Function ( initialise variables )
    /// </summary>
    private void Start()
    {
        // Get Components
        m_gameMode = GameObject.FindGameObjectWithTag("BattleScene").GetComponent<GameModes>();
        m_Animator = GetComponent<Animator>();
        m_swipeComponent = GetComponent<Touch_Swipe>();
        m_rigidBody = GetComponent<Rigidbody>();
        ParticleParent = GameObject.FindGameObjectWithTag("ParticlesHolder");

        // Initialising Variables
        m_bSpawn = false;
		m_bStunned = false;
        m_swipeDirection.Value = (int)m_swipeComponent.SwipeDirection;
        m_originalPlayerIndex = m_playerLaneIndex;

        // Send Player Obj 
        ES_Event_Object temp = m_cameraPlayer as ES_Event_Object;
        temp.RaiseEvent(gameObject);

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
        if (m_stateMachine != null)
            m_stateMachine.Update();

        // ---------- Player Damaged 
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Damaged"))
        {
            //m_PlayerDamagedEvent.Invoke();
            SpawnParticles();
            return;
        }
        else
        {
            m_bSpawn = false;
        }
        if (!m_stateMachine.GetCurrentState().Equals("Idle")) return;

        // ---------- Player Win / Lose
        if (m_playerHealth.value <= 0)
        {
            m_stateMachine.SetNextState("PlayerLose");
            m_spawnLoseScreen.RaiseEvent();
            //m_PlayerDiedEvent.Invoke();
        }
        else if (m_AIHealth.value <= 0.0f)
        {
            m_stateMachine.SetNextState("PlayerVictory");
            m_spawnWinScreen.RaiseEvent();
        }

        // ---------- Player Movement ( only during Idle state )
        if (m_bStunned)
        {
            m_swipeDirection.Value = 0;
            m_bTriggerJump = false;
            return;
        }
        if (!m_bButtonMode.value)
        {
            PlayerMovement((int)m_swipeComponent.SwipeDirection);
        }
        else
        {
            PlayerMovement(m_swipeDirection.Value);
            // Reset Swipe Direction to NONE, to prevent continuous updates
            m_swipeDirection.Value = 0;
        }
    }

    /// <summary>
    /// Update every Physics frame
    /// </summary>
    private void FixedUpdate()
    {
        if (m_bTriggerJump)
        {
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            m_bTriggerJump = false;
            Debug.Log("Help: " + IsGrounded() + " / " + m_bTriggerJump + " / ");
        }
    }

	/// <summary>
	/// Sets whether the player is stunned or not.
	/// </summary>
	public void PlayerStunned(bool _stunValue)
	{
		m_bStunned = _stunValue;
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
                    if (m_gameMode.Mode.Equals(GameModes.GAME_MODE.TUTORIAL))
                    {
                        if (m_nextInstruction.Value < m_swipeVertInstruct.transform.GetSiblingIndex()) return;
                        if (m_nextInstruction.Value.Equals(m_swipeVertInstruct.transform.GetSiblingIndex()))
                        {
                            m_gameMode.GetSpawnerEvent.RaiseEvent(true);
                        }
                    }
                    
                    if (IsGrounded())
                        m_bTriggerJump = true;
                }
                break;
            case (int)Touch_Swipe.SWIPE_DIRECTION.DOWN:
                break;
            case (int)Touch_Swipe.SWIPE_DIRECTION.LEFT:
                {
                    // Check if instruct index is this instruction, then raise event
                    if (m_gameMode.Mode.Equals(GameModes.GAME_MODE.TUTORIAL))
                    {
                        if (m_nextInstruction.Value < m_swipeHorzInstruct.transform.GetSiblingIndex()) return;
                        if (m_nextInstruction.Value.Equals(m_swipeHorzInstruct.transform.GetSiblingIndex()))
                        {
                            m_nextInstruction.RaiseEvent(m_nextInstruction.Value + 1);
                        }
                    }
                    // Adjust Player index according to camera view and lane layout
                    if (m_laneStyle.Equals(0))
                    {
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
                    else
                    {
                        if (m_playerLaneIndex <= 0)
                        {
                            m_playerLaneIndex = m_numLanes - 1;
                        }
                        else m_playerLaneIndex--;
                    }
                }
                break;
            case (int)Touch_Swipe.SWIPE_DIRECTION.RIGHT:
                {
                    // Check if instruct index is this instruction, then raise event
                    if (m_gameMode.Mode.Equals(GameModes.GAME_MODE.TUTORIAL))
                    {
                        if (m_nextInstruction.Value < m_swipeHorzInstruct.transform.GetSiblingIndex()) return;
                        if (m_nextInstruction.Value.Equals(m_swipeHorzInstruct.transform.GetSiblingIndex()))
                        {
                            m_nextInstruction.RaiseEvent(m_nextInstruction.Value + 1);
                        }
                    }

                    // Adjust Player index according to camera view and lane layout
                    if (m_laneStyle.Equals(0))
                    {
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
                    else
                    {
                        if (m_playerLaneIndex >= m_numLanes - 1)
                        {
                            m_playerLaneIndex = 0;
                        }
                        else m_playerLaneIndex++;
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
        return Physics.Raycast(transform.position, Vector3.down, 0.03f);
    }

    /// <summary>
    /// Set the State of Player
    /// </summary>
    /// <param name="_stateName">Name of State</param>
    public void SetState(string _stateName)
    {
        m_stateMachine.SetNextState(_stateName);
    }

    /// <summary>
    /// Function to select which particle to spawn according to events
    /// </summary>
    private void SpawnParticles()
    {
        if (!m_bSpawn)
        {
            GameObject temp;
            temp = Instantiate(ParticlePrefab, ParticleParent.transform);
            temp.transform.position = transform.position;
            ParticleSystem.MainModule mainModule = temp.GetComponent<ParticleSystem>().main;
            mainModule.playOnAwake = false;
            mainModule.loop = false;
            m_bSpawn = true;
        }
    }
}