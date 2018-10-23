using ATXK.EventSystem;
using ATXK.Helper;
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
    private ES_Event_Bool[] m_eventsToSend;
    private bool m_bIsNegativeZ;
    private PlayerManager m_player;

    private void Start()
    {
        m_bIsNegativeZ = true;
    }

    /// <summary>
    /// Unity Update Function
    /// </summary>
    private void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        // ================================== Movement of Camera
        if (Input.GetKey(KeyCode.W))
        {
            m_camera.transform.position += m_camera.transform.forward * Time.deltaTime * m_cameraSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_camera.transform.position -= m_camera.transform.forward * Time.deltaTime * m_cameraSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_camera.transform.position -= m_camera.transform.right * Time.deltaTime * m_cameraSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_camera.transform.position += m_camera.transform.right * Time.deltaTime * m_cameraSpeed;
        }

        // ================================== Rotation of Camera
        if (Input.GetKey(KeyCode.J))
        {
            m_camera.transform.Rotate(0.0f, -50.0f * Time.deltaTime, 0.0f);
        }
        if (Input.GetKey(KeyCode.L))
        {
            m_camera.transform.Rotate(0.0f, 50.0f * Time.deltaTime, 0.0f);
        }
        if (Input.GetKey(KeyCode.I))
        {
            m_camera.transform.Rotate(-50.0f * Time.deltaTime, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.K))
        {
            m_camera.transform.Rotate(50.0f * Time.deltaTime, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.U))
        {
            m_camera.transform.Rotate(0.0f, 0.0f, 50.0f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.O))
        {
            m_camera.transform.Rotate(0.0f, 0.0f, -50.0f * Time.deltaTime);
        }
#endif

        // ================================== Update only when there is movement
        if (m_player != null)
        {
            // ------------------ Invoke events 
            if (Vector3.Angle(m_player.transform.forward, m_camera.transform.forward) <= 90.0f
                && m_player.m_laneStyle.Equals(0)
                && m_bIsNegativeZ)
            {
                m_bIsNegativeZ = false;

                m_eventsToSend[0].Invoke(m_bIsNegativeZ);
                //DebugLogger.LogWarning<CameraMovement>("Event Send: " + m_eventsToSend[0].name + " Value: " + m_eventsToSend[0].value);
            }
            else if (Vector3.Angle(m_player.transform.forward, m_camera.transform.forward) > 90.0f
                && m_player.m_laneStyle.Equals(0)
                && !m_bIsNegativeZ)
            {
                m_bIsNegativeZ = true;

                m_eventsToSend[0].Invoke(m_bIsNegativeZ);
                //DebugLogger.LogWarning<CameraMovement>("Event Send: " + m_eventsToSend[0].name + " Value: " + m_eventsToSend[0].value);
            }
        }
    }

    public void EventReceived(Object _value)
    {
        //DebugLogger.Log<CameraMovement>("Event Received with value: " + _value);
        GameObject temp = _value as GameObject;
        m_player = temp.GetComponent<PlayerManager>();
    }
}
