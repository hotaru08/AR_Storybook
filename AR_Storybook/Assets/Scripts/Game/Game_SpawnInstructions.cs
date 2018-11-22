using ATXK.CustomVariables;
using ATXK.EventSystem;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// To Spawn the instructions
/// </summary>
public class Game_SpawnInstructions : MonoBehaviour
{
    /// <summary>
    /// Screens
    /// </summary>
    [Header("Variables")]
    [Tooltip("Instruction Screens to be shown")]
    [SerializeField] private GameObject m_instruction;
    [Tooltip("If any, the first interaction screen ( eg. swipe, tap etc )")]
    [SerializeField] private GameObject m_firstInteractiveScreen;
    [Tooltip("To Spawn only once")]
    [SerializeField] private bool m_triggerSpawnOnce;
    //[SerializeField] private CV_Bool m_SpawnOnce;

    [Header("Events")]
    [Tooltip("The current index/ number for the instructions")]
    [SerializeField] private ES_Event_Int m_instructionIndex;
    [Tooltip("Event to trigger next UI to be shown")]
    [SerializeField] private ES_Event_Abstract m_setMainHUD;
    [Tooltip("Event to trigger something that only works when instructions are done")]
    [SerializeField] private ES_Event_Bool m_start;

    private int prevInstructIndex;

    private void Start()
    {
        prevInstructIndex = m_instructionIndex.Value = 0;

        // To spawn the first time they use
        if (!m_triggerSpawnOnce) return;
        if (PlayerPrefs.GetInt("SpawnOnce").Equals(0))
        {
            m_instructionIndex.RaiseEvent(0);
            m_start.RaiseEvent(false);
            PlayerPrefs.SetInt("SpawnOnce", 1);
        }
        else if (PlayerPrefs.GetInt("SpawnOnce").Equals(1))
        {
            m_start.RaiseEvent(true);
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Set the index of which instruction to show
    /// </summary>
    /// <param name="_index">Index of instruction</param>
    public void SetInstructions(int _index)
    {
        if (_index < prevInstructIndex)
            return;

        // Check if screen is interactive screen, then set overlay gone
        if (m_instruction.transform.GetChild(_index).GetSiblingIndex().Equals(m_firstInteractiveScreen.transform.GetSiblingIndex()))
        {
            if (GetComponent<Image>())
                GetComponent<Image>().enabled = false;
        }

        // Set next screen active
        if (_index >= m_instruction.transform.childCount) return;
        m_instruction.transform.GetChild(_index).gameObject.SetActive(true);

        // set previous screen inactive
        if (_index <= 0) return;
        m_instruction.transform.GetChild(_index - 1).gameObject.SetActive(false);

        // set value of screen
        prevInstructIndex = m_instructionIndex.Value;
        m_instructionIndex.Value = _index;
    }

    /// <summary>
    /// Set something to happen when its the last instruction
    /// </summary>
    public void SetLastInstruction(bool _value)
    {
        if (!_value) return;

        // Remove the last instruction
        if (m_instruction.transform.GetChild(m_instruction.transform.childCount - 1) != null)
            m_instruction.transform.GetChild(m_instruction.transform.childCount - 1).gameObject.SetActive(false);

        // Set index to be child count
        m_instructionIndex.Value = m_instruction.transform.childCount;

        // Spawn any next UI
        if (m_setMainHUD != null)
            m_setMainHUD.RaiseEvent();

        // Set intructions to be inactive
        gameObject.SetActive(false);
    }
}
