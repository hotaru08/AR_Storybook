namespace ATXK.ItemSystem.Examples
{
	using UnityEngine;
	using System.Collections.Generic;
	using System.Collections;

	public class Spawner : MonoBehaviour
	{
		[SerializeField] float batchSize;
		[SerializeField] int itemSpawnFrequency;
		[SerializeField] int batchSpawnFrequency;
		[SerializeField] float spawnPositionRange;
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

					Destroy(spawned, 10f);

					yield return new WaitForSeconds(timeBetweenSpawns);
				}

				yield return new WaitForSeconds(timeBetweenBatches);
			}
		}
	}
}