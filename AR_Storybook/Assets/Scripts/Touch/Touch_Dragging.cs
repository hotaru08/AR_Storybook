using UnityEngine;
using System.Collections;
using ATXK.CustomVariables;

public class Touch_Dragging : MonoBehaviour
{
    /// <summary>
    /// Flag to trigger dragging
    /// </summary>
    private bool m_bisDrag = false;

    /// <summary>
    /// Getting Screen coords and offset from one object to another
    /// </summary>
    private Vector3 m_screenSpace;
    //private Vector3 m_offset;
    private const float m_yOffset = 0.2f;

    /// <summary>
    /// The Object that will be dragged
    /// </summary>
    public GameObject m_target;

    /// <summary>
    /// Speed of which Object will be dragged 
    /// Will change this to Player Health when Player is done
    /// </summary>
    public float m_dragSpeed;

    /// <summary>
    /// Function to get the Object's screen position and offset from Object's pos to mouse world space pos
    /// </summary>
    public void DragObject(GameObject _selected)
    {
        m_bisDrag = true;

        // set target obj to be selected ( for dragging )
        m_target = _selected;

        // get selected obj screen space pos
        m_screenSpace = Camera.main.WorldToScreenPoint(_selected.transform.position);
        // when u select the sides, the offset will allow moving from there ( no sudden jump to center )
        //m_offset = _selected.transform.position - 
        //    Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_screenSpace.z));
    }

    /// <summary>
    /// Works for most platforms, when pointer moves this function is called
    /// </summary>
    public void OnMouseDrag()
    {
        if (!m_bisDrag) return;

        // get position of pointer in screen space
        Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_screenSpace.z);

        // get the pos of pointer in world space
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace);

        // Update the pos of selected obj according to the pos of the pointer in world space
        m_target.transform.position = new Vector3(curPosition.x, m_yOffset , curPosition.z);
    }
}