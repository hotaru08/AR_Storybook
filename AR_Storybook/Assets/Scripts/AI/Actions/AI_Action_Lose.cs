using ATXK.AI;
using ATXK.CustomVariables;
using ATXK.EventSystem;
using ATXK.Helper;
using UnityEngine;

/// <summary>
/// Action for Lose of AI
/// </summary>
[CreateAssetMenu(menuName = "AI/Action/Lose")]
public class AI_Action_Lose : AI_Action
{
    [SerializeField] private Sound m_sound;
    [SerializeField] private ES_Event_Object m_triggerSoundEvent;

    public override void Act(AI_Controller _controller)
    {
        // Set "Lose" Animation to Play
        _controller.gameObject.GetComponent<Animator>().SetBool("Lose", true);

        // Send Sound event to play
        m_triggerSoundEvent.RaiseEvent(m_sound);
    }
}
