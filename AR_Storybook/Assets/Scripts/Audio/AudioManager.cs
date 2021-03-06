﻿using ATXK.CustomVariables;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Handles the audio that is going to be played
/// </summary>
public class AudioManager : MonoBehaviour
{ 
    [Header("Mixers")]
    [SerializeField] private AudioMixer m_bgmMixer;
    [SerializeField] private AudioMixer m_sfxMixer;

    [Header("Custom Variables")]
    [Tooltip("Float Custom Variable that contains the value of BGM Volume")]
    [SerializeField] private CV_Float m_bgmVolume;
    [Tooltip("Float Custom Variable that contains the value of SFX Volume")]
    [SerializeField] private CV_Float m_sfxVolume;

    [Header("List of Sounds to play")]
    [SerializeField] private Sound[] m_soundList;

	/// <summary>
    /// Loop thru all the Sound in list and add sound component
    /// </summary>
	void Awake ()
    {
        foreach (Sound _sound in m_soundList)
        {
            _sound.m_audioSource = gameObject.AddComponent<AudioSource>(); // add new audio source to this sound
            _sound.m_audioSource.clip = _sound.m_audio; // set audio source slip to be obj's clip
            _sound.m_audioSource.outputAudioMixerGroup = _sound.m_mixerGroup; // set audio source mixer group
            _sound.m_audioSource.volume = _sound.m_volume; // set audio source volume to be obj's volume
            _sound.m_audioSource.pitch = _sound.m_pitch; // set audio source pitch to be obj's pitch
            _sound.m_audioSource.loop = _sound.m_bLoop; // setting loop
            _sound.m_audioSource.playOnAwake = _sound.m_bPlayOnAwake; // setting play on awake
        }

        // Load at the volume that is saved
        m_bgmMixer.SetFloat("BGM_Volume", m_bgmVolume.value);
        m_sfxMixer.SetFloat("SFX_Volume", m_sfxVolume.value);
    }

    /// <summary>
    /// Play Sounds ( one time )
    /// </summary>
    public void PlaySound(string _soundName)
    {
        foreach (Sound _sound in m_soundList)
        {
            // skip if not sound name that we finding
            if (_sound.m_audioName != _soundName) continue;

            // Play Audio
            _sound.m_audioSource.PlayOneShot(_sound.m_audio);
        }
    }
    public void PlaySound(Object _soundObj)
    {
        Sound temp = _soundObj as Sound;
        foreach (Sound _sound in m_soundList)
        {
            // skip if not sound name that we finding
            if (_sound.m_audioName != temp.m_audioName) continue;

            // Play Audio
            _sound.m_audioSource.PlayOneShot(_sound.m_audio);
        }
    }

	/// <summary>
	/// Play Music ( Looping )
	/// </summary>
	public void PlayBGM(Object _soundObj)
	{
        Sound temp = _soundObj as Sound;
		if (temp == null)
		{
			Debug.Log("PlayBGM(). Sound Object is not of type Sound.");
			return;
		}

        foreach (Sound _sound in m_soundList)
        {
            // if there is already another sound being played, stop that sound
            if (_sound.m_bIsBGM && temp.m_bIsBGM)
			{
				Debug.Log("PlayBGM(). Stopping current BGM.");
				_sound.m_audioSource.Stop();
			}

			if (_sound.m_audioName == temp.m_audioName)
			{
				// Play Audio
				temp.m_audioSource.Play();

				Debug.Log("PlayBGM(). Playing new BGM.");
				return;
			}
		}

		Debug.Log("PlayBGM(). Sound provided does not match any known sounds.");
	}
}
