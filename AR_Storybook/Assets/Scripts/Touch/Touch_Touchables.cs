using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class for objects that can be selected ( its bad :( )
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

    public void Dragging(GameObject _selectedObject)
    {
        //Debug.Log("This function will do Dragging");
        // Add Component
        if (_selectedObject.GetComponent<Touch_Dragging>() == null)
        {
            Debug.Log("Adding Component Drag");
            _selectedObject.AddComponent<Touch_Dragging>();
        }
        // Use Function to Drag
        _selectedObject.GetComponent<Touch_Dragging>().DragObject(_selectedObject);


        // Play Dragging Animation
        if (m_animator)
            m_animator.SetBool("DraggingAnimation", true);
    }

    public void Rotating()
    {
        Debug.Log("This function will do Rotating");

    }

    public void Reset()
    {
        // Reset all Animation
        if (m_animator)
        {
            m_animator.SetBool("DraggingAnimation", false);
        }
    }

}
