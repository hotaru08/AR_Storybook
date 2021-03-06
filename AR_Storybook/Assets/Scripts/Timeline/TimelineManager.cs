﻿using ATXK.EventSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

/// <summary>
/// Handles Timelines ( will need listener to listen for events )
/// </summary>
[RequireComponent(typeof(ES_EventListener))]
public class TimelineManager : MonoBehaviour
{
    [Header("Current Playable Director")]
    [SerializeField] private PlayableDirector m_currDirector;

    [Header("Settings")]
    [Tooltip("Do you want to play director upon change?")]
    [SerializeField] private bool m_playOnChange = false;

    [Header("Events")]
    [Tooltip("Bool Event to trigger the dialogue to spawn")]
    [SerializeField] private ES_Event_Bool m_SpawnDialogueEvent;
    [Tooltip("Bool Event to trigger the End overlay to spawn")]
    [SerializeField] private ES_Event_Abstract m_SpawnEndOverlayEvent;

    /// <summary>
    /// Start and End Times for Timeline clips
    /// </summary>
    private double m_clipStartTime, m_clipEndTime;

    [SerializeField] private PlayableDirector m_playable;
    private void Start()
    {
        m_clipStartTime = m_clipEndTime = 0.0;

        //Instantiate(m_playable);
    }

    #region Functions
    /// <summary>
    /// Changes Director according to current active Cutscene
    /// </summary>
    public void ChangeDirector(Object _director)
    {
        if (_director == null)
        {
            Debug.Log("Director received is null");
            return;
        }

        // Set current director to be received director
        m_currDirector = _director as PlayableDirector;

        // if bool is true, play new director 
        if (m_playOnChange)
            m_currDirector.Play();
    }

    /// <summary>
    /// Sets what to do with current director
    /// </summary>
    public void PlayDirector()
    {
        // Plays the director if there is tracks in that timeline
        if (m_currDirector.playableAsset.duration <= 0.0)
        {
            Debug.Log("There is no tracks / nothing in timeline!");
            return;
        }
        m_currDirector.Play();

        // If dialogue has spawned ( true ), break out of function
        if (m_SpawnDialogueEvent.Value) return;

        //TimelineAsset temp = m_currDirector.playableAsset as TimelineAsset;
        //foreach (TimelineClip _clip in temp.GetOutputTrack(0).GetClips())
        //{
        //    Debug.Log("Clip index: " + _clip.displayName + " End Time: " + _clip.end + " Start time: " + _clip.start);
        //}
    }

    /// <summary>
    /// Receives string and split them into doubles
    /// </summary>
    /// <param name="_timings">String containing timings</param>
    public void SetTimings(string _timings)
    {
        // Split string and store in array
        string[] temp = _timings.Split(',');

        // Assign values
        m_clipStartTime = double.Parse(temp[0]);
        m_clipEndTime = double.Parse(temp[1]);

        // Set current director's time to be start of that dialogue node's
        m_currDirector.time = m_clipStartTime;
    }

    /// <summary>
    /// Reset Timelines
    /// </summary>
    public void Reset()
    {
        m_currDirector.time = 0.0;
        PlayDirector();
    }

    /// <summary>
    /// Pause / Resume Director
    /// </summary>
    public void PauseDirector(bool _pause)
    {
        if (m_currDirector == null) return;

        if (_pause)
            m_currDirector.Pause();
        else
            m_currDirector.Resume();
    }
    #endregion

    private void Update()
    {
        // If there is no director, dont update anything
        if (m_currDirector == null) return;

        // If dialogue has not spawned yet, spawn when reached end of first clip
        if (!m_SpawnDialogueEvent.Value)
        {
            if (m_currDirector.time > m_clipEndTime)
            {
                m_SpawnDialogueEvent.RaiseEvent(true);
            }
        }

        // If dialogue is spawned, get that node's start and end timings
        if (m_currDirector.time > m_clipEndTime)
        {
            //Debug.Log("Its time to stop!" + m_currDirector.time + " Stop Time: " + m_clipEndTime);
            m_currDirector.Pause();
        }

        // If timeline reached the end, raise event to spawn end overlay
        if (m_currDirector.time >= m_currDirector.duration - 0.05f)
        {
            Debug.Log("Entered here " + m_currDirector.duration);
            if (m_SpawnEndOverlayEvent != null)
                m_SpawnEndOverlayEvent.RaiseEvent();
        }
    }
}