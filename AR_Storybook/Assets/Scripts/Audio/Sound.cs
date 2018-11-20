using ATXK.EventSystem;
using UnityEngine;
using UnityEngine.Audio;

/* The custom object class for Sounds */
[CreateAssetMenu(menuName = "Audio/Sounds", order = 2)]
public class Sound : ScriptableObject
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

    // the audio source of the object
    [HideInInspector]
    public AudioSource m_audioSource;

    // loop audio
    public bool m_bLoop;

    // play on awake
    public bool m_bPlayOnAwake;

    // If this is a BGM or not
    public bool m_bIsBGM;
}
