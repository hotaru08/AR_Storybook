using ATXK.EventSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Scene Changing with loading
/// </summary>
public class ChangeScene : MonoBehaviour
{
    [Header("Events to Send")]
    [SerializeField] private ES_Event_Default m_triggerLoading;

    /// <summary>
    /// Use this function to change scenes 
    /// </summary>
    /// <param name="_sceneName">Name of the Scene to change</param>
    public void ChangingScene(string _sceneName)
    {
        //SceneManager.LoadSceneAsync(_sceneName);
        StartCoroutine(LoadAsynchronously(_sceneName));
    }

    /// <summary>
    /// Reloads the Current Active scene
    /// </summary>
    public void ReloadScene()
    {
        ChangingScene(SceneManager.GetActiveScene().name);
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

    /// <summary>
    /// Coroutine to load the application in the background while a loading screen is shown
    /// </summary>
    /// <param name="_sceneName">Name of scene to change to</param>
    private IEnumerator LoadAsynchronously(string _sceneName)
    {
        // Run Scene in background
        AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneName);

        // Raise event to set screen active
        if (m_triggerLoading != null)
            m_triggerLoading.RaiseEvent();

        while (!operation.isDone)
        {
            yield return null;
        }
    }

}
