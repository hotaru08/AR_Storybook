using ATXK.EventSystem;
using UnityEngine;

public class TimeScale : MonoBehaviour
{
    [SerializeField] private ES_Event_Float m_TimeScaleValue;

    public void SetTimeScale(float _value)
    {
        Time.timeScale = Mathf.Clamp01(_value);
    }
}
