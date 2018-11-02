using ATXK.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Have a countdown before actual gameplay starts
/// </summary>
public class Game_CountDown : MonoBehaviour
{
    [Tooltip("Text used to render the Countdown numbers")]
    [SerializeField] private Text m_count;
    private int m_countDownValue;

    [Header("CountDown Variables")]
    [SerializeField] private float m_MaxCountDownTime = 2.0f;
    private float m_countDownTime;

    [Header("Events to Send")]
    [SerializeField] private ES_Event_Bool m_startGame;

    /// <summary>
    /// Set count down values
    /// </summary>
    public void SetCountDownValue(int _value)
    {
        m_countDownValue = _value;
        m_countDownTime = m_MaxCountDownTime;
        m_count.text = m_countDownValue.ToString();
    }

    private void Update()
    {
        if (m_count == null) return;
        
        // If Countdown is done, start game
        if (m_countDownValue <= 0)
        {
            m_startGame.RaiseEvent(true);
            this.gameObject.SetActive(false);
        }

        // After private countdown is done, reduce a value from text
        m_countDownTime -= Time.deltaTime;
        if (m_countDownTime <= 0.0f)
        {
            // Reduce a count from number on UI
            SetCountDownValue(m_countDownValue - 1);
        }
    }
}
