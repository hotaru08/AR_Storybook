namespace ATXK.AI
{
	using UnityEngine;

	public class AI_Controller : MonoBehaviour
	{
		AI_State startingState;
		AI_State currentState;

		private void Start()
		{
			currentState = startingState;
		}

		private void Update()
		{
			currentState.UpdateState(this);
		}
	}
}