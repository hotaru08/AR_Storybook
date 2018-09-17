using System;
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
    public ARImageVisualiser ARVisualizerPrefab;

    /// <summary>
    /// List of all tracked images.
    /// </summary>
    public List<AugmentedImage> m_newTrackedImages;

    /// <summary>
    /// Reference to visualized object.
    /// </summary>
    public List<ARImageVisualiser> m_listOfVisualizedObjects;

    /// <summary>
    /// Debug texts.
    /// </summary>
    public Text m_debuggingText;
    public Text m_trackingStateDebug;
    public Text m_trackingStateDebug2;

    private void Start()
    {
        m_newTrackedImages = new List<AugmentedImage>();
        m_listOfVisualizedObjects = new List<ARImageVisualiser>();
    }

    private void Update()
    {
        //Only get new tracked images
        Session.GetTrackables(m_newTrackedImages, TrackableQueryFilter.All);
        //If list is populated, that means in this frame there are images being tracked
        //Therefore we should remove all old visualized objects in the scene and render the last one in the list (ie the newest one)
        if(m_newTrackedImages.Count > 0)
        {
            //Sort the images from oldest to newest (smallest time to largest time)
            m_newTrackedImages.Sort((x, y) => x.GetTimeCreated().CompareTo(y.GetTimeCreated()));
            //Remove objects in the scene
            RemoveVisualizedObjects();
            //Add objects to the scene
            AddVisualizerObjects();
        }

        //DEBUGGING
        m_debuggingText.text = "New Images Count: " + m_newTrackedImages.Count + "\n";
        m_debuggingText.text += "Visualizers in the scene: " + m_listOfVisualizedObjects.Count;
    }

    void AddVisualizerObjects()
    {
        ////This spawns objects for all tracked images
        ////Create new visualizer objects and add them to the list
        //foreach (AugmentedImage image in m_newTrackedImages)
        //{
        //    //Create an anchor at centre of image to ensure that transformation is relative to real world
        //    Anchor anchor = image.CreateAnchor(image.CenterPose);
        //    //Create new visualiser and set as anchor's child ( so to keep the visualiser in that place )
        //    ARImageVisualiser visualizer = Instantiate(ARVisualizerPrefab, anchor.transform) as ARImageVisualiser;
        //    //Set image of visualiser to be image that is tracked
        //    visualizer.m_image = image;

        //    //Add visualizer to list
        //    m_listOfVisualizedObjects.Add(visualizer);
        //}

        //This spawns one object for the latest tracked image 
        AugmentedImage image = m_newTrackedImages[m_newTrackedImages.Count - 1];
        //Create an anchor at centre of image to ensure that transformation is relative to real world
        Anchor anchor = image.CreateAnchor(image.CenterPose);
        //Create new visualiser and set as anchor's child ( so to keep the visualiser in that place )
        ARImageVisualiser visualizer = Instantiate(ARVisualizerPrefab, anchor.transform) as ARImageVisualiser;
        //Set image of visualiser to be image that is tracked
        visualizer.m_image = image;

        //Add visualizer to list
        m_listOfVisualizedObjects.Add(visualizer);
    }

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
}
