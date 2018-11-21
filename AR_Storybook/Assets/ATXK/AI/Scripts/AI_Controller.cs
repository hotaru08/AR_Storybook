namespace ATXK.AI
{
	using UnityEngine;

	/// <summary>
	/// Controller that holds the starting state and a reference to the current state..
	/// </summary>
	public class AI_Controller : MonoBehaviour
	{
		[SerializeField] AI_State startState;
		[SerializeField] AI_State currState;

		/// <summary>
		/// Called on the frame when this behaviour becomes enabled.
		/// </summary>
		private void Start()
		{
			if(startState != null)
				currState = startState;
		}

		/// <summary>
		/// Called every frame.
		/// </summary>
		private void Update()
		{
			if(currState != null)
				currState.UpdateState(this);
		}

		/// <summary>
		/// Changes the current state to the one provided.
		/// </summary>
		/// <param name="nextState">State that will become the new current state.</param>
		public void ChangeState(AI_State nextState)
		{
			currState = nextState;
		}

		/// <summary>
		/// Changes the current state to the one provided, as long as the Object provided is type of AI_State.
		/// </summary>
		/// <param name="nextState">State that will become the new current state.</param>
		public void ChangeState(Object nextState)
		{
			AI_State state = nextState as AI_State;
			if (state != null)
				ChangeState(state);
		}
	}
}