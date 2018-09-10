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
    /// List storing Images that are tracked at least once before
    /// </summary>
    private List<AugmentedImage> m_TempAugmentedImages = new List<AugmentedImage>();

    /// <summary>
    /// Debug
    /// </summary>
    public Text m_debuggingText;

    /// <summary>
    /// Unity Start Method
    /// </summary>
    public void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    /// <summary>
    /// The Unity Update method
    /// </summary>
    public void Update()
    {
        // Exit the app when the 'back' button is pressed.
        //if (Input.GetKey(KeyCode.Escape))
        //{
        //    Application.Quit();
        //}

        // Check that motion tracking is tracking.
        if (Session.Status != SessionStatus.Tracking)
        {
            m_debuggingText.text = "Not Tracking";
            return;
        }

        // Get updated augmented images for this frame.
        Session.GetTrackables<AugmentedImage>(m_TempAugmentedImages, TrackableQueryFilter.Updated);

        // Create visualizers and anchors for updated augmented images that are tracking and do not previously
        // have a visualizer. Remove visualizers for stopped images.
        foreach (var image in m_TempAugmentedImages)
        {
            ARImageVisualiser visualizer = null;
            m_Visualizers.TryGetValue(image.DatabaseIndex, out visualizer);
            if (image.TrackingState == TrackingState.Tracking && visualizer == null)
            {
                // Create an anchor to ensure that ARCore keeps tracking this augmented image.
                Anchor anchor = image.CreateAnchor(image.CenterPose);
                visualizer = Instantiate(AugmentedImageVisualizerPrefab, anchor.transform) as ARImageVisualiser;
                visualizer.m_image = image;
                m_Visualizers.Add(image.DatabaseIndex, visualizer);
            }
            else if (image.TrackingState == TrackingState.Stopped && visualizer != null)
            {
                m_Visualizers.Remove(image.DatabaseIndex);
                GameObject.Destroy(visualizer.gameObject);
            }
            m_debuggingText.text = "Tracking ...\nImage: " + image.Name.ToString() + "\nVisualiser: " + visualizer.name.ToString();
        }
        

        // Show the fit-to-scan overlay if there are no images that are Tracking.
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