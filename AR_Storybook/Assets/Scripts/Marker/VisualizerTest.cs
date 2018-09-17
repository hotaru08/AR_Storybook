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
    public ARImageVisualiser AugmentedImageVisualizerPrefab;

    /// <summary>
    /// The overlay containing the fit to scan user guide.
    /// </summary>
    public GameObject FitToScanOverlay;

    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    //TESTING CODE START
    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// List of all tracked images.
    /// </summary>
    public List<AugmentedImage> m_trackedImagesList = new List<AugmentedImage>();

    /// <summary>
    /// Object to visualize.
    /// Using only 1 Object and keeping reference to it, so that if new image is tracked (ie new page), we just change the object being shown.
    /// </summary>
    public ARImageVisualiser m_imageVisualizer;

    /// <summary>
    /// Debug texts.
    /// </summary>
    public Text m_debuggingText;
    public Text m_trackingStateDebug;
    public Text m_trackingStateDebug2;

    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    //TESTING CODE END
    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void Update()
    {
        //Make sure AR session is tracking, if not do not run any update code
        if (Session.Status != SessionStatus.Tracking)
            return;

        //Get all tracked images
        Session.GetTrackables(m_trackedImagesList);
        //Sort all tracked images by the time they were first tracked
        m_trackedImagesList.Sort((x, y) => x.GetTimeCreated().CompareTo(y.GetTimeCreated()));

        //DEBUG
        m_debuggingText.text = "Tracked Count: " + m_trackedImagesList.Count + " ";
        foreach(AugmentedImage image in m_trackedImagesList)
        {
            m_debuggingText.text += image.GetTimeCreated() + ", ";
        }

        //Set the visualizer image to the latest image
        AddVisualiser(m_trackedImagesList[m_trackedImagesList.Count - 1]);

        //Show overlay if not tracking object
        FitToScanOverlay.SetActive(true);
        if (m_imageVisualizer != null && m_imageVisualizer.m_image.TrackingState.Equals(TrackingState.Tracking))
            FitToScanOverlay.SetActive(false);
    }

    /// <summary>
    /// Add Visualiser and anchors for updated augmented images that are tracking and do not previously have a visualizer
    /// </summary>
    public void AddVisualiser(AugmentedImage _imageToVisualize)
    {
        //Destroy all old visualizers
        Destroy(m_imageVisualizer.gameObject);

        //Create an anchor at centre of image to ensure that transformation is relative to real world
        Anchor anchor = _imageToVisualize.CreateAnchor(_imageToVisualize.CenterPose);
        //Create new visualiser and set as anchor's child ( so to keep the visualiser in that place )
        m_imageVisualizer = Instantiate(AugmentedImageVisualizerPrefab, anchor.transform) as ARImageVisualiser;
        //Set image of visualiser to be image that is tracked
        m_imageVisualizer.m_image = _imageToVisualize;
    }
}
