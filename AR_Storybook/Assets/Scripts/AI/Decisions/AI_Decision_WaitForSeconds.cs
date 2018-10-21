using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.AI;
using ATXK.EventSystem;

[CreateAssetMenu(menuName = "AI/Decision/Wait for Seconds")]
public class AI_Decision_WaitForSeconds : AI_Decision
{
	[SerializeField] float waitTime;
	[SerializeField] float runTime;

	public override void Reset()
	{
		runTime = waitTime;
	}

	public override bool Decide(AI_Controller controller)
	{
		return Wait(controller);
	}

	private bool Wait(AI_Controller controller)
	{
		//Debug.Log("AI_Decision_WaitForSeconds runtime=" + runTime);
		runTime -= Time.deltaTime;
		return runTime <= 0f;
	}
}
