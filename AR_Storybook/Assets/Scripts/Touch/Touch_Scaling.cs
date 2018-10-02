using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch_Scaling : MonoBehaviour
{
    /// <summary>
    /// Rate of which GameObject will be scaled
    /// </summary>
    private float m_scaleSpeed = 0.1f;
    public float ScaleSpeed
    {
        set { m_scaleSpeed = value; }
        get { return m_scaleSpeed; }
    }

    /// <summary>
    /// Variables used for scaling 
    /// - the position of the touch in the previous frame
    /// - the difference between the 2 touch ( prev and current frame )
    /// - the difference to scale by 
    /// </summary>
    private Vector3 m_firstPrevPos, m_secondPrevPos;
    private float m_prevDifference, m_currdifference;
    private float m_magnitudeDifference;

    /// <summary>
    /// Calls this function to increase / decrease the scale of the object selected
    /// </summary>
    public void ChangeScaling(GameObject _objToChange, Touch _firstTouch, Touch _secondTouch)
    {
        // Find the position in the previous frame of each touch.
        m_firstPrevPos = _firstTouch.position - _firstTouch.deltaPosition;
        m_secondPrevPos = _secondTouch.position - _secondTouch.deltaPosition;
        Debug.LogWarning("Position of touches in last frame: " + m_firstPrevPos + "/" + m_secondPrevPos);

        // Find the distance between the touches in each frame.
        m_prevDifference = (m_firstPrevPos - m_secondPrevPos).magnitude;
        m_currdifference = (_firstTouch.position - _secondTouch.position).magnitude;
        Debug.LogWarning("Diff prev: " + m_prevDifference + "/ curr: " + m_currdifference);

        // Find the difference in the distances between each frame.
        m_magnitudeDifference = m_currdifference - m_prevDifference;
        Debug.LogWarning("Diff in Mag: " + m_magnitudeDifference + " which will be how much we scale");

        // Alter the size of the Gameobject according to distance between touches
        _objToChange.transform.localScale += new Vector3(m_magnitudeDifference, m_magnitudeDifference, m_magnitudeDifference) * Time.deltaTime * m_scaleSpeed;
        Debug.LogWarning("Scaling: " + _objToChange.transform.localScale);
    }
}
