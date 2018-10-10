﻿using UnityEngine;
using UnityEngine.Audio;

/* The custom object class for Sounds */
[System.Serializable] // -> so can appear in inspector
public class Sound
{
    // name of sound
    public string m_name;

    // audio clip to play for this sound
    public AudioClip m_audio;

    // audio mixer group to control volume
    public AudioMixerGroup m_mixerGroup;

    // volume of sound
    [Range(0.0f,1.0f)]
    public float m_volume;

    // pitch of the sound
    [Range(0.1f, 3.0f)]
    public float m_pitch;

    // the audio source of the object ( public to access it in another script )
    [HideInInspector]
    public AudioSource m_audioSource;

    // loop audio
    public bool m_bLoop;

    // play on awake
    public bool m_bPlayOnAwake;
}