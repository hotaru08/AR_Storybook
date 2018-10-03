namespace ATXK.AI
{
	[System.Serializable]
	public class AI_Transition
	{
		public AI_Decision decision;
		public AI_State stateToTransitionTo;
	}
}