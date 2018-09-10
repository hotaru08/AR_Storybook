using System;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script to handle visualisation of Gameobject
/// - List of GameObject to render from 
/// - ** See if can store and load list of scenes
/// </summary>
public class ARImageVisualiser : MonoBehaviour
{
    /// <summary>
    /// The AugmentedImage to visualize.
    /// </summary>
    public AugmentedImage m_image;

    /// <summary>
    /// List to store Gameobject that will be shown when tracked
    /// </summary>
    /// public List<Scene> m_sceneList = new List<Scene>();
    public List<GameObject> m_objectList;

    /// <summary>
    /// Debug text ( for Debugging )
    /// </summary>
    public Text m_debugText;

    /// <summary>
    /// Store the generated object
    /// </summary>
    private GameObject m_generatedObject;

    /// <summary>
    /// Unity Start Method
    /// </summary>
    public void Awake()
    {
        m_debugText = GameObject.FindGameObjectWithTag("Debug").GetComponent<Text>();
    }

    /// <summary>
    /// The Unity Update method.
    /// </summary>
    public void Update()
    {
        /// If no Image is tracked / not in tracking mode ( AR Camera )
        if (m_image == null || m_image.TrackingState != TrackingState.Tracking)
        {
            m_debugText.text = "No Image Tracked";
            return;
        }

        /// 1)
        /// Switch Mesh of this Visualiser to be Object that is assigned to the image
        /// Database index => object index in list
        //this.GetComponent<MeshFilter>().mesh = m_objectList[m_image.DatabaseIndex].GetComponent<MeshFilter>().mesh;
        //this.GetComponent<Renderer>().material = m_objectList[m_image.DatabaseIndex].GetComponent<Renderer>().material;

        /// Render Debug Text
        //m_debugText.text = "Pos: " + transform.position + "\n"
        //                 + "Camera Pos: " + Camera.main.transform.position + "\n"
        //                 + "Mesh: " + this.GetComponent<MeshFilter>().mesh + "\n"
        //                 + "Image Name: " + m_image.Name + "\n"
        //                 + "GameObject: " + gameObject.name.ToString() + "\n"
        //                 + "Active: " + gameObject.activeSelf.ToString();

        /// 2)
        /// Generate Gameobject on top of this Visualiser
        /// Instantiate it, and Destroy it ( Database index => object index in list ) 
        /// ** This method will require data to be saved before destroying
        if (m_generatedObject == null)
        {
            m_generatedObject = Instantiate(m_objectList[m_image.DatabaseIndex], this.transform.position, this.transform.rotation);
            m_generatedObject.transform.parent = this.transform;
        }


        m_debugText.text = "Name: " + m_generatedObject.name + "\n"
                         + "Pos: " + m_generatedObject.transform.position + "\n"
                         + "Rot: " + m_generatedObject.transform.rotation + "\n"
                         + "Scale: " + m_generatedObject.transform.localScale + "\n"
                         + "Parent: " + m_generatedObject.transform.parent + "\n"
                         + "Active: " + m_generatedObject.activeSelf;


    }
}

