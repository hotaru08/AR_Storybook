using ATXK.CustomVariables;
using ATXK.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// For Settings in Options ( eg. audio volumes, mode triggers etc )
/// </summary>
public class UI_Options : MonoBehaviour
{
    [Tooltip("Bool Custom Variable to trigger Button Mode")]
    [SerializeField] private CV_Bool m_buttonMode;
    [Tooltip("Bool event to trigger the appearance of buttons if button mode is toggled")]
    [SerializeField] private ES_Event_Bool m_isButtonMode;
    [Tooltip("Toggle Component to trigger Button Mode")]
    [SerializeField] private Toggle m_toggle;

	// Use this for initialization
	void OnEnable()
    {
        m_isButtonMode.Value = m_buttonMode.value;

        // Set the toggle to display according to Custom Variable settings
        if (m_buttonMode.value)
            m_toggle.isOn = true;
        else
            m_toggle.isOn = false;
    }

    /// <summary>
    /// Setting custon variable to set button mode
    /// </summary>
    public bool ButtonMode
    {
        set
        {
            m_buttonMode.value = value;
            m_isButtonMode.RaiseEvent(m_buttonMode.value);
            Debug.LogWarning("Mode: " + m_buttonMode.value);
        }
        get
        {
            return m_buttonMode.value;
        }
    }
}
