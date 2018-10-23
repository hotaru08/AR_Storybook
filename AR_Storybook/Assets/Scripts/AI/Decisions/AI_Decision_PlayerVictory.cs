using ATXK.AI;
using ATXK.CustomVariables;
using ATXK.Helper;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decision/PlayerWin")]
public class AI_Decision_PlayerVictory : AI_Decision
{
    [SerializeField]
    private CV_Float m_AIHealth;

    public override bool Decide(AI_Controller _controller)
    {
        return ChangeToVictory(_controller);
    }

	private bool ChangeToVictory(AI_Controller _controller)
    {
        return m_AIHealth.value <= 0.0f;
    }
}
