﻿using ATXK.EventSystem;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(ES_EventListener))]
public class CutsceneManager : MonoBehaviour
{
    [Header("Cutscene Settings")]
    [SerializeField] bool playOnChange;
    [SerializeField] int startIndex;

    [Header("Cutscene Directors")]
    [SerializeField] PlayableDirector[] directors;

    [Header("Runtime Directors")]
    [SerializeField] PlayableDirector currentDirector;

    #region Property Getters
    public PlayableDirector CurrentDirector { get { return currentDirector; } }
    public PlayableDirector[] Directors { get { return directors; } }
    public bool PlayOnChange { get { return playOnChange; } set { playOnChange = value; } }
    #endregion

    private void Start()
    {
        foreach (PlayableDirector director in directors)
        {
            director.gameObject.SetActive(false);
        }
        ChangeCutscene(startIndex);
    }

    public void ChangeCutscene(int index)
    {
        if (index < 0 || index > directors.Length - 1)
            return;

        foreach (PlayableDirector director in directors)
        {
            director.gameObject.SetActive(false);
        }

        currentDirector = directors[index];
        currentDirector.gameObject.SetActive(true);

        if (playOnChange)
            currentDirector.Play();
    }

    /// <summary>
    /// Plays the current director ( timeline )
    /// </summary>
    public void PlayCurrentCutscene()
    {
        currentDirector.Play();
    }

    private void Update()
    {
    }
}
