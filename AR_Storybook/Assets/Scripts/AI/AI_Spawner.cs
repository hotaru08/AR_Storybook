using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using ATXK.EventSystem;

/// <summary>
/// Spawner of items ( this is bad im sorry :( )
/// </summary>
public class AI_Spawner : MonoBehaviour
{
    [Tooltip("Number of GameObjects spawned per batch.")]
    [SerializeField]
    private float m_batchSize;

    //[Tooltip("Frequency in hertz for individual objects in each batch.")]
    //[SerializeField]
    //private int m_itemSpawnFrequency;

    //[Tooltip("Frequency in hertz for each batch.")]
    //[SerializeField]
    //private int m_batchSpawnFrequency;

    [Tooltip("Range for object spawning.")]
    [SerializeField]
    private float m_spawnPositionRange;

    [Tooltip("List of prefabs to spawn.")]
    [SerializeField]
    private List<GameObject> m_spawnPrefabs;

    /// <summary>
    /// Times in between each item spawn and batch spawn
    /// </summary>
    public float m_timeBetweenSpawns, m_timeBetweenBatches;

    [SerializeField]
    private float m_maxSpawnRandValue;
    [SerializeField]
    private float m_maxBatchRandValue;

    [SerializeField]
    private ES_Event_Abstract[] m_eventsToSend;

    private List<GameObject> m_spawnedProjectiles = new List<GameObject>();

    private void OnEnable()
    {
        // Initialise ( higher frequency, shorter time between )
        //timeBetweenSpawns = 1f / m_itemSpawnFrequency;
        //timeBetweenBatches = 1f / m_batchSpawnFrequency;

        // Start Spawning Loop
        StartCoroutine(SpawnLoop());
    }

    private void OnDisable()
    {
        if (m_spawnedProjectiles.Count.Equals(0))
            return;

        foreach (GameObject _obj in m_spawnedProjectiles)
        {
            Destroy(_obj);
        }
        m_spawnedProjectiles.Clear();
    }

    /// <summary>
    /// Spawning of items loop
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            m_timeBetweenSpawns = Random.Range(1f, m_maxSpawnRandValue);
            m_timeBetweenBatches = Random.Range(1f, m_maxBatchRandValue);

            for (int i = 0; i < m_batchSize; i++)
            {
                yield return new WaitForSeconds(m_timeBetweenSpawns);

                GameObject spawned = Instantiate(m_spawnPrefabs[Random.Range(0, m_spawnPrefabs.Count)], transform);
                m_spawnedProjectiles.Add(spawned);

                Vector3 spawnPos = transform.position;
                spawnPos.x += Random.Range(-m_spawnPositionRange, m_spawnPositionRange);
                //spawnPos.y += Random.Range(-m_spawnPositionRange, m_spawnPositionRange);
                spawnPos.z += Random.Range(-m_spawnPositionRange, m_spawnPositionRange);
                spawned.transform.position = spawnPos;

                Destroy(spawned, 10f);

                // Raise Event_ChangeToAttack
                m_eventsToSend[0].RaiseEvent();

            }

            //Debug.Log("List: " + m_spawnedProjectiles.Count);
            yield return new WaitForSeconds(m_timeBetweenBatches);
        }

    }
}
