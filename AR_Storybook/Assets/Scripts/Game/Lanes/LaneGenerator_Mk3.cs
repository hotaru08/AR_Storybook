namespace ARStorybook.LaneItems
{
    using ATXK.AI;
    using ATXK.EventSystem;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(ES_EventListener))]
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
        [SerializeField] private LANE_LAYOUT m_laneLayout = 0;
        [Tooltip("The GameObject ( with lane script ) that you want to be a lane")]
        [SerializeField] private Lane m_lanePrefab;
        [Tooltip("Number of Lanes that will be generated in columns ( max 5 lanes )")]
        [Range(1, 5)]
        [SerializeField] private int m_numLanes = 1;

        /// <summary>
        /// List to store the lanes
        /// </summary>
        private List<Lane_Mk2> m_laneList;

        /// <summary>
        /// Enemies Settings
        /// </summary>
        public enum ENEMIES_SPAWN_STYLE
        {
            EACH_LANE,
            W_STYLE,
            BOSS
        }
        [Header("Enemies Settings")]
        [Tooltip("Spawn Style of Enemies")]
        [SerializeField] private ENEMIES_SPAWN_STYLE m_style;
        [Tooltip("The GameObject ( with AIController script ) that you want to be an Enemy")]
        [SerializeField] private AI_Controller m_enemyPrefab;

        /// <summary>
        /// Player Settings
        /// </summary>
        [Header("Player Settings")]
        [Tooltip("The GameObject ( with PlayerManager script ) that you want to be a Player")]
        [SerializeField] private PlayerManager m_playerPrefab;
        [Tooltip("Starting lane of Player")]
        [Range(0, 4)]
        [SerializeField] private int m_playerIndex;

        /// <summary>
        /// Other Settings
        /// </summary>
        [Header("Other Settings")]
        [Tooltip("Multipler Scale for GameObjects in LaneGenerator")]
        [SerializeField] private float m_scaleMultiplier;
        [Tooltip("Event to sent all lanes position to Enemies")]
        [SerializeField] private ES_Event_String m_lanePositions;

        /// <summary>
        /// Private Variables
        /// </summary>
        private PlayerManager m_player;
        private float m_widthBounds, m_heightBounds;
        private float m_offsetX, m_scaleOffsetX;


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
