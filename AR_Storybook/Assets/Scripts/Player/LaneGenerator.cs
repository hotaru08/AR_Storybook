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
    /// Player's starting lane index
    /// </summary>
    [Header("Player")]
    [SerializeField]
    private int m_playerIndex;
    private int m_playerPrevIndex;
    private GameObject m_player;

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
        DebugLogger.Log<LaneGenerator>("Width: " + m_widthBounds + " Height: " + m_heightBounds);

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

            // TODO : Spawn bumpers to seperate lanes

            // Spawn Enemies at the end of each lanes 
            GenerateEnemies(m_style, temp.transform.localPosition, temp.transform.localScale);
            

            // Store to Array
            m_lanes[i] = temp;

            DebugLogger.Log<LaneGenerator>("OffsetZ: " + m_offsetZ);
            DebugLogger.Log<LaneGenerator>("OffsetX: " + m_offsetX);
            DebugLogger.Log<LaneGenerator>("Pos " + i + " : " + temp.transform.localPosition);
            DebugLogger.Log<LaneGenerator>("Scale " + i + " : " + temp.transform.localScale);
        }

        GeneratePlayer();
    }

    /// <summary>
    /// Spawn Player according to which lane user wants
    /// </summary>
    private void GeneratePlayer()
    {
        m_player = Instantiate(m_playerPrefab, transform, true);

        try
        {
            // TODO : Set scale to be 1/2 of lane width


            // Set Player pos according to lane index
            m_player.transform.position = m_lanes[m_playerIndex].transform.position;
        }
        catch(Exception ex)
        {
            // Out of Range Exception
            if (ex is IndexOutOfRangeException)
            {
                //if (m_playerIndex > m_lanes.Length)
                //    m_playerIndex = m_NumLanes - 1;
                //else if (m_playerIndex < 0)
                //    m_playerIndex = 0;

                m_playerIndex = m_NumLanes / 2;
                m_player.transform.position = m_lanes[m_playerIndex].transform.position;
                //m_player.transform.position = new Vector3(m_lanes[m_playerIndex].transform.position.x,
                //                                          m_lanes[m_playerIndex].transform.position.y,
                //                                          m_lanes[m_playerIndex].transform.position.z - m_lanes[m_playerIndex].transform.localScale.z * 0.5f);
            }
            else
                DebugLogger.LogError<LaneGenerator>("Exception receieved: " + ex.ToString());
        }
    }

    /// <summary>
    /// Spawn Enemies at the end of each lane 
    /// - Will modify to spawn in any manner player wants ( eg. W, random, rows etc )
    /// </summary>
    private void GenerateEnemies(ENEMIES_SPAWN_STYLE _style, Vector3 _lanePos, Vector3 _laneScale)
    {
        switch (_style)
        {
            case ENEMIES_SPAWN_STYLE.EACH_LANE:
                GameObject tempEnemy = Instantiate(m_enemyPrefab, transform.GetChild(1), true);
                tempEnemy.transform.localPosition = _lanePos;



                DebugLogger.Log<LaneGenerator>("Enemy Pos " + tempEnemy.transform.localPosition);
                break;
            case ENEMIES_SPAWN_STYLE.W_STYLE:
                break;
            case ENEMIES_SPAWN_STYLE.RANDOM:
                break;
        }
    }

    private void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        // TODO : Move to Another Script? To handle player movements
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (m_playerIndex <= 0) return;

            m_playerIndex--;
            DebugLogger.Log<LaneGenerator>("Left Arrow Pressed, Player Index is " + m_playerIndex);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (m_playerIndex >= m_NumLanes - 1) return;

            m_playerIndex++;
            DebugLogger.Log<LaneGenerator>("Right Arrow Pressed, Player Index is " + m_playerIndex);
        }
#elif UNITY_ANDROID || UNITY_IOS
        // TODO : Move to Another Script? To handle player movements
        // Using swipe direction to determine + / -
#endif
        // Update Player Pos if there is any changes ( not prev index )
        if (m_playerIndex != m_playerPrevIndex)
        {
            m_playerPrevIndex = m_playerIndex;
            m_player.transform.position = m_lanes[m_playerIndex].transform.position;
            DebugLogger.Log<LaneGenerator>("Prev Index: " + m_playerPrevIndex + " Index: " + m_playerIndex);
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
}
