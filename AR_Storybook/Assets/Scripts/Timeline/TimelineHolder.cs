using ATXK.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Attached to Gameobjects that has Director Component ( Cutscenes )
/// </summary>
public class TimelineHolder : MonoBehaviour
{
    [Tooltip("Event containing PlayableDirector that is to be sent to TimelineManager")]
    [SerializeField] private ES_Event_Object m_directorObject;
    [Tooltip("Event to trigger spawning of dialogue")]
    [SerializeField] private ES_Event_Bool m_spawnDialogue;
    [Tooltip("Event containing first clip time for respective Timelines")]
    [SerializeField] private ES_Event_String m_firstClipTiming;
    [SerializeField] private string m_timing;

    private void Awake()
    {
        // Set the current time of director to be at start
        GetComponent<PlayableDirector>().time = 0.0;

        // Raise events
        m_directorObject.RaiseEvent(GetComponent<PlayableDirector>());
        m_spawnDialogue.RaiseEvent(false);
        m_firstClipTiming.RaiseEvent(m_timing);
    }
}
