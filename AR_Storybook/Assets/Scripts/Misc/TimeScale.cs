using ATXK.EventSystem;
using UnityEngine;

public class TimeScale : MonoBehaviour
{
    public void SetTimeScale(float _value)
    {
        Time.timeScale = Mathf.Clamp01(_value);
    }
}
