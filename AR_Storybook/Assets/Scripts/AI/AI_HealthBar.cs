using ATXK.CustomVariables;
using ATXK.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_HealthBar : MonoBehaviour
{
    /// <summary>
    /// RecTransform of the UI that is going to be healthbar
    /// </summary>
    [SerializeField]
    private RectTransform m_healthBar;

    /// <summary>
    /// AI Health Custom Variable
    /// </summary>
    [SerializeField]
    private CV_Float m_AIHealth;

    /// <summary>
    /// Player Health
    /// </summary>
    [SerializeField]
    private CV_Int m_playerHealth;

    /// <summary>
    /// Speed of decrease
    /// </summary>
    [SerializeField]
    private float m_multiplier = 20.0f;
    
    /// <summary>
    /// Various damage style to AI_Health
    /// </summary>
    public enum GAME_MODE
    {
        NONE,
        DAMAGED_BASED,
        TIME_BASED
    }
    public GAME_MODE m_mode;

    /// <summary>
    /// Variables for reducing AI_Health
    /// </summary>
    private bool m_bCanDealDamaged;
    private float m_fDamageToDeal;

    private void Start()
    {
        m_AIHealth.value = m_healthBar.sizeDelta.y;
        m_bCanDealDamaged = false;
        m_fDamageToDeal = 0.0f;
    }

    /// <summary>
    /// Unity Update
    /// </summary>
    private void Update()
    {
        // If Player Health / AI Health is 0, stop healthbar
        if (m_playerHealth.value <= 0 || m_AIHealth.value <= 0.0f) return;

        // Based on mode, reduce health accordingly
        switch (m_mode)
        {
            case GAME_MODE.NONE: // For arcade mode
                break;
            case GAME_MODE.DAMAGED_BASED: // Deals Damage to AI
                if (!m_bCanDealDamaged) return;

                m_AIHealth.value -= m_fDamageToDeal;
                m_bCanDealDamaged = false;
                break;
            case GAME_MODE.TIME_BASED: // Reduce health over time
                m_AIHealth.value -= m_multiplier * Time.deltaTime;
                break;
        }
        
        // Adjusts the Healthbar scale according to AI_Health
        m_healthBar.sizeDelta = new Vector2(m_healthBar.sizeDelta.x, m_AIHealth.value);
    }

    /// <summary>
    /// Responses to Events 
    /// </summary>
    public void EventReceived()
    {
        m_bCanDealDamaged = true;
    }
    public void EventReceived(float _value)
    {
        m_fDamageToDeal = _value;
    }
}
