using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.Helper;
using ATXK.EventSystem;
using UnityEngine.Playables;

public class Event_Cutscene : MonoBehaviour
{
    //string eventName;

    //private void Start()
    //{
    //    eventName = GetComponent<ES_GameEventListener>().gameEvent.name;
    //}

    //public void EventReceived()
    //{
    //    DebugLogger.Log<Event_Cutscene>("Event received from: " + eventName);
    //}

    /// <summary>
    /// When this script is enabled, set Timeline Manager to handle this Director and Play Cutscene attacted
    /// </summary>
    private void OnEnable()
    {
        TimelineManager.Instance.SetPlayableDirector(GetComponent<PlayableDirector>());
        TimelineManager.Instance.UseDirectorFunctions("Play");
        DebugLogger.Log<Event_Cutscene>("TimelineManager: " + TimelineManager.Instance.ToString());
    }

    /// <summary>
    /// When this script is disabled, call OnDestroy()
    /// </summary>
    //private void OnDisable()
    //{
    //    //OnDestroy();
    //    TimelineManager.Instance.UseDirectorFunctions("Stop");
    //    //TimelineManager.Instance.SetPlayableDirector(null);
    //}
}
