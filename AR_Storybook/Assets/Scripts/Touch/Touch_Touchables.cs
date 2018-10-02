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
    [SerializeField]
    private Animator m_animator;

    /// <summary>
    /// Get mesh of Gameobject ( if have )
    /// </summary>
    [SerializeField]
    private Material m_oldmat, m_newmat;
    private bool m_change = false;

    /// <summary>
    /// The root of the hierachy this gameObject is in
    /// </summary>
    public GameObject rootObject;

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
        // set to change mesh
        m_change = !m_change;
        // play animation if they have 
        if (m_animator)
            m_animator.SetTrigger("SelectionAnimation");
        return temp;
    }

    /// <summary>
    /// Changing of Mesh of selected Gameobject
    /// </summary>
    public void ChangeMesh()
    {
        // no mesh to change
        if (m_oldmat == null || m_newmat == null)
            return;

        if (m_change)
            GetComponent<Renderer>().material = m_newmat;
        else
            GetComponent<Renderer>().material = m_oldmat;
    }

    /// <summary>
    /// Scaling Mode
    /// - Scales up and down selected Gameobject
    /// </summary>
    public void Scaling(GameObject _obj, Touch _one, Touch _two)
    {
        // Add Component
        if (_obj.GetComponent<Touch_Scaling>() == null)
        {
            Debug.LogWarning("Adding Scaling Component");
            _obj.AddComponent<Touch_Scaling>();
        }
        // Use Function to Scale
        _obj.GetComponent<Touch_Scaling>().ChangeScaling(_obj, _one, _two);
    }

    /// <summary>
    /// Dragging Mode
    /// - Drag selected Gameobject along x and z axis
    /// </summary>
    public void Dragging(GameObject _selectedObject)
    {
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

    /// <summary>
    /// Rotating Mode
    /// - Rotate in both directions, by y axis 
    /// </summary>
    public void Rotating(GameObject _obj)
    {
        // Add Component
        if (_obj.GetComponent<Touch_Rotating>() == null)
        {
            Debug.LogWarning("Adding Rotating Component");
            _obj.AddComponent<Touch_Rotating>();
        }
        // Use Function to Rotate

    }

    /// <summary>
    /// Reset all animations when not selected
    /// </summary>
    public void ResetAnimation(GameObject _obj)
    {
        if (!m_animator) return;

        if (m_state == TOUCH_STATES.DRAG)
        {
            m_animator.SetBool("DraggingAnimation", false);
            _obj.GetComponent<Touch_Dragging>().enabled = false;
        }
    }

}
