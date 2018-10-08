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
        //originalPos = transform.position;
        startPos = transform.position;
        m_camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //float newPos = Mathf.Repeat(Time.deltaTime * ScrollingSpeed, 50);
        //transform.position = startPos + Vector2.right * newPos;

        transform.position += Vector3.right * Time.deltaTime * ScrollingSpeed;
        if (transform.position.x + transform.localScale.x * 0.5f 
            > m_sky.transform.position.x + m_sky.transform.localScale.x * 0.5f)
        {
            transform.position = startPos;
            //Debug.Log("Out of sky cube");
        }
        //Debug.Log("Pos: " + (transform.position.x + transform.localScale.x).ToString());
        //Debug.Log("Scale: " + transform.localScale.x);
        //Debug.Log("Sky Pos: " + (m_sky.transform.position.x + m_sky.transform.localScale.x * 0.5f).ToString());

        //Debug.Log("Sky screen pos: " + m_camera.WorldToScreenPoint(m_sky.transform.position + m_sky.transform.localScale));
    }
}
