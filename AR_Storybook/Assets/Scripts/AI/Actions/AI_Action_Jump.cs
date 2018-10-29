using ATXK.AI;
using ATXK.EventSystem;
using ATXK.Helper;
using UnityEngine;

/// <summary>
/// Action for Jump
/// </summary>
[CreateAssetMenu(menuName = "AI/Action/Jump")]
public class AI_Action_Jump : AI_Action
{
    public override void Act(AI_Controller _controller)
    {
        // Set "Jump" Animation to play
        _controller.gameObject.GetComponent<Animator>().SetTrigger("Jump");
    }
}
