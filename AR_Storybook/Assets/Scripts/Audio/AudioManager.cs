using ATXK.EventSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/* Handles the Audio of the Game ( using Sounds Object class ) */
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// Add in settings file to load and adjust volume
    /// </summary>
    //[SerializeField]
    //private Settings m_settings;

    /// <summary>
    /// Have Array of Sound where can add/remove easily
    /// Have different settings for each Audio
    /// </summary>
    public Sound[] m_soundList;

	/// <summary>
    /// Loop thru all the Sound in list and add sound component
    /// </summary>
	void Awake ()
    {
		foreach(Sound _sound in m_soundList)
        {
            _sound.m_audioSource = gameObject.AddComponent<AudioSource>(); // add new audio source to this sound
            _sound.m_audioSource.clip = _sound.m_audio; // set audio source slip to be obj's clip
            _sound.m_audioSource.outputAudioMixerGroup = _sound.m_mixerGroup; // set audio source mixer group
            _sound.m_audioSource.volume = _sound.m_volume; // set audio source volume to be obj's volume
            _sound.m_audioSource.pitch = _sound.m_pitch; // set audio source pitch to be obj's pitch
            _sound.m_audioSource.loop = _sound.m_bLoop; // setting loop
            _sound.m_audioSource.playOnAwake = _sound.m_bPlayOnAwake; // setting play on awake
        }
	}

    /// <summary>
    /// Play BGM here
    /// </summary>
    private void Start()
    {
        // Load at the volume that is saved
        //m_settings.SetVolume(PlayerPrefs.GetFloat("BGM_Volume"));
        //m_settings.SetSFXVolume(PlayerPrefs.GetFloat("sfx_Volume"));
        //Debug.Log("Audio : " + PlayerPrefs.GetFloat("BGM_Volume") + " SFX : " + PlayerPrefs.GetFloat("sfx_Volume"));
    }

    /// <summary>
    /// Function to play Sound according to param
    /// </summary>
    //public void PlaySound(string _soundName)
    //{
    //    foreach (Sound _sound in m_soundList)
    //    {
    //        // if there is already another sound being played, stop that sound
    //        if (_sound.m_bIsBGM)
    //            _sound.m_audioSource.Stop();

    //        // skip if not sound name that we finding
    //        if (_sound.m_name != _soundName) continue;
            
    //        // Play Audio
    //        _sound.m_audioSource.Play();
    //    }
    //}
    public void PlaySound(Object _soundObj)
    {
        Sound temp = _soundObj as Sound;
        foreach (Sound _sound in m_soundList)
        {
            // if there is already another sound being played, stop that sound
            if (_sound.m_bIsBGM && temp.m_bIsBGM)
                _sound.m_audioSource.Stop();

            // skip if not sound name that we finding
            if (_sound.m_name != temp.m_name) continue; 

            // Play Audio
            _sound.m_audioSource.Play();
        }
    }
}
