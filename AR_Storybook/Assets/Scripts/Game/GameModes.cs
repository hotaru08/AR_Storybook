using ATXK.CustomVariables;
using ATXK.EventSystem;
using ATXK.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// IDK what im doing really
/// </summary>
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
    public GAME_MODE m_gameMode;

    /// <summary>
    /// Variables for countdown
    /// </summary>
    [Header("CountDown Variables")]
    [SerializeField]
    private float m_countDownTime;
    private float m_countDown;

    /// <summary>
    /// Events to send
    /// </summary>
    [Header("Events")]
    [SerializeField]
    private ES_Event_Int m_triggerInstructions;

    /// <summary>
    /// Unity Start Function
    /// </summary>
    private void Start()
    {
        // ---------- Send the first event according to game mode
        switch (m_gameMode)
        {
            case GAME_MODE.TUTORIAL:
                m_triggerInstructions.value = 0;
                m_triggerInstructions.Invoke(0);
                break;
            case GAME_MODE.GAME:
                break;
        }
    }

    public void EventReceived(int _value)
    {
        m_triggerInstructions.value = _value;
    }
}