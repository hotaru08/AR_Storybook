using ATXK.CustomVariables;
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

    // TODO: Erm have different styles of reducing health bar ( eg. thru time, upon impact etc )

    private void Start()
    {
        m_AIHealth.value = m_healthBar.sizeDelta.y;
    }

    /// <summary>
    /// Unity Update
    /// </summary>
    private void Update()
    {
        // If Player Health / AI Health is 0, stop healthbar
        if (m_playerHealth.value <= 0 || m_AIHealth.value <= 0.0f) return;

        m_AIHealth.value -= 50.0f * Time.deltaTime;
        m_healthBar.sizeDelta = new Vector2(m_healthBar.sizeDelta.x, m_AIHealth.value);
    }
}
