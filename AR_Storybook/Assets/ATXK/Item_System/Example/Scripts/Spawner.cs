namespace ATXK.ItemSystem.Examples
{
	using UnityEngine;
	using System.Collections.Generic;
	using System.Collections;

	public class Spawner : MonoBehaviour
	{
		[Tooltip("Number of GameObjects spawned per batch.")]
		[SerializeField] float batchSize;
		[Tooltip("Frequency in hertz for individual objects in each batch.")]
		[SerializeField] int itemSpawnFrequency;
		[Tooltip("Frequency in hertz for each batch.")]
		[SerializeField] int batchSpawnFrequency;
		[Tooltip("Range for object spawning.")]
		[SerializeField] float spawnPositionRange;
		[Tooltip("List of prefabs to spawn.")]
		[SerializeField] List<GameObject> spawnPrefabs;

		float timeBetweenSpawns, timeBetweenBatches;

		private void Start()
		{
			timeBetweenSpawns = 1f / itemSpawnFrequency;
			timeBetweenBatches = 1f / batchSpawnFrequency;

			StartCoroutine(SpawnLoop());
		}

		IEnumerator SpawnLoop()
		{
			while(true)
			{
				for(int i = 0; i < batchSize; i++)
				{
					GameObject spawned = Instantiate(spawnPrefabs[Random.Range(0, spawnPrefabs.Count)], transform);

					Vector3 spawnPos = transform.position;
					spawnPos.x += Random.Range(-spawnPositionRange, spawnPositionRange);
					spawnPos.y += Random.Range(-spawnPositionRange, spawnPositionRange);
					spawnPos.z += Random.Range(-spawnPositionRange, spawnPositionRange);
					spawned.transform.position = spawnPos;

					Destroy(spawned, 20f);

					yield return new WaitForSeconds(timeBetweenSpawns);
				}

				yield return new WaitForSeconds(timeBetweenBatches);
			}
		}
	}
}