using ATXK.CustomVariables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TriggerButtonUI : MonoBehaviour
{
    [SerializeField]
    private CV_Bool m_buttonMode;

	// Use this for initialization
	void Start ()
    {
        gameObject.SetActive(m_buttonMode.value);
	}
}
