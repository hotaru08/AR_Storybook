using ATXK.AI;
using ATXK.EventSystem;
using ATXK.Helper;
using UnityEngine;

/// <summary>
/// Action for Victory
/// </summary>
[CreateAssetMenu(menuName = "AI/Action/Victory")]
public class AI_Action_Victory : AI_Action
{
    [SerializeField] private Sound m_sound;
    [SerializeField] private ES_Event_Object m_triggerSoundEvent;

    public override void Act(AI_Controller _controller)
    {
        // Set "Victory" Animation to Play
        _controller.gameObject.GetComponent<Animator>().SetBool("Victory", true);

        // Send Sound event to play
        if (m_sound != null)
            m_triggerSoundEvent.RaiseEvent(m_sound);
    }
}
