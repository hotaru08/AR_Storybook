using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.Helper;
using ATXK.EventSystem;
using UnityEngine.Playables;

public class Event_Cutscene : MonoBehaviour
{
    /// <summary>
    /// When this script is enabled, set Timeline Manager to handle this Director and Play Cutscene attacted
    /// </summary>
    private void OnEnable()
    {
        TimelineManager.Instance.SetPlayableDirector(GetComponent<PlayableDirector>());
        TimelineManager.Instance.UseDirectorFunctions("Play");
        DebugLogger.Log<Event_Cutscene>("TimelineManager: " + TimelineManager.Instance.ToString());
    }
}
