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
    [SerializeField] private ES_Event_UnityObject m_directorObject;
    [Tooltip("Event to trigger spawning of dialogue")]
    [SerializeField] private ES_Event_Bool m_spawnDialogue;

    private void Awake()
    {
        // Set the current time of director to be at start
        GetComponent<PlayableDirector>().time = 0.0;

        m_directorObject.Invoke(GetComponent<PlayableDirector>());
        m_spawnDialogue.Value = false;
    }
}
