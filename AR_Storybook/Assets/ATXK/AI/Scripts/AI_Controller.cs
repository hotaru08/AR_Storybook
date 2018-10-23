namespace ATXK.AI
{
	using UnityEngine;

	public class AI_Controller : MonoBehaviour
	{
		[SerializeField] AI_State startState;
		[SerializeField] AI_State currState;

		private void Start()
		{
			currState = startState;
		}

		private void Update()
		{
			currState.UpdateState(this);
		}

		public void ChangeState(AI_State nextState)
		{
			currState = nextState;
		}
	}
}