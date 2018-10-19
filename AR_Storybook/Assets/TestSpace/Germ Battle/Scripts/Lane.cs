namespace TestSpace
{
	using UnityEngine;
	using ATXK.EventSystem;
	using ATXK.AI;

	[RequireComponent(typeof(ES_EventListener))]
	public class Lane : MonoBehaviour
	{
		public Transform enemyPosition;
		public Transform playerPosition;
		public ConveyorBelt conveyorBelt;
		public AI_Controller enemyPrefab;
		public GameObject enemy;
		public int laneID;
		public float enemyScale;

		private void Start()
		{
			conveyorBelt = GetComponent<ConveyorBelt>();

			SpawnEnemy();
		}

		private void SpawnEnemy()
		{
			enemy = Instantiate(enemyPrefab).gameObject;
			enemy.transform.position = enemyPosition.position;
			enemy.transform.localScale = new Vector3(enemyScale, enemyScale, enemyScale);
			enemy.transform.LookAt(playerPosition);
		}
	}
}