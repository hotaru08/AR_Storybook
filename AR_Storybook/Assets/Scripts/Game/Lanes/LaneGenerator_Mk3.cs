namespace ARStorybook.LaneItems
{
    using ATXK.AI;
    using ATXK.EventSystem;
    using System.Collections.Generic;
    using UnityEngine;

    public class LaneGenerator_Mk3 : MonoBehaviour
    {
        /// <summary>
        /// Lanes Settings
        /// </summary>
        public enum LANE_LAYOUT
        {
            HORIZONTAL,
            CIRCULAR,
        }
        [Header("Lanes' Settings")]
        [Tooltip("Spawn Style of Lanes")]
        [SerializeField]
        private LANE_LAYOUT m_laneLayout = 0;
        [Tooltip("The GameObject ( with lane script ) that you want to be a lane")]
        [SerializeField]
        private Lane_Mk2 m_lanePrefab;
        [Tooltip("Number of Lanes that will be generated in columns ( max 5 lanes )")]
        [Range(1, 5)]
        [SerializeField]
        private int m_numLanes = 1;

        /// <summary>
        /// Enemies Settings
        /// </summary>
        public enum ENEMIES_SPAWN_STYLE
        {
            EACH_LANE,
            W_STYLE,
        }
        [Header("Enemies Settings")]
        [Tooltip("Spawn Style of Enemies")]
        [SerializeField]
        private ENEMIES_SPAWN_STYLE m_style;
        [Tooltip("The GameObject ( with AIController script ) that you want to be an Enemy")]
        [SerializeField]
        private AI_Controller m_enemyPrefab;

        /// <summary>
        /// Player Settings
        /// </summary>
        [Header("Player Settings")]
        [Tooltip("The GameObject ( with PlayerManager script ) that you want to be a Player")]
        [SerializeField]
        private PlayerManager m_playerPrefab;
        [Tooltip("Starting lane of Player")]
        [Range(0, 4)]
        [SerializeField]
        private int m_playerIndex;

        /// <summary>
        /// Other Settings
        /// </summary>
        [Header("Other Settings")]
        [Tooltip("Multipler Scale for GameObjects in LaneGenerator")]
        [SerializeField]
        private float m_scaleMultiplier;

        /// <summary>
        /// Private Variables
        /// </summary>
        private PlayerManager m_player;
        private float m_widthBounds, m_heightBounds;
        private float m_widthLane;
        private Renderer m_renderer;
        private Vector3 m_startingSpawnPos;
        private int m_numEnemies;

        /// HACK # 0
        [SerializeField]
        private Transform m_spawnPoint;

        /// <summary>
        /// List to store the lanes
        /// </summary>
        private List<Lane_Mk2> m_laneList;

        /// <summary>
        /// Unity Start Function
        /// </summary>
        void Start()
        {
            // ----- Get Components
            m_renderer = GetComponent<Renderer>();
            GetComponent<MeshRenderer>().enabled = false;

            // ----- Get bounds of spawn area
            m_widthBounds = transform.localScale.x;
            m_heightBounds = transform.localScale.z;

            // ----- Initialise 
            m_laneList = new List<Lane_Mk2>();
            m_laneList.Clear();

            // ----- Find width of lanes
            //m_widthLane = m_widthBounds / m_numLanes;
            m_widthLane = 1f / m_numLanes;

            // ----- Calculate the number of enemies
            if (m_style.Equals(ENEMIES_SPAWN_STYLE.W_STYLE))
                m_numEnemies = (m_numLanes * 2) - 1;
            else
                m_numEnemies = m_numLanes;

            // ----- Generate Objects
            GenerateLayout();
            GeneratePlayer();
            GenerateEnemyLayout();
        }

        /// <summary>
        /// Generate Lanes according to lane layout 
        /// </summary>
        private void GenerateLayout()
        {
            switch (m_laneLayout)
            {
                case LANE_LAYOUT.HORIZONTAL:
                    GenerateHorizontalLayout();
                    break;
                case LANE_LAYOUT.CIRCULAR:
                    GenerateCircularLayout();
                    break;
            }
        }

        /// <summary>
        /// Generation of Lanes in a Horizontal Layout
        /// </summary>
        private void GenerateHorizontalLayout()
        {
            // Set Starting Position for generating
            // HACK # 1
            m_startingSpawnPos = new Vector3(((m_spawnPoint.localPosition.x - m_renderer.bounds.extents.x) / m_widthBounds) + m_widthLane * 0.5f,
                //m_spawnPoint.localPosition.x - m_renderer.bounds.extents.x * 0.5f,
                                             0.0f,
                                             0.0f);
            //Debug.Log("Extents: " + m_renderer.bounds.extents.x);

            for (int i = 0; i < m_numLanes; ++i)
            {
                // Create a new Lane
                Lane_Mk2 tempLane = Instantiate(m_lanePrefab, transform);
                // Set Scale of Lane
                tempLane.transform.localScale = new Vector3(m_widthLane, tempLane.transform.localScale.y, 1f);

                // Set Position of Lane
                tempLane.transform.localPosition = m_startingSpawnPos + m_spawnPoint.localPosition; // HACK # 2
                m_startingSpawnPos.x += m_widthLane;
                // Set Forward
                tempLane.transform.forward = -tempLane.transform.forward;
                // Add to list 
                m_laneList.Add(tempLane);
            }
        }

        /// <summary>
        /// Generation of Lanes in a Circular Layout
        /// </summary>
        private void GenerateCircularLayout()
        {
            // Angle in degrees between each lane
            float m_angleBetweenLanes = 360f / m_numLanes;
            // Radius of the circular area in the middle of lanes
            float m_radius = Mathf.Max(m_renderer.bounds.extents.x, m_renderer.bounds.extents.z);

            for (int i = 0; i < m_numLanes; ++i)
            {
                Lane_Mk2 tempLane = Instantiate(m_lanePrefab);
                float m_offsetPos = tempLane.GetComponent<Renderer>().bounds.extents.z;
                //float m_offsetPos = tempLane.GetComponent<Renderer>().bounds.extents.z;
                //Debug.Log("Offset Z: " + tempLane.GetComponent<Renderer>().bounds.extents.z);

                // Scale Lane - S
                //tempLane.transform.localScale = new Vector3(m_widthLane, 1f, 1f);
                // Rotate Lane according to forward of transform - R
                //tempLane.transform.localRotation = transform.rotation;
                tempLane.transform.rotation = transform.rotation;
                // Set position of lane - T
                tempLane.transform.position = m_spawnPoint.position;
                tempLane.transform.position += tempLane.transform.forward * (m_radius + m_offsetPos);
                // Set Forward
                tempLane.transform.forward = -tempLane.transform.forward;
                //tempLane.enemyPrefab = m_enemyPrefab;
                //tempLane.enemyScale = m_scaleMultiplier;
                //tempLane.laneID = i;
                // Rotate transform to face next spawn direction
                transform.Rotate(Vector3.up, m_angleBetweenLanes);
                m_laneList.Add(tempLane);
            }
        }

        /// <summary>
        /// Generation of Player 
        /// </summary>
        private void GeneratePlayer()
        {
            // Create Player object
            m_player = Instantiate(m_playerPrefab, transform.parent, true);

            // Setting Player Variables
            m_player.PlayerIndex = m_playerIndex;
            m_player.NumberOfLanes = m_numLanes;
            m_player.m_laneStyle = (int)m_laneLayout;

            // Set Scale 
            m_player.transform.localScale = new Vector3(m_scaleMultiplier, m_scaleMultiplier, m_scaleMultiplier);
            // Set Position
            m_player.transform.position = m_laneList[m_playerIndex].m_playerSpawnPoint.position;
            // Make Player Look at forward of Lane
            m_player.transform.LookAt(m_laneList[m_playerIndex].m_enemySpawnPoint);
            // Set bool true
            m_laneList[m_playerIndex].ChangeLaneColor(true);
        }

        /// <summary>
        /// Based on style, generate enemies
        /// </summary>
        private void GenerateEnemyLayout()
        {
            // ------ Enemies Layout
            for (int i = 0; i < m_numEnemies; ++i)
            {
                if (m_style.Equals(ENEMIES_SPAWN_STYLE.W_STYLE) && i != 0)
                {
                    // Spawn according to pattern
                    switch (i % 2)
                    {
                        case 0: // even index
                            GenerateEnemy(m_style, m_laneList[i / 2], i);
                            break;
                        case 1: // odd index
                            GenerateEnemy(m_style, m_laneList[(i / 2) + 1], i);
                            break;
                    }
                }
                else
                {
                    // Spawn on every lane
                    GenerateEnemy(m_style, m_laneList[i], i);
                }
            }
        }

        /// <summary>
        /// Generation of Enemies
        /// </summary>
        private void GenerateEnemy(ENEMIES_SPAWN_STYLE _style, Lane_Mk2 _lane, int _index)
        {
            // Create Enemies
            AI_Controller tempEnemy = Instantiate(m_enemyPrefab, transform.parent, true);

            tempEnemy.transform.localScale = new Vector3(m_scaleMultiplier, m_scaleMultiplier, m_scaleMultiplier);
            // Offset Variables ---- HACK #3 KMS yes
            float m_offsetZ = 0.25f * transform.localScale.z;

            // Based on Style, position the enemies accordingly
            tempEnemy.transform.position = _lane.m_enemySpawnPoint.position;
            switch (_style)
            {
                case ENEMIES_SPAWN_STYLE.W_STYLE:
                    switch (m_laneLayout)
                    {
                        case LANE_LAYOUT.HORIZONTAL:
                            switch (_index % 2)
                            {
                                case 0:
                                    break;
                                case 1:
                                    // Add a z offset
                                    tempEnemy.transform.localPosition -= _lane.m_enemySpawnPoint.forward * m_offsetZ;
                                    // Add a x offset
                                    tempEnemy.transform.localPosition += _lane.m_enemySpawnPoint.right * m_widthLane * 0.5f;
                                    Destroy(tempEnemy.transform.Find("ProjectileSpawner").gameObject);
                                    return;
                            }
                            break;
                        case LANE_LAYOUT.CIRCULAR:
                            // generate surrounding in a circle
                            break;
                    }
                    break;
                default:
                    break;
            }
            // Add a z offset
            //tempEnemy.transform.localPosition -= _lane.m_enemySpawnPoint.forward * m_offsetZ;

            // Make Enemy look accordingly to lane layout
            switch (m_laneLayout)
            {
                case LANE_LAYOUT.HORIZONTAL:
                    tempEnemy.transform.forward = m_laneList[0].transform.forward;
                    break;
                case LANE_LAYOUT.CIRCULAR:
                    tempEnemy.transform.LookAt(transform);
                    break;
            }
        }

        private void Update()
        {
            // ---------- Check so that Update only when changing position
            if (m_player.transform.position.x.Equals(m_laneList[m_player.PlayerIndex].m_playerSpawnPoint.position.x))
            {
                return;
            }
            else if (m_player.transform.position.x < m_laneList[m_player.PlayerIndex].m_playerSpawnPoint.position.x + 0.001f &&
                     m_player.transform.position.x > m_laneList[m_player.PlayerIndex].m_playerSpawnPoint.position.x - 0.001f)
            {
                // Set position when first time reach new position
                m_player.transform.position = m_laneList[m_player.PlayerIndex].m_playerSpawnPoint.position;

                // Set player to be on that lane
                m_laneList[m_player.PlayerIndex].ChangeLaneColor(true);
                m_laneList[m_playerIndex].ChangeLaneColor(false);

                // Setting index of Player
                m_playerIndex = m_player.PlayerIndex;
                return;
            }

            // Update Player Pos using Lane Pos 
            m_player.transform.LookAt(m_laneList[m_player.PlayerIndex].m_enemySpawnPoint);
            m_player.transform.position = Vector3.Lerp(m_player.transform.position, m_laneList[m_player.PlayerIndex].m_playerSpawnPoint.position, m_player.m_playerSpeed);
        }
    }
}