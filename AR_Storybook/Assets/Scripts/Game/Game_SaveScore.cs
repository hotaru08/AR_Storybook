using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.CustomVariables;
using ATXK.EventSystem;

[RequireComponent(typeof(ES_EventListener))]
public class Game_SaveScore : MonoBehaviour
{
	[SerializeField] CV_String playerPref_HighScore;

	public void SaveScore(int score)
	{
		if (score > PlayerPrefs.GetInt(playerPref_HighScore.value))
		{
			Debug.Log("New Highscore!! Current Highscore = " + PlayerPrefs.GetInt(playerPref_HighScore.value) + " New Highscore = " + score);
			PlayerPrefs.SetInt(playerPref_HighScore.value, score);
		}
	}
}
