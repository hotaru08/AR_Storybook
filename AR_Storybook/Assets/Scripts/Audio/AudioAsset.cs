using ATXK.EventSystem;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Class for Audio Asset where multiple Sound is needed to be sent in a group
/// </summary>
[CreateAssetMenu(menuName = "Audio/AudioAsset")]
public class AudioAsset : ScriptableObject
{
    [SerializeField]
    private Sound[] m_sounds;
    public Sound[] Sounds { get { return m_sounds; } }
}
