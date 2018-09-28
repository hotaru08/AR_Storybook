using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This script calculate the current fps and show it to a text ui.
/// </summary>
public class UI_DisplayFPS : MonoBehaviour
{
    private float m_fps = 0.0f;
    public GameObject m_fpsDisplayMesh;

    private void Update()
    {
        m_fps = 1.0f / Time.unscaledDeltaTime;
        m_fpsDisplayMesh.GetComponent<TextMeshProUGUI>().text = "FPS: " + m_fps.ToString("F2");
    }
}