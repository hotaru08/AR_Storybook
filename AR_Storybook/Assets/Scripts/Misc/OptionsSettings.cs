using ATXK.CustomVariables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Settings file for Options
/// </summary>
public class OptionsSettings : ScriptableObject
{
    [SerializeField] private CV_Float m_bgmVolume;
    [SerializeField] private CV_Float m_sfxVolume;
}
