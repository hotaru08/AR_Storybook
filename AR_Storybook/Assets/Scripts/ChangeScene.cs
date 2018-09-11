using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script handles Scene Changing ( implementation of loading screen if applicable )
/// </summary>
public class ChangeScene : MonoBehaviour
{
    /// <summary>
    /// Changing to SceneMarker
    /// </summary>
    public void ChangeToSceneMarker()
    {
        SceneManager.LoadScene("SceneMarker");
    }

    /// <summary>
    /// Changing to SceneMarkerless
    /// </summary>
    public void ChangeToSceneMarkerless()
    {
        SceneManager.LoadScene("SceneMarkerless");
    }

    /// <summary>
    /// Quit the application ( any device, not working in editor )
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        Debug.Log("Application Quitting ... ");
#endif
        Application.Quit();
    }
}
