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
    [SerializeField] private Sound m_sound;
    [SerializeField] private ES_Event_Object m_triggerSoundEvent;

    public override void Act(AI_Controller _controller)
    {
        // Set "Attack" Animation to play
        _controller.gameObject.GetComponent<Animator>().SetTrigger("Attack");

        // Send Sound event to play
        if (m_sound != null)
        m_triggerSoundEvent.RaiseEvent(m_sound);
    }
}
