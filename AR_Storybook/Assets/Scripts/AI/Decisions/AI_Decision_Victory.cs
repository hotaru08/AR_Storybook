using ATXK.AI;
using ATXK.CustomVariables;
using ATXK.Helper;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decision/ChangeToVictory")]
public class AI_Decision_Victory : AI_Decision
{
    [SerializeField]
    private CV_Int m_playerHealth;

    public override bool Decide(AI_Controller _controller)
    {
        return ChangeToVictory(_controller);
    }

	private bool ChangeToVictory(AI_Controller _controller)
    {
        if (m_playerHealth.value <= 0)
        {
            DebugLogger.Log<AI_Decision_Victory>("Player Health in Decision: " + m_playerHealth.value);
            return true;
        }
        return false;
    }
}
