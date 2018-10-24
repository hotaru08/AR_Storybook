using ATXK.AI;
using ATXK.EventSystem;
using ATXK.Helper;
using UnityEngine;

/// <summary>
/// Action for Idle
/// </summary>
[CreateAssetMenu(menuName = "AI/Action/Victory")]
public class AI_Action_Victory : AI_Action
{
    public override void Act(AI_Controller _controller)
    {
        // Set "Victory" Animation to Play
        _controller.gameObject.GetComponent<Animator>().SetBool("Victory", true);
    }
}
