using UnityEngine;
using ATXK.Helper;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using System.Collections.Generic;

public class DetectedPlaneGenerator_Mk2 : SingletonBehaviour<DetectedPlaneGenerator_Mk2>
{
	[Header("Surface Visualizer Prefab")]
	[SerializeField] DetectedPlaneVisualizer detectedPlanePrefab;

	[Header("AR Surfaces")]
	[SerializeField] List<GameObject> detectedPlanes = new List<GameObject>();
	[SerializeField] List<DetectedPlane> newDetectedPlanes = new List<DetectedPlane>();

	public List<GameObject> DetectedPlanes { get { return detectedPlanes; } }

	private void Update()
	{
		// Only update if session is tracking
		if (Session.Status != SessionStatus.Tracking)
			return;

		Session.GetTrackables(newDetectedPlanes, TrackableQueryFilter.New);
		foreach(DetectedPlane newPlane in newDetectedPlanes)
		{
			DetectedPlaneVisualizer planeVisualizer = Instantiate(detectedPlanePrefab, Vector3.zero, Quaternion.identity, transform);
			planeVisualizer.Initialize(newPlane);
			detectedPlanes.Add(planeVisualizer.gameObject);
		}
	}
}