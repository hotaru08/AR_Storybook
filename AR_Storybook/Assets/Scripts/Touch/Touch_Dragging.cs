using UnityEngine;
using System.Collections;
using ATXK.CustomVariables;

public class Touch_Dragging : MonoBehaviour
{
    /// <summary>
    /// Flag to trigger dragging
    /// </summary>
    private bool _mouseState;

    /// <summary>
    /// Getting Screen coords and offset from one object to another
    /// </summary>
    private Vector3 m_screenSpace;
    private Vector3 m_offset;

    /// <summary>
    /// The Object that will be dragged
    /// </summary>
    public GameObject m_target;

    /// <summary>
    /// Speed of which Object will be dragged 
    /// Will change this to Player Health when Player is done
    /// </summary>
    public float m_dragSpeed;

    private void Awake()
    {
        m_dragSpeed = 1.0f;
    }

    /// <summary>
    /// Function to get the Object's screen position and offset from Object's pos to mouse world space pos
    /// </summary>
    public void DragObject(GameObject _selected)
    {
        _mouseState = true;
        m_target = _selected;
        m_screenSpace = Camera.main.WorldToScreenPoint(_selected.transform.position);
        m_offset = _selected.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_screenSpace.z));
    }

    public void OnMouseDrag()
    {
        if (!_mouseState) return;

        //keep track of the mouse position
        var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_screenSpace.z);

        //convert the screen mouse position to world point and adjust with offset
        var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + m_offset;

        //update the position of the object in the world
        m_target.transform.position = new Vector3(curPosition.x, curPosition.y, 0.0f);
        //m_target.transform.position *= m_dragSpeed * Time.deltaTime;
    }
}