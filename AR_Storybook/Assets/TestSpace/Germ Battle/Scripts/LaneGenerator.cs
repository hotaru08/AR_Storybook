namespace TestSpace
{
	using System.Collections.Generic;
	using UnityEngine;

	public class LaneGenerator : MonoBehaviour
	{
		enum LaneLayout
		{
			HORIZONTAL,
			CIRCULAR
		}

		[SerializeField] Lane lanePrefab;
		[Range(1, 5)][SerializeField] int numLanes;

		[SerializeField] float areaWidth;
		[SerializeField] float areaLength;

		[SerializeField] LaneLayout laneLayout;

		List<Lane> spawnedLanes = new List<Lane>();
		Renderer renderer;

		float offsetX;
		float scaleX;

		Vector3 startPos;

		private void Start()
		{
			renderer = GetComponent<Renderer>();

			if (numLanes < 1)
				numLanes = 1;
			if (numLanes > 5)
				numLanes = 5;

			areaWidth = transform.localScale.x;
			areaLength = transform.localScale.z;

			offsetX = ((100f / (numLanes)) * (renderer.bounds.size.x / 100f));
			scaleX = 1f / numLanes;

			SpawnLanes();
		}

		/// <summary>
		/// Spawns lanes according to the selected style.
		/// </summary>
		private void SpawnLanes()
		{
			switch(laneLayout)
			{
				case LaneLayout.HORIZONTAL:
					HorizontalLanes();
					break;

				case LaneLayout.CIRCULAR:
					CircularLanes();
					break;
			}
		}

		/// <summary>
		/// Spawns Lanes side-by-side horizontally.
		/// </summary>
		private void HorizontalLanes()
		{
			startPos = new Vector3(renderer.bounds.min.x, 0.1f, 0);

			// Temporary Lane to calculate the offset needed for the starting point
			Lane temp = Instantiate(lanePrefab, transform);
			temp.transform.localScale = new Vector3(scaleX, temp.transform.localScale.y, 1);
			// Adjust starting point to allow Lane to be generated with the correct positions
			startPos.x -= temp.GetComponent<Renderer>().bounds.extents.x;
			DestroyImmediate(temp.gameObject);

			for (int i = 0; i < numLanes; i++)
			{
				Lane lane = Instantiate(lanePrefab, transform);

				lane.transform.localScale = new Vector3(scaleX, lane.transform.localScale.y, 1);

				startPos.x += offsetX;
				lane.transform.position = startPos;

				spawnedLanes.Add(lane);
			}
		}

		/// <summary>
		/// Spawns Lanes around a central point.
		/// </summary>
		private void CircularLanes()
		{
			startPos = transform.position;

			// Angle in degrees between each lane
			float angleBetweenLanes = 360f / numLanes;
			// Radius of which lanes will spawn outside
			float radius = Mathf.Max(renderer.bounds.extents.x, renderer.bounds.extents.z);

			for(int i = 0; i < numLanes; i++)
			{
				Lane lane = Instantiate(lanePrefab);

				float offsetPosition = lane.GetComponent<Renderer>().bounds.extents.z;

				lane.transform.position = transform.position;
				lane.transform.rotation = transform.rotation;
				lane.transform.position += lane.transform.forward * (radius + offsetPosition);

				transform.Rotate(Vector3.up, angleBetweenLanes);
			}
		}
	}
}