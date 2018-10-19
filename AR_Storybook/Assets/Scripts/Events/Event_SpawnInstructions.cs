using ATXK.CustomVariables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// To Spawn the instructions
/// </summary>
public class Event_SpawnInstructions : MonoBehaviour
{
    /// <summary>
    /// Screens
    /// </summary>
    [SerializeField] private GameObject m_instruction;
    [SerializeField] private GameObject m_firstInteractiveScreen;

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
    }

    /// <summary>
    /// Set something to happen when its the last instruction
    /// </summary>
    public void SetLastInstruction(bool _value)
    {
        if (!_value) return;
        m_instruction.transform.GetChild(m_instruction.transform.childCount - 1).gameObject.SetActive(false);
    }
}
