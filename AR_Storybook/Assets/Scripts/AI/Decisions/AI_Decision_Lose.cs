﻿using ATXK.AI;
using ATXK.CustomVariables;
using ATXK.Helper;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decision/ChangeToLose")]
public class AI_Decision_Lose : AI_Decision
{
    [SerializeField]
    private CV_Float m_AIHealth;

    public override bool Decide(AI_Controller _controller)
    {
        return ChangeToLose(_controller);
    }

	public override void Reset()
	{

	}

	private bool ChangeToLose(AI_Controller _controller)
    {
        if (m_AIHealth.value <= 0.0f)
        {
            DebugLogger.Log<AI_Decision_Lose>("AI Health in Decision: " + m_AIHealth.value);
            return true;
        }
        return false;
    }
}
