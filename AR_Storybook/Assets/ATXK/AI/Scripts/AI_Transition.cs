namespace ATXK.AI
{
	/// <summary>
	/// Holds data regarding the state's transition.
	/// </summary>
	[System.Serializable]
	public class AI_Transition
	{
		public AI_Decision decision;
		public AI_State stateToTransitionTo;
	}
}