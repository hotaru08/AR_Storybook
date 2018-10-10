using ATXK.AI;
using ATXK.Helper;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decision/ChangeToAttack")]
public class AI_Decision_Attack : AI_Decision
{
    [SerializeField]
    

    public override bool Decide(AI_Controller controller)
    {
        return Attack(controller);
    }

    /// <summary>
    /// Function to check for trigger to change the AI state to Attack
    /// </summary>
    private bool Attack(AI_Controller controller)
    {
        DebugLogger.Log<AI_Decision_Attack>("Entered Here");
        return true;
    }
}
