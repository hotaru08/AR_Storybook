using ATXK.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For the movement of the Camera ( for testing ReverseControls of Player )
/// </summary>
public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float m_cameraSpeed;

    [SerializeField]
    private Camera m_camera;

    /// <summary>
    /// Events to trigger something in other scripts
    /// </summary>
    [SerializeField]
    private ES_Event[] m_eventsToSend;


    /// <summary>
    /// Unity Update Function
    /// </summary>
    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            m_camera.transform.position += Vector3.left * Time.deltaTime * m_cameraSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_camera.transform.position -= Vector3.left * Time.deltaTime * m_cameraSpeed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            m_camera.transform.position += Vector3.up * Time.deltaTime * m_cameraSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_camera.transform.position -= Vector3.up * Time.deltaTime * m_cameraSpeed;
        }
    }
}
