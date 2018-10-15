using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Apply a small rotational force to Objects
/// </summary>
public class ObjectRotation : MonoBehaviour
{
    /// <summary>
    /// Rotational Force to apply to Objects
    /// </summary>
    public Vector3 m_rotationalForce;

    private void Start()
    {
        m_rotationalForce = new Vector3(Random.Range(0.0f, 180.0f), Random.Range(0.0f, 180.0f), Random.Range(0.0f, 180.0f));
        Debug.LogWarning("RotationalForce: " + m_rotationalForce);
    }

    private void Update()
    {
        transform.Rotate(m_rotationalForce * Time.deltaTime);
    }

}
