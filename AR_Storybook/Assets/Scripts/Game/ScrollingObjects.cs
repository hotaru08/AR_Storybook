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
    private Queue<GameObject> m_spawnedObjects, m_movingObjects;
    private GameObject m_currObject;
    
    private void Start()
    {
        // Intialise
        m_spawnedObjects = new Queue<GameObject>();
        m_movingObjects = new Queue<GameObject>();

        // Initialise a pool of Gameobjects ( reusability )
        int index = 0;
        for (int i = 0; i < m_PoolSize; ++i)
        {
            index = Random.Range(0, m_spawnList.Count);

            // Create new GameObject
            GameObject temp = Instantiate(m_spawnList[index], transform);
            temp.SetActive(false);

            // Store to Queue
            m_spawnedObjects.Enqueue(temp);
        }

        // Set current object to be first in queue
        m_currObject = m_spawnedObjects.Dequeue();
        m_currObject.SetActive(true);
        m_movingObjects.Enqueue(m_currObject);
    }

    private void Update()
    {
        // If current object's position is over bounds, spawn new object and set current to be that
        if (m_currObject.transform.localPosition.z > m_currObject.transform.localScale.z * 1.5f)
        {
            m_currObject = m_spawnedObjects.Dequeue();
            m_currObject.SetActive(true);
            m_movingObjects.Enqueue(m_currObject);
        }

        // Update all objects in MovingObject Queue
        foreach (GameObject _go in m_movingObjects)
        {
            // If _go has reached the end, move back to start point
            if(_go.transform.localPosition.z > m_TravelAlongObject.transform.localScale.z)
            {
                // Remove from queue
                m_movingObjects.Dequeue();
                // Move _go back to starting position
                _go.transform.position = transform.position;
                // Set inactive
                _go.SetActive(false);
                // Add back to spawning queue
                m_spawnedObjects.Enqueue(_go);
                return;
            }
            
            _go.transform.position += transform.forward * Time.deltaTime * m_movementSpeed;
        }

        if (m_movingObjects[0])
    }

}
