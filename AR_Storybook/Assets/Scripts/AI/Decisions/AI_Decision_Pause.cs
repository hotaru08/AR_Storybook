using ATXK.AI;
using ATXK.EventSystem;
using ATXK.Helper;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decision/ChangeToPause")]
public class AI_Decision_Pause : AI_Decision
{
    /// <summary>
    /// Event containing value of Pause
    /// </summary>
    [SerializeField] private ES_Event_Bool m_pause;

    public override bool Decide(AI_Controller _controller)
    {
        return m_pause.Value;
    }
}
