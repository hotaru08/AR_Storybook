using ATXK.EventSystem;
using UnityEngine;

/// <summary>
/// To handle the Timescale of the project
/// </summary>
public class TimeScale : MonoBehaviour
{
    /// <summary>
    /// Set Time scale according to value
    /// </summary>
    /// <param name="_value">Value to set to time scale</param>
    public void SetTimeScale(float _value)
    {
        Time.timeScale = Mathf.Clamp01(_value);
    }
}
