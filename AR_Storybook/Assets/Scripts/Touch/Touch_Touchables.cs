using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class for objects that can be selected
/// </summary>
public class Touch_Touchables : MonoBehaviour
{
    /// <summary>
    /// This determines what kind of interactions will be available to that touchable
    /// </summary>
    public enum TOUCH_STATES
    {
        NONE = 0,
        SCALE,
        DRAG,
        ROTATE,
        ALL
    }
    public TOUCH_STATES m_state = 0;

    /// <summary>
    /// Get Gameobjects animation ( if have )
    /// </summary>
    //[SerializeField]
    public Animator m_animator;

    /******************* Various Interaction Functions *******************/
    /// <summary>
    /// Single Selection Mode
    /// - can only select one Gameobject
    /// </summary>
    public GameObject Selection(GameObject _objRayHit)
    {
        if (_objRayHit == null) // nth hit
            return null;

        GameObject temp = null;
        // assign new selected object
        temp = _objRayHit;
        // assign new material to object
        temp.GetComponentInChildren<Renderer>().material.color = Color.red;
        // play animation if they have 
        if (m_animator)
            m_animator.SetTrigger("SelectionAnimation");

        return temp;
    }

    public void Scaling()
    {
        Debug.Log("This function will do scaling");
    }

    public void Dragging(GameObject _selectedOBJTransform, Vector3 _objScreenPos, Vector3 _offset)
    {
        Debug.Log("This function will do Dragging");
        Debug.Log("Obj in screen space: " + _objScreenPos);
        Debug.Log("offset: " + _offset);

        //keep track of the mouse position
        Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _objScreenPos.z);

        //convert the screen mouse position to world point and adjust with offset
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + _offset;

        //update the position of the object in the world
        _selectedOBJTransform.transform.position = curPosition;

        if (m_animator)
            m_animator.SetBool("DraggingAnimation", true);
    }

    public void Rotating()
    {
        Debug.Log("This function will do Rotating");

    }

    public void Transforming()
    {
        Debug.Log("This function will do all :D");
    }

    public void Reset()
    {
        if (m_animator)
        {
            m_animator.SetBool("DraggingAnimation", false);
        }
    }
}
