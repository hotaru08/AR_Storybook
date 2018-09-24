using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A manager to handle Touch events ( rip i have nvr made a manager OR have the skill to make a proper manager )
/// </summary>
public class TouchManager : ATXK.SingletonMono<TouchManager>
{
    /// <summary>
    /// Stack of gameobjects that is touched by ray
    /// </summary>
    private GameObject m_selectedObject;
    //private List<GameObject> m_selectedObjectStack;

    /// <summary>
    /// Previously selected object settings
    /// </summary>
    private GameObject m_prevSelectedObject;

    /// <summary>
    /// Ray and RayHit
    /// </summary>
    private Ray m_ray;
    private RaycastHit m_rayHitInfo;

    public Text m_debug;

    private bool _mouseState;
    public Vector3 screenSpace;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            m_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.GetTouch(0).phase.Equals(TouchPhase.Began))
        {
             m_ray = Camera.main.ScreenPointToRay(Input.GetTouch(_touch.fingerId).position);
#endif

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
                m_selectedObject = m_rayHitInfo.collider.gameObject.GetComponent<Touch_Touchables>().Selection(m_rayHitInfo.collider.gameObject);
            }
            else
            {
                ResetSelected(m_selectedObject);
                m_selectedObject = null;
                m_prevSelectedObject = null;
                Debug.Log("m_selectedObjectStack is now empty");
            }
            Debug.DrawLine(m_ray.origin, m_rayHitInfo.point, Color.red);
            // ---------- If selected, handle its state
#if UNITY_EDITOR || UNITY_STANDALONE
        }
        else if (m_selectedObject && Input.GetMouseButton(0))
            HandleStates(m_selectedObject.GetComponent<Touch_Touchables>().m_state, m_rayHitInfo);
        //else if (Input.GetMouseButtonUp(0))
        //{
        //    _mouseState = false;
        //    ResetSelected(m_selectedObject);
        //}

#elif UNITY_ANDROID || UNITY_IOS
        }
        else if (m_selectedObject && Input.touchCount > 0)
            HandleStates(m_selectedObject.GetComponent<Touch_Touchables>().m_state, m_rayHitInfo);
        //else if (Input.touchCount <= 0)
        //{
        //    _mouseState = false;
        //    ResetSelected(m_selectedObject);
        //}
            
#endif

        //if (_mouseState)
        //{
        //    //keep track of the mouse position
        //    var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

        //    //convert the screen mouse position to world point and adjust with offset
        //    var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;

        //    //update the position of the object in the world
        //    m_selectedObject.transform.position = new Vector3(curPosition.x, m_selectedObject.transform.position.y, curPosition.y);

        //    m_selectedObject.GetComponent<Touch_Touchables>().m_animator.SetBool("DraggingAnimation", true);
        //}
    }

    /******************* Various Helper Functions *******************/
    /// <summary>
    /// Change the last selected back to normal
    /// </summary>
    private void ResetSelected(GameObject _prev)
    {
        if (_prev == null)
            return;

        _prev.GetComponentInChildren<Renderer>().material.color = Color.white;
        _prev.GetComponent<Touch_Touchables>().Reset();
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
                m_selectedObject.GetComponent<Touch_Touchables>().Scaling();
                break;
            case Touch_Touchables.TOUCH_STATES.DRAG:
                if (_hitInfo.collider.name == m_selectedObject.name && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    //_mouseState = true;
                    //screenSpace = Camera.main.WorldToScreenPoint(m_selectedObject.transform.position);
                    //offset = m_selectedObject.transform.position 
                    //    - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));

                    
                }
                break;
            case Touch_Touchables.TOUCH_STATES.ROTATE:
                m_selectedObject.GetComponent<Touch_Touchables>().Rotating();
                break;
            case Touch_Touchables.TOUCH_STATES.ALL:
                m_selectedObject.GetComponent<Touch_Touchables>().Transforming();
                break;

#elif UNITY_ANDROID || UNITY_IOS
            case Touch_Touchables.TOUCH_STATES.SCALE:
                    m_selectedObject.GetComponent<Touch_Touchables>().Scaling();
                break;
            case Touch_Touchables.TOUCH_STATES.DRAG:
                if (_hitInfo.collider.name == m_selectedObject.name)
                {
                    m_selectedObject.GetComponent<Touch_Touchables>().Dragging(m_selectedObject.transform,
                                            Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position));
                    m_debug.text = "HELP";
                }
                break;
            case Touch_Touchables.TOUCH_STATES.ROTATE:
                    m_selectedObject.GetComponent<Touch_Touchables>().Rotating();
                break;
            case Touch_Touchables.TOUCH_STATES.ALL:
                    m_selectedObject.GetComponent<Touch_Touchables>().Transforming();
                break;
#endif
        }
    }
}
