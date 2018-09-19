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
    public ARImageVisualiser m_arVisualizerPrefab;

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

    /// <summary>
    /// Debug texts.
    /// </summary>
    public Text m_debuggingText;
    public Text m_trackingStateDebug;
    public Text m_trackingStateDebug2;

    private void Start()
    {
		m_arSession = ARSessionManager.Instance.GetSession();

        m_trackedImages = new List<AugmentedImage>();
		m_listOfVisualizedObjects = new List<ARImageVisualiser>();
	}

	private void Update()
	{
        TimeTillSleep();

		//Only get new tracked images
		Session.GetTrackables(m_trackedImages, TrackableQueryFilter.All);
		//If list is populated, that means in this frame there are images being tracked
		//Therefore we should remove all old visualized objects in the scene and render the last one in the list (ie the newest one)
		if (m_trackedImages.Count > 0)
		{
			//Currently tracking more than one image, so we should remove all old images and keep the latest one only
			if (m_trackedImages.Count > 1 && !m_visualizeAllImages)
			{
				ARSessionManager.Instance.ResetSession();
			}

			//Sort the images from oldest to newest (smallest time to largest time)
			m_trackedImages.Sort((x, y) => x.GetTimeCreated().CompareTo(y.GetTimeCreated()));

			//Remove objects in the scene
			RemoveVisualizedObjects();
			//Create a visualizer and add it to the scene
			CreateVisualizerObjects(m_trackedImages);
		}

		//DEBUGGING
		m_debuggingText.text = "New Images Count: " + m_trackedImages.Count + "\n";
		m_debuggingText.text += "Visualizers in the scene: " + m_listOfVisualizedObjects.Count + "\n";
	}

	/// <summary>
	/// Creates a single ARImageVisualiser and adds it to a reference list.
	/// </summary>
	void CreateVisualizerObjects(AugmentedImage _imageToVisualize)
    {
        //Create an anchor at centre of image to ensure that transformation is relative to real world
        Anchor anchor = _imageToVisualize.CreateAnchor(_imageToVisualize.CenterPose);
        //Create new visualiser and set as anchor's child ( so to keep the visualiser in that place )
        ARImageVisualiser visualizer = Instantiate(m_arVisualizerPrefab, anchor.transform) as ARImageVisualiser;
		visualizer.gameObject.SetActive(true);
        //Set image of visualiser to be a copy of the image that is tracked
        visualizer.m_image = _imageToVisualize;

		//Add visualizer to list
		m_listOfVisualizedObjects.Add(visualizer);
    }

	/// <summary>
	/// Creates multiple ARImageVisualizers and adds it to a reference list.
	/// </summary>
	/// <param name="_imagesToVisualize">List of AugmentedImages to visualize</param>
	void CreateVisualizerObjects(List<AugmentedImage> _imagesToVisualize)
	{
		foreach (AugmentedImage image in _imagesToVisualize)
		{
			CreateVisualizerObjects(image);
		}
	}

	/// <summary>
	/// Destroys all the ARImageVisualisers in the scene.
	/// </summary>
    void RemoveVisualizedObjects()
    {
        //Destroy all the visualizer objects in the scene
        foreach(ARImageVisualiser arImageVisualiser in m_listOfVisualizedObjects)
        {
            Destroy(arImageVisualiser);
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
