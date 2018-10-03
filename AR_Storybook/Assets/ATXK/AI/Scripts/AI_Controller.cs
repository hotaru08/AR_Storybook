namespace ATXK.AI
{
	using UnityEngine;
	using UnityEngine.AI;

	/// <summary>
	/// Updates the current state of the AI and handles transitioning to other states.
	/// </summary>
	[RequireComponent(typeof(NavMeshAgent))]
	public class AI_Controller : MonoBehaviour
	{
		[Header("AI States")]
		[SerializeField] AI_State startingState;
		[SerializeField] AI_State currentState;

		[Header("Statistics")]
		public AI_Stats aiStats;
		[HideInInspector] public GameObject target;
		[HideInInspector] public NavMeshAgent aiNavMeshAgent;

		private void Start()
		{
			aiNavMeshAgent = GetComponent<NavMeshAgent>();

			ChangeState(startingState);
		}

		private void Update()
		{
			currentState.UpdateState(this);
		}

		public void ChangeState(AI_State state)
		{
			currentState = state;
		}

		public Vector3 GetTargetPosition()
		{
			return target.transform.position;
		}
	}
}