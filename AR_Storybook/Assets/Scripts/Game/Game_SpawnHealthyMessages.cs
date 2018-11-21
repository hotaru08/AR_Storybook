using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Spawn Healthy Messages on any screen it is attached to
/// </summary>
public class Game_SpawnHealthyMessages : MonoBehaviour
{
    /// <summary>
    /// Array of messages to be shown
    /// </summary>
    [SerializeField]
    [TextArea(1, 3)]
    private string[] m_messages;

    /// <summary>
    /// Screen to show messages on
    /// </summary>
    [SerializeField] private Text m_textToShowOn;

    /// <summary>
    /// Unity OnEnable Function - select a random message to show
    /// </summary>
    private void OnEnable()
    {
        if (m_textToShowOn == null) return;
        
        m_textToShowOn.text = m_messages[Random.Range(0, m_messages.Length - 1)];
        Debug.Log(m_textToShowOn.text);
    }
}
