using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to make continuous scrolling in 3D world
/// </summary>
public class ScrollingObjects : MonoBehaviour
{
    [Header("Objects to be spawned")]
    [SerializeField] private List<GameObject> m_spawnList;
    [SerializeField] private int m_PoolSize = 10;

    [Header("Ground to travel along")]
    [SerializeField] private Transform m_TravelAlongObject;

    [Header("Variables")]
    [SerializeField] private float m_movementSpeed;
    private Queue<GameObject> m_movingObjects;
    private GameObject m_currObject;
    private int index, m_activeIndex;

    /// <summary>
    /// Collections to store spawned objects 
    /// Queue - For linear selection 
    /// List - For random selection
    /// </summary>
    private List<GameObject> m_spawnedObjectList;
    //private Queue<GameObject> m_spawnedObjects;

    private void Start()
    {
        // Intialise
        index = 0;
        m_activeIndex = 0;
        m_movingObjects = new Queue<GameObject>();
        m_spawnedObjectList = new List<GameObject>();
        //m_spawnedObjects = new Queue<GameObject>();
        SpawnObjectPool();

        // Set current object to be first in List / Queue
        m_currObject = m_spawnedObjectList[0];
        m_spawnedObjectList.RemoveAt(0);
        //m_currObject = m_spawnedObjects.Dequeue();

        m_currObject.SetActive(true);
        m_movingObjects.Enqueue(m_currObject);
    }

    private void Update()
    {
        // If the current pool of objects is used ( all active ), spawn another pool of objects
        if (m_movingObjects.Count >= m_PoolSize)
        {
            m_PoolSize += m_PoolSize;
            SpawnObjectPool();
        }

        // If current object's position is over bounds, spawn new object and set current to be that
        if (m_currObject.transform.localPosition.z > m_currObject.transform.localScale.z * 1.5f)
        {
            // Random Selection of one object in spawned list
            index = Random.Range(0, m_spawnedObjectList.Count);
            m_currObject = m_spawnedObjectList[index];
            m_spawnedObjectList.RemoveAt(index);

            // Get the first in spawned queue
            //m_currObject = m_spawnedObjects.Dequeue();

            // Set Active
            m_currObject.SetActive(true);
            // Add to moving queue to update 
            m_movingObjects.Enqueue(m_currObject);
        }

        // If first in queue has reached the end, move back to start point
        if (m_movingObjects.Peek().transform.localPosition.z > m_TravelAlongObject.transform.localScale.z)
        {
            // Move _go back to starting position
            m_movingObjects.Peek().transform.position = transform.position;
            // Set inactive
            m_movingObjects.Peek().SetActive(false);
            // Add back to spawning List / Queue
            m_spawnedObjectList.Add(m_movingObjects.Peek());
            //m_spawnedObjects.Enqueue(m_movingObjects.Peek());
            // Remove from queue
            m_movingObjects.Dequeue();
        }

        // Update all objects in MovingObject Queue
        foreach (GameObject _go in m_movingObjects)
        {
            _go.transform.position += transform.forward * Time.deltaTime * m_movementSpeed;
        }
    }

    /// <summary>
    /// Creates a pool of Gameobjects
    /// </summary>
    private void SpawnObjectPool()
    {
        do
        {
            index = Random.Range(0, m_spawnList.Count);

            // Create new GameObject
            GameObject temp = Instantiate(m_spawnList[index], transform);
            temp.SetActive(false);

            // Store to List / Queue
            m_spawnedObjectList.Add(temp);
            //m_spawnedObjects.Enqueue(temp);

            // Increase active index
            ++m_activeIndex;
        }
        while (m_activeIndex < m_PoolSize);
    }
}