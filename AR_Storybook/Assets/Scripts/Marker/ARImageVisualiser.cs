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
	private void Start()
    {
        m_debugText = GameObject.FindGameObjectWithTag("Debug").GetComponent<Text>();
		m_debugText.text = "START\n";
	}

    /// <summary>
    /// Renders the appropriate prefab/model according to what image is set.
    /// </summary>
    private void VisualizeImage()
    {
		GetComponent<Renderer>().enabled = false;

		m_generatedObject = Instantiate(m_objectList[m_image.DatabaseIndex], transform.parent.position, transform.parent.rotation);
		m_generatedObject.transform.parent = transform;
		m_generatedObject.transform.localScale = new Vector3(m_image.ExtentX, 1f, m_image.ExtentZ);

		m_debugText.text = "Name: " + m_generatedObject.name + "\n"
                         + "Pos: " + m_generatedObject.transform.position + "\n"
                         + "Rot: " + m_generatedObject.transform.rotation + "\n"
                         + "Scale: " + m_generatedObject.transform.localScale + "\n"
                         + "Parent: " + m_generatedObject.transform.parent.name + "\n"
                         + "Active: " + m_generatedObject.activeSelf + "\n"
                         + "Tracking State: " + m_image.TrackingState.ToString();
    }

	private void OnEnable()
	{
		if(m_image != null)
		{
			VisualizeImage();
		}
		else
		{
			Debug.LogError("ARImageVisualizer(): m_image is NULL");
		}
	}

	private void OnDisable()
	{
		//Destroy the generated GameObject
		Destroy(m_generatedObject);
	}

	private void OnDestroy()
    {
		//Destroy the generated GameObject
		Destroy(m_generatedObject);
        //Destroy the GameObject holding this component
        Destroy(gameObject);
    }
}

