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
    private GameObject m_player;
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
        m_healthIconStack = new Stack<GameObject>();
    }

    /// <summary>
    /// Responses to Events Received
    /// </summary>
	public void EventReceived(Object _obj)
    {
        m_player = _obj as GameObject;
        m_prevPlayerHealth = m_player.GetComponent<PlayerManager>().PlayerHealth;

        // Render Player Health on UI ( surely there is a better way )
        for (int i = 0; i < m_player.GetComponent<PlayerManager>().PlayerHealth; ++i)
        {
            // Create Icon
            GameObject temp = Instantiate(m_healthIcon, transform, true);

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
        if (m_player.GetComponent<PlayerManager>().PlayerHealth == m_prevPlayerHealth) return;

        m_prevPlayerHealth = m_player.GetComponent<PlayerManager>().PlayerHealth;
        GameObject tempHeart = m_healthIconStack.Pop();
        Destroy(tempHeart);
    }
}
