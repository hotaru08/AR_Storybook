using ATXK.CustomVariables;
using ATXK.EventSystem;
using ATXK.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// IDK what im doing really
/// </summary>
[RequireComponent(typeof(ES_EventListener))]
public class GameModes : MonoBehaviour
{
    /// <summary>
    /// Enum to store the various style of game 
    /// </summary>
    public enum GAME_MODE
    {
        TUTORIAL,
        GAME
    }
    [Header("Mode")]
    [SerializeField] private GAME_MODE m_gameMode;
    public GAME_MODE Mode { get { return m_gameMode; } }

    /// <summary>
    /// Custom Variables of Healths
    /// </summary>
    [Header("Health Variables")]
    [SerializeField] private CV_Int m_playerHealth;
    [SerializeField] private CV_Float m_AIHealth;

    /// <summary>
    /// Variables for countdown
    /// </summary>
    [Header("CountDown Variables")]
    [SerializeField] private int m_countDownTime = 3;

    /// <summary>
    /// Screens for the various Modes
    /// </summary>
    [Header("Screen Variables")]
    [SerializeField] private GameObject m_instructionScreen;
    [SerializeField] private GameObject m_countDownScreen;

    /// <summary>
    /// Events to send
    /// </summary>
    [Header("Events")]
    [Tooltip("Event to trigger the respective instruction screens according to their child indexes")]
    [SerializeField] private ES_Event_Int m_triggerInstructions;
    [Tooltip("Event to trigger CountDown to start")]
    [SerializeField] private ES_Event_Int m_startCountDown;

    [Tooltip("Event to reset States of Gameobjects to string input")]
    [SerializeField] private ES_Event_String m_resetState;
    [Tooltip("Event to set activeness of AIs spawner")]
    [SerializeField] private ES_Event_Bool m_setSpawner;
    public ES_Event_Bool GetSpawnerEvent { get { return m_setSpawner; } }

    [Tooltip("Event to reset Health of both Player and AIs")]
    [SerializeField] private ES_Event_Base m_resetHealth;

    /// <summary>
    /// Unity Start Function
    /// </summary>
    private void Start()
    {
        // ---------- Initialise Variables
        switch (m_gameMode)
        {
            case GAME_MODE.TUTORIAL:
                if (this.transform.Find("UI/Screen ( Instructions )") == null)
                {
                    GameObject temp = Instantiate(m_instructionScreen, this.transform.Find("UI"));
                }
                m_triggerInstructions.Invoke(0);
                break;
            case GAME_MODE.GAME:
                if (this.transform.Find("UI/Screen ( Countdown )") == null)
                {
                    GameObject temp = Instantiate(m_countDownScreen, this.transform.Find("UI"));
                }
                m_startCountDown.Invoke(m_countDownTime);
                break;
        }
    }

    public void EventReceived(int _value)
    {
        m_triggerInstructions.Value = _value;
    }

    /// <summary>
    /// Call this when resetting game scenes
    /// </summary>
    public void ResetGame()
    {
        m_resetState.Invoke();
        m_setSpawner.Invoke(true);
        m_resetHealth.Invoke();
    }
}