using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.EventSystem;
using ATXK.CustomVariables;
using UnityEngine.UI;

public class Game_TimeScore : MonoBehaviour
{
    [Header("Variables")]
	[SerializeField] float currentScore;
	[SerializeField] ES_Event_Int saveScore;
	[SerializeField] bool playerDied;
    [SerializeField] CV_String playerPref_HighScore;
	[SerializeField] int scoreMultiplier;

    [Header("Scores")]
    [Tooltip("Text to display score on HUD")]
    [SerializeField] private Text m_displayScoreHUD;
    [SerializeField] private Text m_displayScoreHUD2;
    [Tooltip("Text to display scores on Screens")]
    [SerializeField] private Text m_displayScoreScreen;

    [Header("Events")]
    [Tooltip("Event to trigger the start of the Game")]
    [SerializeField] private ES_Event_Bool m_startGame;

    private void Start()
	{
		currentScore = 0f;
		playerDied = false;
	}

	private void Update()
	{
		if(!playerDied && m_startGame.Value)
			currentScore += Time.deltaTime * scoreMultiplier;

        // Display score on HUD
        m_displayScoreHUD.text = ((int)currentScore).ToString();
        m_displayScoreHUD2.text = ((int)currentScore).ToString();
    }

	public void SaveScore()
	{
		playerDied = true;
		saveScore.RaiseEvent((int)currentScore);
	}

    /// <summary>
    /// Function to display highscore and score on screens
    /// </summary>
    public void DisplayScore(Object _screen)
    {
        GameObject temp = _screen as GameObject;
        m_displayScoreScreen = temp.GetComponent<Text>();
        m_displayScoreScreen.text = string.Format("<size=90>Highscore: {0}</size>\n" + // Highscore
                                                  "<size=80>Score: {1}</size>" // Score
                                                  , PlayerPrefs.GetInt(playerPref_HighScore.value), (int)currentScore);

    }

	public void AddScore(int value)
	{
		currentScore += value;
	}
}
