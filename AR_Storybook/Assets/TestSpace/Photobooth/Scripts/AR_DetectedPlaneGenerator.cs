namespace TestSpace
{
	using UnityEngine;
	using ATXK.Helper;
	using System.Collections.Generic;
	using GoogleARCore;
	using GoogleARCore.Examples.Common;

	public class AR_DetectedPlaneGenerator : SingletonBehaviour<AR_DetectedPlaneGenerator>
	{
		[SerializeField] GameObject detectedLanePrefab;

		List<GameObject> m_detectedPlanes = new List<GameObject>();
		List<DetectedPlane> m_newPlanes = new List<DetectedPlane>();

		private void Update()
		{
			if (Session.Status != SessionStatus.Tracking)
				return;

			Session.GetTrackables(m_newPlanes, TrackableQueryFilter.New);
			foreach(DetectedPlane plane in m_newPlanes)
			{
				GameObject planeObject = Instantiate(detectedLanePrefab, Vector3.zero, Quaternion.identity, transform);
				m_detectedPlanes.Add(planeObject);

				planeObject.GetComponent<DetectedPlaneVisualizer>().Initialize(plane);
			}
		}
	}
}