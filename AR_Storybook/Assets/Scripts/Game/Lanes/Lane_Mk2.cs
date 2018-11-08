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
        
        /* Material to store original settings of lane */
        private Color m_newColor, m_oldColor;
        /* Renderer of lane */
        private Renderer m_renderer;

        private void OnEnable()
        {
            m_conveyorBelt = GetComponent<ConveyorBelt>();

            m_renderer = GetComponent<Renderer>();
            m_oldColor = m_renderer.material.color;
            m_newColor = new Color(1f, 1f, 1f, 1f);
        }

        /// <summary>
        /// Change the Color of Lane when player is standing on the lane
        /// </summary>
        public void ChangeLaneColor(bool _playerOnLane)
        {
            if (_playerOnLane)
            {
                m_renderer.material.color = m_newColor;
            }
            else
            {
                m_renderer.material.color = m_oldColor;
            }
        }
    }
}
