using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch_Scaling : MonoBehaviour
{
    /// <summary>
    /// Rate of which GameObject will be scaled
    /// </summary>
    [SerializeField] float m_scaleSpeed;

    /// <summary>
    /// Variables used for scaling 
    /// - the position of the touch in the previous frame
    /// - the difference between the 2 touch ( prev and current frame )
    /// - the difference to scale by 
    /// </summary>
    private Vector2 m_firstPrevPos, m_secondPrevPos;
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

        // Find the distance between the touches in each frame.
        m_prevDifference = (m_firstPrevPos - m_secondPrevPos).magnitude;
        m_currdifference = (_firstTouch.position - _secondTouch.position).magnitude;

        // Find the difference in the distances between each frame.
        m_magnitudeDifference = m_currdifference - m_prevDifference;

        // Alter the size of the Gameobject according to distance between touches
        if (_objToChange.transform.localScale.x <= 0.1f &&
            _objToChange.transform.localScale.x <= 0.1f &&
            _objToChange.transform.localScale.x <= 0.1f)
        {
            _objToChange.transform.localScale = Vector3.zero;
        }
        _objToChange.transform.localScale += new Vector3(m_magnitudeDifference, m_magnitudeDifference, m_magnitudeDifference) * Time.deltaTime * m_scaleSpeed;
    }
}
