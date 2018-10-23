using ATXK.EventSystem;
using ATXK.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

/// <summary>
/// Handles the Cutscenes
/// </summary>
public class TimelineManager : SingletonBehaviour<TimelineManager>
{
    [SerializeField]
    private PlayableDirector m_director;

    [SerializeField]
    private TimelineAsset[] m_TimelineArray;

    [SerializeField]
    private ES_Event_Base[] m_eventsToSend;

    /// <summary>
    /// Play the specific Timeline according to its index in the array
    /// </summary>
    /// <param name="_index">Index of timeline in array</param>
    public void PlayTimeline(int _index)
    {
        if (_index < 0 || _index > m_TimelineArray.Length - 1)
        {
            Debug.Log("Index is too low or too high");
            return;
        }

        TimelineAsset tempTimeline = null;
        tempTimeline = m_TimelineArray[_index];
        if (tempTimeline == null)
        {
            Debug.Log("Timeline index returned null");
            return;
        }
        m_director.Play(tempTimeline);
    }

    /// <summary>
    /// Set Playable Director to be controlled by Manager
    /// </summary>
    public void SetPlayableDirector(PlayableDirector _director)
    {
        m_director = _director;
    }

	public void SetDirectorTime(float time)
	{
		m_director.time = time;
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

    // For now, just do it here
    private void Update()
    {
        if (m_director == null) return;

        Debug.LogWarning(m_director.duration);
        Debug.LogWarning(m_director.time);

        if (m_director.time >= m_director.duration - 0.5f)
        {
            m_eventsToSend[0].Invoke();
        }
    }
}