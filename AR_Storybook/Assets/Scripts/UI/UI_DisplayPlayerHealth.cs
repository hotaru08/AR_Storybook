﻿using ATXK.CustomVariables;
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
    private int m_prevPlayerHealth;

    /// <summary>
    /// Health to Render on UI
    /// </summary>
    [SerializeField]
    private GameObject m_healthIcon;
    private Stack<GameObject> m_healthIconStack;

    [Tooltip("Icon shown at the top of health")]
    [SerializeField] private GameObject m_headerIcon;
    private GameObject m_headerIconObject;

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

        // Render Header Icon once all Health Icon is rendered
        m_headerIconObject = Instantiate(m_headerIcon, transform);
        m_headerIconObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(m_startingPos.x, 
            m_healthIconStack.Peek().GetComponent<RectTransform>().anchoredPosition.y + m_paddingTop + 100);
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
}
