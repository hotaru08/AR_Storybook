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

        private void Start()
        {
            m_conveyorBelt = GetComponent<ConveyorBelt>();
        }
    }
}
