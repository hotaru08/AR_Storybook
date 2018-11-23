using ATXK.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script to hold Buttons present in a scene and do things that are to be applied to all of them
/// </summary>
public class ButtonHolders : MonoBehaviour
{
    /// <summary>
    /// Array holding all buttons that are present in a specific scene
    /// </summary>
    [SerializeField] private Button[] m_buttonList;

    /// <summary>
    /// Sound event to play when button is clicked
    /// </summary>
    [Header("Sounds")]
    [SerializeField] private ES_Event_Object m_soundEvent;
    [SerializeField] private Sound m_sound;

    private void Awake()
    {
        foreach (Button _button in m_buttonList)
        {
            if (_button == null) continue;
            _button.onClick.AddListener(PlayButtonSound);
        }
    }

    private void PlayButtonSound()
    {
        m_soundEvent.RaiseEvent(m_sound);
    }
}
