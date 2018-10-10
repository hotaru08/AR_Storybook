using ATXK.AI;
using ATXK.Helper;
using UnityEngine;

/// <summary>
/// Action for Idle
/// </summary>
[CreateAssetMenu(menuName = "AI/Action/Idle")]
public class AI_Action_Idle : AI_Action
{
    /// <summary>
    /// Take in Gameobject to edit ( eg. animation )
    /// </summary>
    [SerializeField]
    private GameObject m_objToAnimate;

    public override void Act(AI_Controller controller)
    {
        Idle(controller);
    }

    /// <summary>
    /// Action that is carried out when in IDLE state
    /// </summary>
    private void Idle(AI_Controller controller)
    {
        DebugLogger.Log<AI_Action_Idle>("In Idle State");
        DebugLogger.Log<AI_Action_Idle>("Animator: " + m_objToAnimate.GetComponent<Animator>().GetCurrentAnimatorClipInfoCount(0));
        
        
    }
}
