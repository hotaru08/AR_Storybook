using ATXK.AI;
using ATXK.CustomVariables;
using ATXK.EventSystem;
using ATXK.Helper;
using UnityEngine;

/// <summary>
/// Action for Lose of AI
/// </summary>
[CreateAssetMenu(menuName = "AI/Action/Lose")]
public class AI_Action_Lose : AI_Action
{
    public override void Act(AI_Controller _controller)
    {
        Lose(_controller);
    }

    /// <summary>
    /// Action that is carried out when in state
    /// </summary>
    private void Lose(AI_Controller _controller)
    {
        //DebugLogger.Log<AI_Action_Lose>("This is Action Lose");
        _controller.gameObject.GetComponent<Animator>().SetBool("Lose", true);
    }
}
