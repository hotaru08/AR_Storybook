using ATXK.CustomVariables;
using ATXK.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Display Player's Health on UI
/// </summary>
public class UI_DisplayPlayerHealth : MonoBehaviour
{
    /// <summary>
    /// Health of Player
    /// </summary>
    //private GameObject m_player;
    [SerializeField]
    private CV_Int m_playerHealth;
    public const int m_playerMaxHealth = 3;
    private int m_prevPlayerHealth;

    /// <summary>
    /// Health to Render on UI
    /// </summary>
    [SerializeField]
    private GameObject m_healthIcon;
    private Stack<GameObject> m_healthIconStack;

    /// <summary>
    /// Variables to position UI
    /// </summary>
    [SerializeField]
    private Vector2 m_startingPos;
    [SerializeField]
    private float m_paddingTop;

    private void Start()
    {
        // Initialise Variables
        m_healthIconStack = new Stack<GameObject>();
        m_prevPlayerHealth = m_playerHealth.value = m_playerMaxHealth;
        
        // Render Player Health on UI
        for (int i = 0; i < m_playerHealth.value; ++i)
        {
            // Create Icon
            GameObject temp = Instantiate(m_healthIcon, transform);

            // Displace Position 
            temp.GetComponent<RectTransform>().anchoredPosition = new Vector2(m_startingPos.x, m_startingPos.y + m_paddingTop * i);

            // Add to Stack
            m_healthIconStack.Push(temp);
        }
    }

    /// <summary>
    /// Unity Update Function
    /// </summary>
    public void Update()
    {
        if (m_playerHealth.value == m_prevPlayerHealth) return;

        // If health is lower, pop from Stack
        if (m_playerHealth.value < m_prevPlayerHealth)
        {
            GameObject tempPop = m_healthIconStack.Pop();
            Destroy(tempPop);
            m_prevPlayerHealth = m_playerHealth.value;
            return;
        }

        // Re-Instantiate Stack if health is higher
        for (int i = 0; i < m_playerHealth.value; ++i)
        {
            // Check if slot in stack is already taken 
            if (i < m_healthIconStack.Count) continue;

            // Create Icon
            GameObject temp = Instantiate(m_healthIcon, transform);

            // Displace Position 
            temp.GetComponent<RectTransform>().anchoredPosition = new Vector2(m_startingPos.x, m_startingPos.y + m_paddingTop * i);

            // Add to Stack
            m_healthIconStack.Push(temp);
        }
        m_prevPlayerHealth = m_playerHealth.value;
    }

    /// <summary>
    /// Reduce the health of player
    /// </summary>
    /// <param name="_value">Value to decrease health</param>
    public void ReducePlayerHealth(int _value)
    {
        if (m_playerHealth.value <= 0) return;
        m_playerHealth.value -= _value;
    }

    /// <summary>
    /// Increase the health of Player
    /// </summary>
    /// <param name="_value"></param>
    public void IncreasePlayerHealth(float _value)
    {
        if (m_playerHealth.value >= m_playerMaxHealth) return;
        m_playerHealth.value += (int)_value;
    }

    /// <summary>
    /// Reset Player Health
    /// </summary>
    public void ResetPlayerHealth()
    {
        m_playerHealth.value = m_playerMaxHealth;
    }
}
