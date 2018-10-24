using ATXK.AI;
using ATXK.EventSystem;
using ATXK.Helper;
using UnityEngine;

/// <summary>
/// Action for Attack
/// </summary>
[CreateAssetMenu(menuName = "AI/Action/Attack")]
public class AI_Action_Attack : AI_Action
{
    public override void Act(AI_Controller _controller)
    {
        // Set "Attack" Animation to play
        _controller.gameObject.GetComponent<Animator>().SetTrigger("Attack");
    }
}
