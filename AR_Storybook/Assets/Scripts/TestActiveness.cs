using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestActiveness : MonoBehaviour
{
    [SerializeField]
    private GameObject m_cube;

    public void SetActiveness()
    {
        if (m_cube.activeSelf)
            m_cube.SetActive(false);
        else
            m_cube.SetActive(true);
    }

}
