﻿using ATXK.AI;
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
        Victory(_controller);
    }

    /// <summary>
    /// Action that is carried out when in IDLE state
    /// </summary>
    private void Victory(AI_Controller _controller)
    {
        //DebugLogger.Log<AI_Action_Victory>("This is Action Victory");
        _controller.gameObject.GetComponent<Animator>().SetBool("Victory", true);

        // If Player uses this, then skip cuz no spawner
        if (_controller.gameObject.CompareTag("Player")) return;
        if (_controller.gameObject.transform.Find("Item_Spawner") == null || 
            !_controller.gameObject.transform.Find("Item_Spawner").gameObject.activeSelf)
            return;

        _controller.gameObject.transform.Find("Item_Spawner").gameObject.SetActive(false);
    }
}