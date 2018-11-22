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
    [SerializeField] private Sound m_sound;
    [SerializeField] private ES_Event_Object m_triggerSoundEvent;

    public override void Act(AI_Controller _controller)
    {
        // Set "Jump" Animation to play
        _controller.gameObject.GetComponent<Animator>().SetTrigger("Jump");

        // Send Sound event to play
        if (m_sound != null)
            m_triggerSoundEvent.RaiseEvent(m_sound);
    }
}
