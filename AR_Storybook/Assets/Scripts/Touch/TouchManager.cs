using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ATXK.Helper;
using ATXK.EventSystem;

/// <summary>
/// A manager to handle Touch events
/// </summary>
public class TouchManager : SingletonBehaviour<TouchManager>
{
    /// <summary>
    /// Stack of gameobjects that is touched by ray
    /// </summary>
    private GameObject m_selectedObject;

    /// <summary>
    /// Previously selected object settings
    /// </summary>
    private GameObject m_prevSelectedObject;

    /// <summary>
    /// Ray and RayHit
    /// </summary>
    private Ray m_ray;
    private RaycastHit m_rayHitInfo;

    /// <summary>
    /// Event Array to store events that maybe raised upon interaction
    /// </summary>
    [SerializeField]
    private ES_Event[] m_eventsToSend;

    private bool m_bUpdate = true;

    /// <summary>
    /// Unity Update Function
    /// </summary>
    void Update()
    {
        if (!m_bUpdate) return;

#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            m_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount <= 0) return;

        // When there is touches
        if (Input.GetTouch(0).phase.Equals(TouchPhase.Began))
        {
             m_ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
#endif
            // TODO : Set event to set bool to true, only then can invoke this event ( eg. BattleSceneGameManager )
            // Raise Event_Jump
            m_eventsToSend[0].Invoke();
            

            // ---------- If Ray casted hit something
            if (Physics.Raycast(m_ray, out m_rayHitInfo))
            {
                // ---------- return if not Touchables
                if (m_rayHitInfo.collider.gameObject.GetComponent<Touch_Touchables>() == null)
                {
                    Debug.Log("Not a selectable");
                    return;
                }

                // ---------- Selection 
                // set prev to be selected object
                if (m_selectedObject)
                {
                    m_prevSelectedObject = m_selectedObject;
                    ResetSelected(m_prevSelectedObject);
                }
                m_selectedObject = m_rayHitInfo.collider.gameObject.GetComponent<Touch_Touchables>().
                                                         Selection(m_rayHitInfo.collider.gameObject);

                // ---------- Changing Mesh
                m_selectedObject.GetComponent<Touch_Touchables>().ChangeMesh();
            }
            else
            {
                ResetSelected(m_selectedObject);
                Debug.Log("m_selectedObject is now empty");
            }
            Debug.DrawLine(m_ray.origin, m_rayHitInfo.point, Color.red);

            // ---------- If selected, handle its state
#if UNITY_EDITOR || UNITY_STANDALONE
        }
        else if (m_selectedObject && Input.GetMouseButton(0))
            HandleStates(m_selectedObject.GetComponent<Touch_Touchables>().m_state, m_rayHitInfo);
        else if (m_selectedObject && Input.GetMouseButtonUp(0))
        {
            ResetSelected(m_selectedObject);
        }

#elif UNITY_ANDROID || UNITY_IOS
        }
        else if (m_selectedObject && Input.touchCount > 0)
            HandleStates(m_selectedObject.GetComponent<Touch_Touchables>().m_state, m_rayHitInfo);
        else if (m_selectedObject && Input.touchCount <= 0)
        {
            ResetSelected(m_selectedObject);
        }
#endif
    }

    /******************* Various Helper Functions *******************/
    /// <summary>
    /// Change the last selected back to normal
    /// </summary>
    private void ResetSelected(GameObject _prev)
    {
        if (_prev == null)
            return;

        _prev.GetComponent<Touch_Touchables>().ResetAnimation(_prev);
        m_selectedObject = null;
        m_prevSelectedObject = null;
    }

    /// <summary>
    /// Based on States, do certain interaction
    /// </summary>
    private void HandleStates(Touch_Touchables.TOUCH_STATES _state, RaycastHit _hitInfo)
    {
        switch (_state)
        {
            case Touch_Touchables.TOUCH_STATES.NONE:
                Debug.Log("Normal Selection Mode");
                break;

#if UNITY_EDITOR || UNITY_STANDALONE
            case Touch_Touchables.TOUCH_STATES.SCALE:
                break;
            case Touch_Touchables.TOUCH_STATES.DRAG:
                if (_hitInfo.collider.name == m_selectedObject.name)
                {
                    m_selectedObject.GetComponent<Touch_Touchables>().Dragging(m_selectedObject);
                }
                break;
            case Touch_Touchables.TOUCH_STATES.ROTATE:
                break;
            case Touch_Touchables.TOUCH_STATES.ALL:
                m_selectedObject.GetComponent<Touch_Touchables>().Dragging(m_selectedObject);
                break;

#elif UNITY_ANDROID || UNITY_IOS
            case Touch_Touchables.TOUCH_STATES.SCALE:
                // Get first and second touch for scale offset
                if (Input.touchCount == 2)
                    m_selectedObject.GetComponent<Touch_Touchables>().Scaling(m_selectedObject, Input.GetTouch(0), Input.GetTouch(1));
                break;
            case Touch_Touchables.TOUCH_STATES.DRAG:
                if (_hitInfo.collider.name == m_selectedObject.name)
                {
                    m_selectedObject.GetComponent<Touch_Touchables>().Dragging(m_selectedObject);
                }
                break;
            case Touch_Touchables.TOUCH_STATES.ROTATE:
                if (Input.touchCount == 2)
                    m_selectedObject.GetComponent<Touch_Touchables>().Rotating(m_selectedObject);
                break;
            case Touch_Touchables.TOUCH_STATES.ALL:
                m_selectedObject.GetComponent<Touch_Touchables>().Dragging(m_selectedObject);
                if (Input.touchCount == 2)
                {
                    m_selectedObject.GetComponent<Touch_Touchables>().Rotating(m_selectedObject);
                    m_selectedObject.GetComponent<Touch_Touchables>().Scaling(m_selectedObject, Input.GetTouch(0), Input.GetTouch(1));
                }
                break;
#endif
        }
    }

    /// <summary>
    /// Responses to Events received
    /// </summary>
    public void EventReceived(bool _value)
    {
        m_bUpdate = _value;
    }
}
