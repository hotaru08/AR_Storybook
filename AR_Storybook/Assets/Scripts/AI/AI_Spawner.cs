﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using ATXK.EventSystem;

/// <summary>
/// Spawner of items
/// </summary>
public class AI_Spawner : MonoBehaviour
{
    [Tooltip("Number of GameObjects spawned per batch.")]
    [SerializeField]
    private float m_batchSize;

    [Tooltip("Frequency in hertz for individual objects in each batch.")]
    [SerializeField]
    private int m_itemSpawnFrequency;

    [Tooltip("Frequency in hertz for each batch.")]
    [SerializeField]
    private int m_batchSpawnFrequency;

    [Tooltip("Range for object spawning.")]
    [SerializeField]
    private float m_spawnPositionRange;

    [Tooltip("List of prefabs to spawn.")]
    [SerializeField]
    private List<GameObject> m_spawnPrefabs;

    /// <summary>
    /// Times in between each item spawn and batch spawn
    /// </summary>
    private float timeBetweenSpawns, timeBetweenBatches;


    [SerializeField]
    private ES_Event[] m_eventsToSend;

    private void Start()
    {
        // Initialise ( higher frequency, shorter time between )
        timeBetweenSpawns = 1f / m_itemSpawnFrequency;
        timeBetweenBatches = 1f / m_batchSpawnFrequency;

        // Start Spawning Loop
        StartCoroutine(SpawnLoop());
    }

    /// <summary>
    /// Spawning of items loop
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            for (int i = 0; i < m_batchSize; i++)
            {
                GameObject spawned = Instantiate(m_spawnPrefabs[Random.Range(0, m_spawnPrefabs.Count)], transform);

                Vector3 spawnPos = transform.position;
                spawnPos.x += Random.Range(-m_spawnPositionRange, m_spawnPositionRange);
                spawnPos.y += Random.Range(-m_spawnPositionRange, m_spawnPositionRange);
                spawnPos.z += Random.Range(-m_spawnPositionRange, m_spawnPositionRange);
                spawned.transform.position = spawnPos;

                Destroy(spawned, 10f);

                // Raise Event_ChangeToAttack
                m_eventsToSend[0].Invoke();

                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            yield return new WaitForSeconds(timeBetweenBatches);
        }
    }
}
