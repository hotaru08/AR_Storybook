namespace ATXK.AI
{
	using UnityEngine;

	public class AI_Controller : MonoBehaviour
	{
		[SerializeField] AI_State startState;
		[SerializeField] AI_State currState;

		private void Start()
		{
			if(startState != null)
				currState = startState;
		}

		private void Update()
		{
			if(currState != null)
				currState.UpdateState(this);
		}

		public void ChangeState(AI_State nextState)
		{
			currState = nextState;
		}
	}
}