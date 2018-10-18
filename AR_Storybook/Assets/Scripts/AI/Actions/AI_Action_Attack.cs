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
    /// Action that is carried out when in IDLE state
    /// </summary>
    private void Attack(AI_Controller _controller)
    {
        //DebugLogger.Log<AI_Action_Attack>("This is Action Attack");

        //if (_controller.gameObject.CompareTag("Player")) return;
        //if (_controller.gameObject.transform.Find("Item_Spawner") == null ||
        //    _controller.gameObject.transform.Find("Item_Spawner").gameObject.activeSelf)
        //    return;

        //_controller.gameObject.transform.Find("Item_Spawner").gameObject.SetActive(true);
    }
}
