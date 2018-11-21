using ATXK.AI;
using ATXK.EventSystem;
using ATXK.Helper;
using UnityEngine;

/// <summary>
/// Action for Idle
/// </summary>
[CreateAssetMenu(menuName = "AI/Action/Idle")]
public class AI_Action_Idle : AI_Action
{
    [SerializeField] private Sound m_sound;
    [SerializeField] private ES_Event_Object m_triggerSoundEvent;

    public override void Act(AI_Controller _controller)
    {
        Idle(_controller);
    }

    /// <summary>
    /// Action that is carried out when in IDLE state
    /// </summary>
    private void Idle(AI_Controller _controller)
    {
        // reset animation
        if (_controller.gameObject.GetComponent<Animator>().GetBool("Victory"))
            _controller.gameObject.GetComponent<Animator>().SetBool("Victory", false);
        else if (_controller.gameObject.GetComponent<Animator>().GetBool("Lose"))
            _controller.gameObject.GetComponent<Animator>().SetBool("Lose", false);

        // Send Sound event to play
        if (m_sound != null)
            m_triggerSoundEvent.RaiseEvent(m_sound);
    }
}
