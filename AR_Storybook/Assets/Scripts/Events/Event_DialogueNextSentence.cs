using ATXK.EventSystem;
using ATXK.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to trigger DialogueManager NextSentence
/// </summary>
public class Event_DialogueNextSentence : MonoBehaviour
{
    string eventName;

    private void Start()
    {
        eventName = GetComponent<ES_GameEventListener>().gameEvent.name;
    }

    /// <summary>
    /// Carry out certain action when function is called
    /// </summary>
    public void OnEventReceived()
    {
        DebugLogger.Log<Event_DialogueNextSentence>("Event Received From: " + eventName);
        DialogueSystem.Instance.NextSentence();
    }
}
