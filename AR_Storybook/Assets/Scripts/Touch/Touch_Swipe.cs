using ATXK.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch_Swipe : MonoBehaviour
{
    /// <summary>
    /// Enum to determine which direction the swipe is 
    /// </summary>
    public enum SWIPE_DIRECTION
    {
        NONE = 0,
        UP,
        DOWN,
        LEFT, 
        RIGHT
    }
    private SWIPE_DIRECTION m_direction = 0;
    public SWIPE_DIRECTION SwipeDirection { get { return m_direction; } }

    /// <summary>
    /// Boolean to check if there is Touch / Mouse Down
    /// </summary>
    private bool m_isTouching = false;
    public bool IsTouch { get { return m_isTouching; } }

    /// <summary>
    /// Starting Touch Pos
    /// Diff in start and end pos of Touch
    /// </summary>
    private Vector2 m_startTouchPos, m_swipeDelta;

    /// <summary>
    /// The area where user have to cross to consider as a swipe ( in pixels )
    /// </summary>
    [SerializeField]
    private float m_deadzoneDistance = 10;

    private void Update()
    {
        // reset every frame ( prevent continuous update )
        m_direction = SWIPE_DIRECTION.NONE;

        #region Inputs
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            m_isTouching = true;
            // Store the first mouse down pos as starting pos
            m_startTouchPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            m_isTouching = false;
            // when mouse is up, reset
            Reset();
        }
#elif UNITY_ANDROID || UNTIY_IOS
        if (Input.touchCount <= 0) return;

        // When first Touch first touches the screen
        if (Input.touches[0].phase == TouchPhase.Began)
        {
            m_isTouching = true;
            // store the pos as starting pos
            m_startTouchPos = Input.touches[0].position;
        }
        else if (Input.touches[0].phase == TouchPhase.Ended ||
            Input.touches[0].phase == TouchPhase.Canceled)
        {
            m_isTouching = false;
            // When first finger is up, reset
            Reset();
        }
#endif
        #endregion

        #region SwipeDelta
        // Calculate the difference in start and end pos of Touch / Mouse
        if (m_isTouching)
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetMouseButton(0))
                m_swipeDelta = (Vector2)Input.mousePosition - m_startTouchPos;
#elif UNITY_ANDROID || UNITY_IOS
            if (Input.touchCount > 0)
                m_swipeDelta = Input.touches[0].deltaPosition;
#endif
        }
        #endregion

        #region DetermineDirection
        // Check if deadzone is passed
        if (m_swipeDelta.sqrMagnitude > m_deadzoneDistance * m_deadzoneDistance)
        {
            // Determine direction of swipe
            try
            {
                if (Mathf.Abs(m_swipeDelta.x) > Mathf.Abs(m_swipeDelta.y))
                {
                    // Swipe left or right ( x has more displacement )
                    if (m_swipeDelta.x < 0)
                        m_direction = SWIPE_DIRECTION.LEFT;
                    else
                        m_direction = SWIPE_DIRECTION.RIGHT;
                }
                else
                {
                    // Swipe up or down ( y has more displacement )
                    if (m_swipeDelta.y > 0)
                        m_direction = SWIPE_DIRECTION.UP;
                    else
                        m_direction = SWIPE_DIRECTION.DOWN;
                }

                DebugLogger.Log<Touch_Swipe>("Swipe Direction: " + m_direction);

                // reset the start and difference in touch pos so as to prevent continuous updating
                Reset();
            }
            catch (Exception ex)
            {
                DebugLogger.LogError<Touch_Swipe>("Exception Received: " + ex.ToString());
            }
        }
        #endregion
    }

    /// <summary>
    /// Reset variables 
    /// </summary>
    private void Reset()
    {
        m_startTouchPos = m_swipeDelta = Vector2.zero;
        m_isTouching = false;
    }
}
