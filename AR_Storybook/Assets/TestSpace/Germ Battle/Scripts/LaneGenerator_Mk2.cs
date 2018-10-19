namespace TestSpace
{
	using System.Collections.Generic;
	using UnityEngine;
	using ATXK.EventSystem;
	using ATXK.AI;

	[RequireComponent(typeof(ES_EventListener))]
	public class LaneGenerator_Mk2 : MonoBehaviour
	{
		enum LaneLayout
		{
			HORIZONTAL,
			CIRCULAR
		}

		[Header("Lane Settings")]
		[Tooltip("Lane prefab that contains the Lane script.")]
		[SerializeField] Lane lanePrefab;
		[Tooltip("Number of Lanes that will be generated, limited to a maximum of 5 lanes.")]
		[Range(1, 5)][SerializeField] int numLanes = 1;
		[Tooltip("Layout style for generating lanes.")]
		[SerializeField] LaneLayout laneLayout;

		List<Lane> spawnedLanes = new List<Lane>();

		[Header("Global Scale Setting")]
		[Tooltip("Scaling for enemy and player prefab.")]
		[SerializeField] float prefabScale = 10f;

		[Header("Player Settings")]
		[Tooltip("Player prefab that contains the PlayerManager script.")]
		[SerializeField] PlayerManager playerPrefab;
		[Tooltip("0-based index of the lane that the player will start from.")]
		[Range(0, 4)][SerializeField] int playerStartLane = 0;
		PlayerManager player;

		[Header("Enemy Settings")]
		[SerializeField] AI_Controller enemyPrefab;

		Renderer renderer;

		float areaWidth;
		float areaLength;

		float offsetX;
		float scaleX;

		Vector3 startPos;

		private void Start()
		{
			renderer = GetComponent<Renderer>();
			GetComponent<MeshRenderer>().enabled = false;

			if (numLanes < 1)
				numLanes = 1;
			if (numLanes > 5)
				numLanes = 5;

			areaWidth = transform.localScale.x;
			areaLength = transform.localScale.z;

			offsetX = ((100f / (numLanes)) * (renderer.bounds.size.x / 100f));
			scaleX = 1f / numLanes;

			SpawnLanes();
			SpawnPlayer();
		}

		/// <summary>
		/// Spawns lanes according to the selected style.
		/// </summary>
		private void SpawnLanes()
		{
			spawnedLanes.Clear();

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
		/// Spawns the Player at the given starting lane.
		/// </summary>
		private void SpawnPlayer()
		{
			player = Instantiate(playerPrefab);
			player.transform.position = new Vector3(spawnedLanes[playerStartLane].playerPosition.position.x, spawnedLanes[playerStartLane].playerPosition.position.y + 0.1f, spawnedLanes[playerStartLane].playerPosition.position.z);
			player.transform.LookAt(spawnedLanes[playerStartLane].enemyPosition);
			player.transform.localScale = new Vector3(prefabScale, prefabScale, prefabScale);
		}

		/// <summary>
		/// Spawns Lanes side-by-side horizontally.
		/// </summary>
		private void HorizontalLanes()
		{
			startPos = new Vector3(renderer.bounds.min.x, 0.01f, 0f);

			// Temporary Lane to calculate the offset needed for the starting point
			Lane temp = Instantiate(lanePrefab, transform);
			temp.transform.localScale = new Vector3(scaleX, 1, 1);
			// Adjust starting point to allow Lane to be generated with the correct positions
			startPos.x -= temp.GetComponent<Renderer>().bounds.extents.x;
			DestroyImmediate(temp.gameObject);

			for (int i = 0; i < numLanes; i++)
			{
				Lane lane = Instantiate(lanePrefab, transform);

				lane.transform.localScale = new Vector3(scaleX, 1, 1);
				lane.enemyPrefab = enemyPrefab;
				lane.enemyScale = prefabScale;
				lane.laneID = i;

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

				lane.transform.rotation = transform.rotation;
				lane.transform.position = transform.position;
				lane.transform.position += lane.transform.forward * (radius + offsetPosition);
				lane.transform.forward = -lane.transform.forward;
				lane.enemyPrefab = enemyPrefab;
				lane.enemyScale = prefabScale;
				lane.laneID = i;

				transform.Rotate(Vector3.up, angleBetweenLanes);
				spawnedLanes.Add(lane);
			}
		}
	}
}