using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

/// <summary>
/// Handles the Cutscenes
/// </summary>
public class TimelineManager : ATXK.Helper.SingletonBehaviour<TimelineManager>
{
    [SerializeField]
    private PlayableDirector m_director;

    [SerializeField]
    private TimelineAsset[] m_TimelineArray;

    /// <summary>
    /// Play the specific Timeline according to its index in the array
    /// </summary>
    /// <param name="_index">Index of timeline in array</param>
    public void PlayTimeline(int _index)
    {
        if (_index < 0 || _index > m_TimelineArray.Length)
        {
            Debug.Log("Index is too low or too high");
            return;
        }

        TimelineAsset tempTimeline = null;
        tempTimeline = m_TimelineArray[_index];
        Debug.Log("Timeline Name: " + tempTimeline.name);
        if (tempTimeline == null)
        {
            Debug.Log("Timeline index returned null");
            return;
        }
        m_director.Play(tempTimeline);
    }


    /// <summary>
    /// Get the status of the Director
    /// </summary>
    /// <returns>Status of the director handling the Timelines</returns>
    public string GetStatus()
    {
        return m_director.state.ToString();
    }

    /// <summary>
    /// Based on string param, use the various functionality of Director
    /// </summary>
    /// <param name="_trigger">Trigger for various functions</param>
    public void UseDirectorFunctions(string _trigger)
    {
        switch(_trigger)
        {
            case "Pause":
                m_director.Pause();
                break;
            case "Resume":
                m_director.Resume();
                break;
            case "Play":
                m_director.Play();
                break;
            case "Stop":
                m_director.Stop();
                break;
        }
    }
}