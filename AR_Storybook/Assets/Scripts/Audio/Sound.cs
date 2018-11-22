using ATXK.EventSystem;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Class for General Sounds ScriptableObject
/// </summary>
[CreateAssetMenu(menuName = "Audio/Sounds")]
public class Sound : ScriptableObject
{
	// name of this audio object
	public string m_audioName;

    // audio clip to play for this sound
    public AudioClip m_audio;

    // audio mixer group to control volume
    public AudioMixerGroup m_mixerGroup;

    // volume of sound
    [Range(0.0f,1.0f)]
    public float m_volume;

    // pitch of the sound
    [Range(0.1f, 3f)]
    public float m_pitch;

    // the audio source of the object
    //[HideInInspector]
    public AudioSource m_audioSource;

    // loop audio
    public bool m_bLoop;

    // play on awake
    public bool m_bPlayOnAwake;

    // If this is a BGM or not
    public bool m_bIsBGM;
}
