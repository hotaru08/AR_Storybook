using ATXK.CustomVariables;
using ATXK.EventSystem;
using System.Collections;
using System.Collections.Generic;
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
    [Tooltip("Instruction Screens to be showm")]
    [SerializeField] private GameObject m_instruction;
    [Tooltip("If any, the first interaction screen ( eg. swipe, tap etc )")]
    [SerializeField] private GameObject m_firstInteractiveScreen;
    [Tooltip("The current index/ number for the instructions")]
    [SerializeField] private ES_Event_Int m_instructionIndex;
    [Tooltip("If any, the next UI that is going to be showm")]
    [SerializeField] private ES_Event_Abstract m_setMainHUD;
    private int prevInstructIndex;

    private void Start()
    {
        prevInstructIndex = m_instructionIndex.Value = 0;
    }

    /// <summary>
    /// Set the index of which instruction to show
    /// </summary>
    /// <param name="_index">Index of instruction</param>
    public void SetInstructions(int _index)
    {
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
