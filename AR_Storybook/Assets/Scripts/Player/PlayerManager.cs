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
    [SerializeField] private GameModes m_gameMode;
    public GameModes GetGameMode { get { return m_gameMode; } }

    /// <summary>
    /// Lane Movement of Player
    /// </summary>
    private int m_playerLaneIndex;
    public int m_originalPlayerIndex;
    public int PlayerIndex { set { m_playerLaneIndex = value; } get { return m_playerLaneIndex; } }
    private int m_numLanes; 
    public int NumberOfLanes { set { m_numLanes = value; } }
    public int m_laneStyle;

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

    private bool m_bSpawn; //for particles, to prevent spawning more than once
    public GameObject ParticlePrefab; //Particles
    private GameObject ParticleParent; //parent of particles
	private bool m_bStunned;

    /// <summary>
    /// Events to Send
    /// </summary>
    [Header("Events to be Send")]
    [SerializeField] private ES_Event_Base m_spawnWinScreen;
    [SerializeField] private ES_Event_Base m_spawnLoseScreen;
    [SerializeField] private ES_Event_Int m_nextInstruction;
    //[SerializeField] private ES_Event m_PlayerDamagedEvent;
    //[SerializeField] private ES_Event m_PlayerDiedEvent;

    [Tooltip("Player object to send to Camera for detecting reverse movements")]
    [SerializeField] private ES_Event_UnityObject m_cameraPlayer;

    /// <summary>
    /// Unity Start Function ( initialise variables )
    /// </summary>
    private void Start()
    {
        // Get Components
        m_gameMode = GameObject.FindGameObjectWithTag("BattleScene").GetComponent<GameModes>();
        m_Animator = GetComponent<Animator>();
        m_swipeComponent = GetComponent<Touch_Swipe>();
        ParticleParent = GameObject.FindGameObjectWithTag("ParticlesHolder");

        // Initialising Variables
        m_bSpawn = false;
		m_bStunned = false;
		m_gravity = Physics.gravity.y;
        m_swipeDirection.Value = (int)m_swipeComponent.SwipeDirection;

        if (m_originalPlayerIndex < 0 || m_originalPlayerIndex > m_numLanes - 1)
            m_originalPlayerIndex = m_numLanes / 2;
        else m_originalPlayerIndex = m_playerLaneIndex;

        // Send Player Obj 
        ES_Event_UnityObject temp = m_cameraPlayer as ES_Event_UnityObject;
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
        if(m_stateMachine != null)
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

        // ---------- Player Win / Lose
        if (m_playerHealth.value <= 0)
        {
            m_stateMachine.SetNextState("PlayerLose");
            m_spawnLoseScreen.Invoke();
            //m_PlayerDiedEvent.Invoke();
        }
        else if (m_AIHealth.value <= 0.0f)
        {
            m_stateMachine.SetNextState("PlayerVictory");
            m_spawnWinScreen.Invoke();
        }

        // ---------- Player Movement ( only during Idle state )
        if(!m_bStunned)
		{
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
	/// Sets whether the player is stunned or not.
	/// </summary>
	public void PlayerStunned(bool stunValue)
	{
		m_bStunned = stunValue;
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
                            m_gameMode.GetSpawnerEvent.Invoke(true);
                        }
                    }
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
                            m_nextInstruction.Invoke(m_nextInstruction.Value + 1);
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
                            m_nextInstruction.Invoke(m_nextInstruction.Value + 1);
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

    /// <summary>
    /// Function to select which particle to spawn according to events
    /// </summary>
    private void SpawnParticles()
    {
        if (!m_bSpawn)
        {
            GameObject temp;
            temp = Instantiate(ParticlePrefab, transform.position, Quaternion.identity);
            temp.transform.SetParent(ParticleParent.transform); //set temp as parent
            ParticleSystem.MainModule mainModule = temp.GetComponent<ParticleSystem>().main;
            mainModule.playOnAwake = false;
            mainModule.loop = false;
            Debug.Log("o o f");

            m_bSpawn = true;
        }
    }

    /// <summary>
    /// Reset Player to defualt settings
    /// </summary>
    public void Reset()
    {
        // idle state
        m_stateMachine.SetNextState("Idle");
        // original position
        m_playerLaneIndex = m_originalPlayerIndex;
    }
}