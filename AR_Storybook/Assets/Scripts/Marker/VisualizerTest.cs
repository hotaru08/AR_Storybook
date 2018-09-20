using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GoogleARCore;
using UnityEngine;
using UnityEngine.UI;

public class VisualizerTest : MonoBehaviour
{
    /// <summary>
    /// Prefab for visualizing an AugmentedImage.
    /// </summary>
    public GameObject m_arVisualizerPrefab;

	/// <summary>
	/// Main AR session in the scene
	/// </summary>
	public ARCoreSession m_arSession;

	/// <summary>
	/// List of all tracked images.
	/// </summary>
	public List<AugmentedImage> m_trackedImages;

    /// <summary>
    /// Reference to visualized object.
    /// </summary>
    public List<ARImageVisualiser> m_listOfVisualizedObjects;

	/// <summary>
	/// Flag if supposed to only visualize one image at a time, or multiple images. (True = All images in the scene, False = Only one image at a time)
	/// </summary>
	public bool m_visualizeAllImages = false;

	public int m_objectPoolStartSize = 10;

    /// <summary>
    /// Debug texts.
    /// </summary>
    public Text m_debuggingText;
    public Text m_trackingStateDebug;
    public Text m_trackingStateDebug2;

    private void Start()
    {
		//Get current session from session manager
		m_arSession = ARSessionManager.Instance.GetSession();
		//Initialise lists
        m_trackedImages = new List<AugmentedImage>();
		m_listOfVisualizedObjects = new List<ARImageVisualiser>();
		//Set screen to never timeout
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		//Create Object Pool for ARImageVisualiser
		for(int i = 0; i < m_objectPoolStartSize; i++)
		{
			//Create instance of visualizer
			GameObject visualizerObject = Instantiate(m_arVisualizerPrefab);
			visualizerObject.SetActive(false);
			//Get component
			ARImageVisualiser visualizer = visualizerObject.GetComponent<ARImageVisualiser>();

			//Add to object pool
			ObjectPool.Add(visualizer);
		}
	}

	private void Update()
	{
		CheckForImages();

		//DEBUGGING
		m_debuggingText.text = "Update: " + Time.time + "\n";
		m_debuggingText.text += "# TrackedImages: " + m_trackedImages.Count + "\n";
		m_debuggingText.text += "# Visualizers: " + m_listOfVisualizedObjects.Count + "\n";
		m_debuggingText.text += "# ARObjects: " + GameObject.FindGameObjectsWithTag("ARObject").Length + "\n";
		m_debuggingText.text += "# ARAnchor: " + FindObjectsOfType(typeof(Anchor)).Length + "\n";
	}

	void CheckForImages()
	{
		//Only get new tracked images
		Session.GetTrackables(m_trackedImages, TrackableQueryFilter.All);
		//If list is populated, that means in this frame there are images being tracked
		if (m_trackedImages.Count > 0)
		{
			//Currently tracking more than one image
			if (m_trackedImages.Count > 1)
			{
				//Sort the images from oldest to newest (smallest time to largest time)
				m_trackedImages.Sort((x, y) => x.GetTimeCreated().CompareTo(y.GetTimeCreated()));

				//We should remove all old images and keep the latest one only
				if (!m_visualizeAllImages)
				{
					ARSessionManager.Instance.ResetSession();
				}
			}

			//Remove objects in the scene
			RemoveVisualizedObjects();
			//Create a visualizer and add it to the scene
			CreateVisualizerObjects(m_trackedImages);
		}
	}

	/// <summary>
	/// Creates a single ARImageVisualiser and adds it to a reference list.
	/// </summary>
	void CreateVisualizerObjects_OLD(AugmentedImage _imageToVisualize)
    {
		////Create an anchor at centre of image to ensure that transformation is relative to real world
		//Anchor anchor = _imageToVisualize.CreateAnchor(_imageToVisualize.CenterPose);
		////Create new visualiser and set as anchor's child ( so to keep the visualiser in that place )
		//ARImageVisualiser visualizer = Instantiate(m_arVisualizerPrefab, anchor.transform) as ARImageVisualiser;
		//visualizer.gameObject.SetActive(true);
		////Set image of visualiser to be a copy of the image that is tracked
		//visualizer.m_image = _imageToVisualize;

		////Add visualizer to list
		//m_listOfVisualizedObjects.Add(visualizer);
	}

	/// <summary>
	/// Creates multiple ARImageVisualizers and adds it to a reference list.
	/// </summary>
	/// <param name="_imagesToVisualize">List of AugmentedImages to visualize</param>
	void CreateVisualizerObjects(List<AugmentedImage> _imagesToVisualize)
	{
		foreach (AugmentedImage image in _imagesToVisualize)
		{
			//Create an anchor at centre of image to ensure that transformation is relative to real world
			Anchor anchor = image.CreateAnchor(image.CenterPose);
			//Only continue if anchor is legit
			if(anchor != null)
			{
				//Get instance of visualizer from object pool
				ARImageVisualiser visualizer = ObjectPool.GetExisting<ARImageVisualiser>();
				//Set parent of visualizer to anchor
				visualizer.transform.SetParent(anchor.transform);
				//Set image of visualizer
				visualizer.m_image = image;
				//Set visualizer to active
				visualizer.gameObject.SetActive(true);

				//Add visualizer to list
				m_listOfVisualizedObjects.Add(visualizer);

				//Set anchor to self-destruct
				Destroy(anchor, 0.2f);
			}
		}
	}

	/// <summary>
	/// Destroys all the ARImageVisualisers in the scene.
	/// </summary>
    void RemoveVisualizedObjects()
    {
        //Destroy all the visualizer objects in the scene
        foreach(ARImageVisualiser visualizer in m_listOfVisualizedObjects)
        {
			//Disable visualizer
			visualizer.gameObject.SetActive(false);
			//Add object back to pool
			ObjectPool.Add(visualizer);
		}
		//Clear the list
		m_listOfVisualizedObjects.Clear();
    }

    /// <summary>
    /// Setting the sleep timing of the phone
    /// </summary>
    private void TimeTillSleep(int _time = 1)
    {
        Screen.sleepTimeout = _time;

        // Only allow the screen to sleep when not tracking.
        if (m_trackedImages.Count > 0)
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
