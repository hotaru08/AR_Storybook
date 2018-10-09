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
    /// Use this function to change scenes 
    /// </summary>
    /// <param name="_sceneName">Name of the Scene to change</param>
    public void ChangingScene(string _sceneName)
    {
        SceneManager.LoadSceneAsync(_sceneName);
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
