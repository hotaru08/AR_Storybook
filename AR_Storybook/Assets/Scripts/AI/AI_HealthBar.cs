﻿using ATXK.CustomVariables;
using ATXK.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AI Health Bar
/// </summary>
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
    [SerializeField]
    private CV_Float m_AIMaxHealth;

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
    public enum DAMAGE_MODE
    {
        NONE,
        DAMAGED_BASED,
        TIME_BASED
    }
    public DAMAGE_MODE m_mode;

    private void Start()
    {
        m_AIMaxHealth.value = m_healthBar.sizeDelta.y;
        m_AIHealth.value = m_AIMaxHealth.value;
    }

    /// <summary>
    /// Unity Update
    /// </summary>
    private void Update()
    {
        // If AI Health is 0, stop healthbar
        if (m_AIHealth.value <= 0.0f)
        {
            m_healthBar.sizeDelta = new Vector2(m_healthBar.sizeDelta.x, 0f);
            return;
        }

        // Based on mode, reduce health accordingly
        switch (m_mode)
        {
            case DAMAGE_MODE.NONE: // For arcade mode
                break;
            case DAMAGE_MODE.DAMAGED_BASED: // Deals Damage to AI
                break;
            case DAMAGE_MODE.TIME_BASED: // Reduce health over time
                m_AIHealth.value -= m_multiplier * Time.deltaTime;
                break;
        }
        
        // Adjusts the Healthbar scale according to AI_Health
        m_healthBar.sizeDelta = new Vector2(m_healthBar.sizeDelta.x, m_AIHealth.value);
    }
    
    public void ReduceEnemyHealth(float _value)
    {
        if (!m_mode.Equals(DAMAGE_MODE.DAMAGED_BASED)) return;
        m_AIHealth.value -= _value;
    }
    public void IncreaseEnemyHealth(float _value)
    {
        m_AIHealth.value += _value;
    }
    public void ResetEnemyHealth()
    {
        m_AIHealth.value = m_AIMaxHealth.value;
        m_healthBar.sizeDelta = new Vector2(m_healthBar.sizeDelta.x, m_AIMaxHealth.value);
    }
}
