using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollbg : MonoBehaviour
{
    public float ScrollingSpeed;
    Vector2 startPos;

    [SerializeField]
    private GameObject m_sky;
    private Camera m_camera;

    // Use this for initialization
    void Start ()
    {
        startPos = transform.position;
        m_camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += Vector3.right * Time.deltaTime * ScrollingSpeed;
        if (transform.position.x + transform.localScale.x * 0.5f 
            > m_sky.transform.position.x + m_sky.transform.localScale.x * 0.5f)
        {
            transform.position = startPos;
        }
    }
}
