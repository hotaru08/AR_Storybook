using ATXK.CustomVariables;
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
    [Tooltip("Toggle Component to trigger Button Mode")]
    [SerializeField] private Toggle m_toggle;

	// Use this for initialization
	void Start ()
    {
        if (m_buttonMode.value)
            m_toggle.isOn = true;
        else
            m_toggle.isOn = false;
	}

    /// <summary>
    /// Setting custon variable to set button mode
    /// </summary>
    /// <param name="_isOn">Value of Toggle</param>
    public void SetButtonMode(bool _isOn)
    {
        m_buttonMode.value = _isOn;
    }
}
