using ATXK.AI;
using ATXK.CustomVariables;
using ATXK.Helper;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decision/PlayerLose")]
public class AI_Decision_PlayerLose : AI_Decision
{
    [SerializeField]
    private CV_Int m_playerHealth;

    public override bool Decide(AI_Controller _controller)
    {
        return ChangeToPlayerLose(_controller);
    }

	public override void Reset()
	{

	}

	private bool ChangeToPlayerLose(AI_Controller _controller)
    {
        return m_playerHealth.value <= 0;
    }
}
