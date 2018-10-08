using ATXK.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
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
    private GameObject m_bumperPrefab;
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
    public Vector3 m_transformation;
    private float m_offsetZ, m_offsetX;
    private float m_widthBounds,m_heightBounds;

    /// <summary>
    /// Player's Variables
    /// </summary>
    [Header("Player")]
    [SerializeField]
    private int m_playerIndex;
    private int m_playerPrevIndex;
    private GameObject m_player;
    public bool m_reverseControls;
    private const float m_scaleRatio = 5;

    /// <summary>
    /// Enum for different enemies spawn style
    /// </summary>
    public enum ENEMIES_SPAWN_STYLE
    {
        EACH_LANE,
        W_STYLE,
        RANDOM
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
        // Finding width and height of spawning ground
        m_widthBounds = transform.localScale.x;
        m_heightBounds = transform.localScale.z;

        // Initialise
        m_lanes = new GameObject[m_NumLanes];
        m_playerPrevIndex = m_playerIndex;

        LaneLayout();
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
            temp.transform.localPosition = new Vector3(((temp.transform.localPosition.x + temp.transform.localScale.x) * i) + m_offsetX,
                                                        temp.transform.localPosition.y,
                                                        temp.transform.localPosition.z + m_offsetZ);

            // Set Materials accordingly ( green, red, blue )
            temp.GetComponent<Renderer>().material.color = SetLaneColor(i);
            DebugLogger.Log<LaneGenerator>("Color: " + temp.GetComponent<Renderer>().material.color);

            // Spawn Enemies at the end of each lanes 
            GenerateEnemies(m_style, temp.transform.localPosition, temp.transform.localScale);

            // Store to Array
            m_lanes[i] = temp;

            //DebugLogger.Log<LaneGenerator>("OffsetZ: " + m_offsetZ);
            //DebugLogger.Log<LaneGenerator>("OffsetX: " + m_offsetX);
            //DebugLogger.Log<LaneGenerator>("Pos " + i + " : " + temp.transform.localPosition);
            //DebugLogger.Log<LaneGenerator>("Scale " + i + " : " + temp.transform.localScale);
        }

        // Spawn Player according to its index
        GeneratePlayer();
    }

    /// <summary>
    /// Spawn Player according to which lane user wants
    /// </summary>
    private void GeneratePlayer()
    {
        if (m_playerIndex < 0 || m_playerIndex > m_lanes.Length - 1)
            m_playerIndex = m_NumLanes / 2;

        // Create Player
        m_player = Instantiate(m_playerPrefab, transform, true);
        m_player.AddComponent<Touch_Swipe>();

        // Set scale to be 1:3 ratio ( lane:player )
        m_player.transform.localScale = new Vector3(m_lanes[m_playerIndex].transform.localScale.x * m_scaleRatio,
                                                    m_lanes[m_playerIndex].transform.localScale.x * m_scaleRatio,
                                                    m_lanes[m_playerIndex].transform.localScale.x * m_scaleRatio);

        // Set Player pos according to lane index
        m_player.transform.localPosition = new Vector3(m_lanes[m_playerIndex].transform.position.x,
                                                       m_player.transform.position.y,
                                                       m_lanes[m_playerIndex].transform.position.z - m_lanes[m_playerIndex].transform.localScale.z * 0.45f);
    }

    /// <summary>
    /// Spawn Enemies according to style
    /// </summary>
    private void GenerateEnemies(ENEMIES_SPAWN_STYLE _style, Vector3 _lanePos, Vector3 _laneScale)
    {
        GameObject tempEnemy = Instantiate(m_enemyPrefab, transform.GetChild(1), true);

        // Set scale to be 1:5 ratio ( lane:enemies )
        tempEnemy.transform.localScale = new Vector3(_laneScale.x * m_scaleRatio,
                                                     _laneScale.x * m_scaleRatio,
                                                     _laneScale.x * m_scaleRatio);

        switch (_style)
        {
            case ENEMIES_SPAWN_STYLE.EACH_LANE:
                tempEnemy.transform.localPosition = _lanePos;
                tempEnemy.transform.localPosition = new Vector3(_lanePos.x,
                                                        _lanePos.y,
                                                        _lanePos.z + _laneScale.z * 0.5f);
                break;
            case ENEMIES_SPAWN_STYLE.W_STYLE:
                break;
            case ENEMIES_SPAWN_STYLE.RANDOM:
                break;
        }
    }

    /// <summary>
    /// Setting size of the lanes ( within spawning area )
    /// - May change it to be grid based 
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
        switch(_index % 3)
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

        DebugLogger.Log<LaneGenerator>("Color of Lane " + _index + ": " + tempColor);
        return tempColor; // if smth happens wrong
    }

    private void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        // TODO : Move to Another Script? To handle player movements
        if (m_player.GetComponent<Touch_Swipe>().SwipeDirection.Equals(Touch_Swipe.SWIPE_DIRECTION.LEFT) ||
            Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (m_playerIndex <= 0) return;

            m_playerIndex--;
            DebugLogger.Log<LaneGenerator>("Left Arrow Pressed, Player Index is " + m_playerIndex);
        }
        if (m_player.GetComponent<Touch_Swipe>().SwipeDirection.Equals(Touch_Swipe.SWIPE_DIRECTION.RIGHT) ||
            Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (m_playerIndex >= m_NumLanes - 1) return;

            m_playerIndex++;
            DebugLogger.Log<LaneGenerator>("Right Arrow Pressed, Player Index is " + m_playerIndex);
        }
#elif UNITY_ANDROID || UNITY_IOS
        // TODO : Move to Another Script? To handle player movements
        if (m_player.GetComponent<Touch_Swipe>().SwipeDirection.Equals(Touch_Swipe.SWIPE_DIRECTION.LEFT))
        {
            if (m_playerIndex <= 0) return;

            m_playerIndex--;
            DebugLogger.LogWarning<LaneGenerator>("Left Arrow Pressed, Player Index is " + m_playerIndex);
        }
        if (m_player.GetComponent<Touch_Swipe>().SwipeDirection.Equals(Touch_Swipe.SWIPE_DIRECTION.RIGHT))
        {
            if (m_playerIndex >= m_NumLanes - 1) return;

            m_playerIndex++;
            DebugLogger.LogWarning<LaneGenerator>("Right Arrow Pressed, Player Index is " + m_playerIndex);
        }
#endif

        // Update Player Pos if there is any changes ( not prev index )
        if (m_playerIndex != m_playerPrevIndex)
        {
            m_playerPrevIndex = m_playerIndex;
            m_player.transform.localPosition = new Vector3(m_lanes[m_playerIndex].transform.position.x,
                                                           m_player.transform.position.y,
                                                           m_lanes[m_playerIndex].transform.position.z - m_lanes[m_playerIndex].transform.localScale.z * 0.45f);
            DebugLogger.Log<LaneGenerator>("Prev Index: " + m_playerPrevIndex + " Index: " + m_playerIndex);
        }
    }
}
