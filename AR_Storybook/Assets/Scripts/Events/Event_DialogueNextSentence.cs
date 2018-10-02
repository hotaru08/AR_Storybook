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
    /// <summary>
    /// Carry out certain action when function is called
    /// </summary>
    public void EventReceived()
    {
        GetComponent<DialogueSystem>().NextSentence();
    }
}
