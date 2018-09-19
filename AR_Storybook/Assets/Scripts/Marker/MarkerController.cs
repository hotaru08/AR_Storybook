using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GoogleARCore;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controller for SceneMarker
/// </summary>
public class MarkerController : MonoBehaviour
{
    /// <summary>
    /// A prefab for visualizing an AugmentedImage.
    /// </summary>
    public ARImageVisualiser AugmentedImageVisualizerPrefab;

    /// <summary>
    /// The overlay containing the fit to scan user guide.
    /// </summary>
    public GameObject FitToScanOverlay;

    /// <summary>
    /// Dictionary storing the Visualisers in the scene
    /// </summary>
    private Dictionary<int, ARImageVisualiser> m_Visualizers = new Dictionary<int, ARImageVisualiser>();


    /// <summary>
    /// A List of Images we are tracking ( multi-tracking available too )
    /// </summary>
    private List<AugmentedImage> m_TempAugmentedImagesList = new List<AugmentedImage>();

    /// <summary>
    /// Debug
    /// </summary>
    public Text m_debuggingText;
    public Text m_trackingStateDebug;
    public Text m_trackingStateDebug2;
    public Text m_trackingStateDebug3;

    private int m_prevIndex;

    /// <summary>
    /// The Unity Update method
    /// </summary>
    public void Update()
    {
        // If session is tracked, then run code
        //if (Session.Status != SessionStatus.Tracking)
        //    return;

        // If there is any images, it will be added to the list
        //m_TempAugmentedImagesList.Clear();
        m_debuggingText.text = "List of Images before track: " + m_TempAugmentedImagesList.Count + " Prev index " + m_prevIndex + "\n";
        Session.GetTrackables<AugmentedImage>(m_TempAugmentedImagesList, TrackableQueryFilter.All);
        m_debuggingText.text += "List of Images after track: " + m_TempAugmentedImagesList.Count;


        // Set visualisers on top of the images in the list
        SetVisualisersOnImages();

        // Show the overlay for tracking 
        ShowOverlay();
    }

    /// <summary>
    /// When Images are tracked and added into the list, a visualiser will then be set on top of them
    /// </summary>
    public void SetVisualisersOnImages()
    {
        foreach (var _image in m_TempAugmentedImagesList)
        {
            // Get visualiser from image in list and store it
            ARImageVisualiser m_visualiser = null;
            m_visualiser = GetVisualiser(_image, m_visualiser);

            // if there is no visualiser for that image and it is tracked, add one
            if (m_visualiser == null && _image.TrackingState == TrackingState.Tracking)
            {
                AddVisualiser(_image);
            }
            else if (_image.TrackingState != TrackingState.Tracking && m_visualiser != null)
            {
                //m_trackingStateDebug.text = "Count of Dictionary: " + m_Visualizers.Count;
                RemoveVisualiser(_image, m_visualiser);
                //m_trackingStateDebug2.text = "Count of Dictionary: " + m_Visualizers.Count;
            }
            //m_debuggingText.text = "prev index: " + m_prevIndex;

            if (_image.DatabaseIndex == m_prevIndex)
                continue;

            // remove the visualiser 
            //RemoveVisualiser(_image, m_visualiser);
            // remove images from list -> trackable images are still there
            m_trackingStateDebug.text = "Before Count of imagelist: " + m_TempAugmentedImagesList.Count;
            m_TempAugmentedImagesList.RemoveAt(_image.DatabaseIndex);
            m_trackingStateDebug2.text = "After Count of imagelist: " + m_TempAugmentedImagesList.Count;
        }
    }

    /// <summary>
    /// Add Visualiser and anchors for updated augmented images that are tracking and do not previously have a visualizer
    /// </summary>
    public void AddVisualiser(AugmentedImage _imageToAddTo)
    {
        // Create an anchor at centre of image to ensure that transformation is relative to real world
        Anchor anchor = _imageToAddTo.CreateAnchor(_imageToAddTo.CenterPose);
        // Create new visualiser and set as anchor's child ( so to keep the visualiser in that place )
        ARImageVisualiser visualizer = Instantiate(AugmentedImageVisualizerPrefab, anchor.transform) as ARImageVisualiser;
        // Set image of visualiser to be image that is tracked
        visualizer.m_image = _imageToAddTo;
        //m_prevIndex = _imageToAddTo.DatabaseIndex;
        // Add to Dictionary
        m_Visualizers.Add(_imageToAddTo.DatabaseIndex, visualizer);
    }

    /// <summary>
    /// Remove a Visualiser from an image if it stops updating
    /// </summary>
    public void RemoveVisualiser(AugmentedImage _imageToRemoveFrom, ARImageVisualiser _visualiserToRemove)
    {
        // Remove Visualiser from image and Destroys it
        Destroy(m_Visualizers[_imageToRemoveFrom.DatabaseIndex].gameObject);
        m_Visualizers.Remove(_imageToRemoveFrom.DatabaseIndex);
    }

    /// <summary>
    /// Get the visualiser of the Image in list
    /// </summary>
    public ARImageVisualiser GetVisualiser(AugmentedImage _imageTracked, ARImageVisualiser _addVisualiser)
    {
        m_Visualizers.TryGetValue(_imageTracked.DatabaseIndex, out _addVisualiser);
        return _addVisualiser;
    }

    /// <summary>
    /// Show the "FitToScanOverlay" accordingly
    /// </summary>
    public void ShowOverlay()
    {
        foreach (var visualizer in m_Visualizers.Values)
        {
            if (visualizer.m_image.TrackingState == TrackingState.Tracking)
            {
                FitToScanOverlay.SetActive(false);
                return;
            }
        }
        FitToScanOverlay.SetActive(true);
    }
}