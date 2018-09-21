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
    /// Click twice to deselect ( maybe ??? )
    /// </summary>
    private bool m_bIsSelected = false;
    public bool IsSelected
    {
        set { m_bIsSelected = value; }
        get { return m_bIsSelected; }
    }

    /******************* Various Interaction Modes *******************/
    public void Scaling()
    {
        Debug.Log("This function will do scaling");
    }

    public void Dragging(Transform _selectedOBJTransform, Vector3 _newPos)
    {
        Debug.Log("World Space Pos: " + _newPos);
        Debug.Log("Selected obj Pos: " + _selectedOBJTransform.position);
        Debug.Log("This function will do Dragging");

        _selectedOBJTransform.localPosition = new Vector3(_selectedOBJTransform.position.x, _newPos.y, _selectedOBJTransform.position.z);
    }

    public void Rotating()
    {
        Debug.Log("This function will do Rotating");

    }

    public void Transforming()
    {
        Debug.Log("This function will do all :D");
    }
}
