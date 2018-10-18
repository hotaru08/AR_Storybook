using ATXK.EventSystem;
using ATXK.Helper;
using UnityEngine;

/// <summary>
/// Generates the Gameplay Lanes accordingly
/// </summary>
public class LaneGenerator : MonoBehaviour
{
    /// <summary>
    /// The prefabs that are going to be generated
    /// </summary>
    [Header("Prefabs")]
    [SerializeField]
    private GameObject m_gridPrefab;
    [SerializeField]
    private GameObject m_playerPrefab;
    [SerializeField]
    private GameObject m_enemyPrefab;

    /// <summary>
    /// Variables to let user set their own style of lanes 
    /// </summary>
    [Header("Style")]
    [SerializeField]
    private int m_NumLanes;
    private float m_offsetZ, m_offsetX;
    private float m_widthBounds, m_heightBounds;

    /// <summary>
    /// Player's Variables
    /// </summary>
    [Header("Player")]
    [SerializeField]
    private int m_playerIndex;
    //private int m_playerPrevIndex;
    private const float m_scaleRatio = 5;
    private PlayerManager m_player;

    /// <summary>
    /// Enum for different enemies spawn style
    /// </summary>
    public enum ENEMIES_SPAWN_STYLE
    {
        EACH_LANE,
        W_STYLE,
        BOSS
    }
    [Header("Enemies")]
    public ENEMIES_SPAWN_STYLE m_style;

    /// <summary>
    /// Store lanes generated into array
    /// </summary>
    private GameObject[] m_lanes;

    /// <summary>
    /// Unity Start Function ( change it to init )
    /// </summary>
    private void Start()
    {
        // ----- Finding width and height of spawning ground
        m_widthBounds = transform.GetChild(2).localScale.x;
        m_heightBounds = transform.GetChild(2).localScale.z;

        // ----- Initialise
        m_lanes = new GameObject[m_NumLanes];
        //m_playerPrevIndex = m_playerIndex;

        // ----- Lane Layout
        LaneLayout();
        // ----- Enemies Layout
        EnemiesLayout();
    }

    /// <summary>
    /// Layout of the lanes according to user perferences
    /// </summary>
    private void LaneLayout()
    {
        for (int i = 0; i < m_NumLanes; ++i)
        {
            // Create lanes accordingly to parent transform
            GameObject temp = Instantiate(m_gridPrefab, transform.GetChild(0));

            // Find Z Displacement to displace to set top
            m_offsetZ = temp.transform.localPosition.z + temp.transform.localScale.z * 0.5f;

            // Set Size of lanes
            temp.transform.localScale = SetSizeOfLanes(temp.transform.localScale);

            // Find X Displacement to displace to set right
            m_offsetX = temp.transform.localPosition.x + temp.transform.localScale.x * 0.5f;

            // Spawn lanes side by side
            temp.transform.localPosition = new Vector3((temp.transform.localPosition.x + temp.transform.localScale.x * i) + m_offsetX,
                                                        temp.transform.localPosition.y,
                                                        temp.transform.localPosition.z + m_offsetZ);

            // Set Lane Materials accordingly ( green, red, blue )
            temp.GetComponent<Renderer>().material.color = SetLaneColor(i);

            // Store to Lane Array
            m_lanes[i] = temp;
        }
        GeneratePlayer();
    }

    /// <summary>
    /// Layout of the enemies according to user perferences
    /// </summary>
    private void EnemiesLayout()
    {
        // ----- Get Number of Enemies
        int m_NumEnemies;
        if (m_style.Equals(ENEMIES_SPAWN_STYLE.W_STYLE))
            m_NumEnemies = (m_NumLanes * 2) - 1;
        else
            m_NumEnemies = m_NumLanes;

        // ------ Enemies Layout
        for (int i = 0; i < m_NumEnemies; ++i)
        {
            if (m_style.Equals(ENEMIES_SPAWN_STYLE.W_STYLE) && i != 0)
            {
                // Spawn according to pattern
                switch (i % 2)
                {
                    case 0: // even index
                        GenerateEnemies(m_style, m_lanes[i / 2].transform.localPosition, m_lanes[i / 2].transform.localScale, i, m_lanes[i / 2]);
                        break;
                    case 1: // odd index
                        GenerateEnemies(m_style, m_lanes[(i / 2) + 1].transform.localPosition, m_lanes[(i / 2) + 1].transform.localScale, i, m_lanes[(i / 2) + 1]);
                        break;
                }
            }
            else
            {
                // Spawn only once for BOSS
                if (m_style.Equals(ENEMIES_SPAWN_STYLE.BOSS) && i != m_NumLanes / 2) continue;

                GenerateEnemies(m_style, m_lanes[i].transform.localPosition, m_lanes[i].transform.localScale, i, m_lanes[i]);
            }
        }
    }

    /// <summary>
    /// Spawn Player according to which lane user wants
    /// </summary>
    private void GeneratePlayer()
    {
        // Out of Array Index, set to middle lane
        if (m_playerIndex < 0 || m_playerIndex > m_lanes.Length - 1)
            m_playerIndex = m_NumLanes / 2;

        // Create Player and set accordingly
        m_player = Instantiate(m_playerPrefab, transform.GetChild(0), true).GetComponent<PlayerManager>();
        m_player.PlayerIndex = m_playerIndex;
        m_player.NumberOfLanes = m_NumLanes;
        m_player.gameObject.AddComponent<Touch_Swipe>();

        // Set scale to be 1:5 ratio ( lane:player )
        m_player.transform.localScale = new Vector3(m_lanes[m_player.PlayerIndex].transform.localScale.x * m_scaleRatio * 2,
                                                    m_lanes[m_player.PlayerIndex].transform.localScale.x * m_scaleRatio * 2,
                                                    m_lanes[m_player.PlayerIndex].transform.localScale.x * m_scaleRatio * 2);

        // Set Player pos according to lane index, in their local space
        m_player.transform.localPosition = new Vector3(m_lanes[m_player.PlayerIndex].transform.localPosition.x,
                                                       m_lanes[m_player.PlayerIndex].transform.localPosition.y,
                                                       (m_lanes[m_player.PlayerIndex].transform.localPosition.z - m_lanes[m_player.PlayerIndex].transform.localScale.z * 0.45f));

        // Get the lane object that it is spawned with, and get its targetpoint for player
        m_player.transform.forward = m_lanes[m_player.PlayerIndex].transform.forward;
    }

    /// <summary>
    /// Spawn Enemies according to style
    /// </summary>
    private void GenerateEnemies(ENEMIES_SPAWN_STYLE _style, Vector3 _lanePos, Vector3 _laneScale, int _index, GameObject _laneObj)
    {
        // Create Enemies
        GameObject tempEnemy = Instantiate(m_enemyPrefab, transform.GetChild(1), true);

        // Set scale to be 1:5 ratio ( lane:enemies )
        tempEnemy.transform.localScale = new Vector3(_laneScale.x * m_scaleRatio * 8,
                                                     _laneScale.x * m_scaleRatio * 8,
                                                     _laneScale.x * m_scaleRatio * 8);

        // Get the lane object that it is spawned with, and get its targetpoint for enemies
        tempEnemy.transform.forward = -(_laneObj.transform.forward);

        // Based on Style, position the enemies accordingly
        switch (_style)
        {
            case ENEMIES_SPAWN_STYLE.EACH_LANE:
                tempEnemy.transform.localPosition = new Vector3(_lanePos.x, _lanePos.y, _lanePos.z + _laneScale.z * 0.6f);
                break;
            case ENEMIES_SPAWN_STYLE.W_STYLE:
                if (_index.Equals(0))
                {
                    tempEnemy.transform.localPosition = new Vector3(_lanePos.x, _lanePos.y, _lanePos.z + _laneScale.z * 0.6f);
                    break;
                }

                switch (_index % 2)
                {
                    case 0:
                        tempEnemy.transform.localPosition = new Vector3(_lanePos.x, _lanePos.y, _lanePos.z + _laneScale.z * 0.6f);
                        break;
                    case 1:
                        tempEnemy.transform.localPosition = new Vector3(_lanePos.x - _laneScale.x * 0.5f, _lanePos.y, _lanePos.z + _laneScale.z * 0.8f);
                        Destroy(tempEnemy.transform.Find("Item_Spawner").gameObject); // really need to find better way to do this
                        break;
                }

                break;
            case ENEMIES_SPAWN_STYLE.BOSS:
                switch (m_NumLanes % 2)
                {
                    case 0: // in between lanes
                        tempEnemy.transform.localPosition = new Vector3(_lanePos.x - _laneScale.x * 0.5f, _lanePos.y, _lanePos.z + _laneScale.z * 0.6f);
                        break;
                    case 1: // at lanes
                        tempEnemy.transform.localPosition = new Vector3(_lanePos.x, _lanePos.y, _lanePos.z + _laneScale.z * 0.6f);
                        break;
                }
                break;
        }
    }

    /// <summary>
    /// Setting size of the lanes ( within spawning area )
    /// </summary>
    private Vector3 SetSizeOfLanes(Vector3 _scale)
    {
        _scale.z = m_heightBounds;
        _scale.x = m_widthBounds / m_NumLanes;

        return _scale;
    }

    /// <summary>
    /// Setting the materials ( colors ) of the lanes
    /// </summary>
    /// <param name="_index">Index of the lanes</param>
    /// <returns></returns>
    private Color SetLaneColor(int _index)
    {
        Color tempColor = Color.red;
        switch (_index % 3)
        {
            case 0: // green
                tempColor = new Color(0.16f, 0.68f, 0.198f);
                break;
            case 1: // red
                tempColor = new Color(0.7f, 0.16f, 0.16f);
                break;
            case 2: // blue
                tempColor = new Color(0.21f, 0.220f, 0.9f);
                break;
        }
        return tempColor; // if smth happens wrong
    }

    private void Update()
    {
        // ---------- Check so that Update only when changing position
        if (m_player.transform.localPosition.x.Equals(m_lanes[m_player.PlayerIndex].transform.localPosition.x))
        {
            return;
        }
        else if (m_player.transform.localPosition.x < m_lanes[m_player.PlayerIndex].transform.localPosition.x + 0.001f &&
                 m_player.transform.localPosition.x > m_lanes[m_player.PlayerIndex].transform.localPosition.x - 0.001f)
        {
            // Set position when first time reach new position
            m_player.transform.localPosition = new Vector3(m_lanes[m_player.PlayerIndex].transform.localPosition.x,
                                                           m_player.transform.localPosition.y,
                                                           m_lanes[m_player.PlayerIndex].transform.localPosition.z - m_lanes[m_player.PlayerIndex].transform.localScale.z * 0.45f);
            // Setting index of Player
            m_playerIndex = m_player.PlayerIndex;
        }

        // Update Player Pos using Lane Pos 
        m_player.transform.localPosition = new Vector3(Vector3.Lerp(m_player.transform.localPosition, m_lanes[m_player.PlayerIndex].transform.localPosition, 0.1f).x,
                                                       m_player.transform.localPosition.y,
                                                       m_lanes[m_player.PlayerIndex].transform.localPosition.z - m_lanes[m_player.PlayerIndex].transform.localScale.z * 0.45f);
    }
}