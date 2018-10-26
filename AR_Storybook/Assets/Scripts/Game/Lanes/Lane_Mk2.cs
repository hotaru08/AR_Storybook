namespace ARStorybook.LaneItems
{
    using ATXK.AI;
    using UnityEngine;

    public class Lane_Mk2 : MonoBehaviour
    {
        [Tooltip("ConveyerBelt to move items along it")]
        public ConveyorBelt m_conveyorBelt;

        [Header("Position of Spawn Points")]
        public Transform m_playerSpawnPoint;
        public Transform m_enemySpawnPoint;
        public int laneID;
        public AI_Controller enemyPrefab;
        public GameObject enemy;
        public float enemyScale;

        private bool m_bisPlayerOnLane;
        public bool PlayerOnLane { set { m_bisPlayerOnLane = value; } get { return m_bisPlayerOnLane; } }

        private void Start()
        {
            m_conveyorBelt = GetComponent<ConveyorBelt>();
            m_bisPlayerOnLane = false;
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            enemy = Instantiate(enemyPrefab).gameObject;
            enemy.transform.position = m_enemySpawnPoint.position;
            enemy.transform.localScale = new Vector3(enemyScale, enemyScale, enemyScale);
            enemy.transform.LookAt(m_playerSpawnPoint);
        }
    }
}
