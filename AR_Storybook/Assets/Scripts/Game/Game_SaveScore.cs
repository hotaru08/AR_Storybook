using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.CustomVariables;
using ATXK.EventSystem;

[RequireComponent(typeof(ES_EventListener))]
public class Game_SaveScore : MonoBehaviour
{
	[SerializeField] CV_String playerPref_HighScore;

	public bool SaveScore(int score)
	{
		if (score > PlayerPrefs.GetInt(playerPref_HighScore.value))
		{
			PlayerPrefs.SetInt(playerPref_HighScore.value, score);
			return true;
		}
		return false;
	}
}
