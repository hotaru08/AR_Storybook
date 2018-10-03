using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.EventSystem;

public class ListHolder : MonoBehaviour
{
    [Tooltip("List of any GameObjects")]
    public List<GameObject> GOList;

    [Tooltip("An array holding events to be called")]
    public ES_Event[] m_eventsHolder;
}
