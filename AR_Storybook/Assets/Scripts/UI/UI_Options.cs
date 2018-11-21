using ATXK.CustomVariables;
using ATXK.EventSystem;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// For Settings in Options ( eg. audio volumes, mode triggers etc )
/// </summary>
public class UI_Options : MonoBehaviour
{
    [Header("Mixers")]
    [SerializeField] private AudioMixer m_bgmMixer;
    [SerializeField] private AudioMixer m_sfxMixer;

    [Header("Custom Variables")]
    [Tooltip("Bool Custom Variable to trigger Button Mode")]
    [SerializeField] private CV_Bool m_buttonMode;
    [Tooltip("Float Custom Variable that contains the value of BGM Volume")]
    [SerializeField] private CV_Float m_bgmVolume;
    [Tooltip("Float Custom Variable that contains the value of SFX Volume")]
    [SerializeField] private CV_Float m_sfxVolume;

    [Header("GameObjects")]
    [Tooltip("Toggle Component to trigger Button Mode")]
    [SerializeField] private Toggle m_toggle;
    [Tooltip("Slider Component for controlling BGM")]
    [SerializeField] private Slider m_bgmSlider;
    [Tooltip("Slider Component for controlling SFX")]
    [SerializeField] private Slider m_sfxSlider;

    [Header("Events to trigger")]
    [Tooltip("Bool event to trigger the appearance of buttons if button mode is toggled")]
    [SerializeField] private ES_Event_Bool m_isButtonMode;

    // Use this for initialization
    void OnEnable()
    {
        // Set Display to be values that are stored
        m_isButtonMode.Value = m_buttonMode.value;
        m_bgmSlider.value = m_bgmVolume.value;
        m_sfxSlider.value = m_sfxVolume.value;

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
        }
        get { return m_buttonMode.value; }
    }

    /// <summary>
    /// Setting BGM volume and storing into a Custom Variable
    /// </summary>
    public float BGMVolume
    {
        set
        {
            m_bgmVolume.value = m_bgmSlider.value;
            m_bgmMixer.SetFloat("BGM_Volume", m_bgmVolume.value);
        }
    }

    /// <summary>
    /// Setting SFX volume and storing into a Custom Variable
    /// </summary>
    public float SFXVolume
    {
        set
        {
            m_sfxVolume.value = m_sfxSlider.value;
            m_sfxMixer.SetFloat("SFX_Volume", m_sfxVolume.value);
        }
    }
}
