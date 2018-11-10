using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.EventSystem;
using ATXK.CustomVariables;

public class Game_TimeScore : MonoBehaviour
{
	[SerializeField] float currentScore;
	[SerializeField] ES_Event_Int saveScore;
	[SerializeField] bool playerDied;

	private void Start()
	{
		currentScore = 0f;
		playerDied = false;
	}

	private void Update()
	{
		if(!playerDied)
			currentScore += Time.deltaTime;
	}

	public void SaveScore()
	{
		playerDied = true;
		saveScore.RaiseEvent((int)currentScore);
	}
}
