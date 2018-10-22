using ATXK.AI;
using ATXK.EventSystem;
using ATXK.Helper;
using UnityEngine;

/// <summary>
/// Action for Idle
/// </summary>
[CreateAssetMenu(menuName = "AI/Action/Attack")]
public class AI_Action_Attack : AI_Action
{
    public override void Act(AI_Controller _controller)
    {
        Attack(_controller);
    }

    /// <summary>
    /// Action that is carried out when in ATTACK state
    /// </summary>
    private void Attack(AI_Controller _controller)
    {
        // reset animation
        if (_controller.gameObject.GetComponent<Animator>().GetBool("Victory"))
            _controller.gameObject.GetComponent<Animator>().SetBool("Victory", false);
        else if (_controller.gameObject.GetComponent<Animator>().GetBool("Lose"))
            _controller.gameObject.GetComponent<Animator>().SetBool("Lose", false);

    }
}
