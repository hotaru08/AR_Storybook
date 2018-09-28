﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using UnityEngine.UI;

public enum VisualizerMode
{
	VM_ALL,
	VM_SINGLE
}

public class VisualizerManager : MonoBehaviour
{
	[Header("Visualizer Setup")]
	[SerializeField] ImageVisualizer m_ImageVisualizerPrefab;
	[SerializeField] ARCoreSession m_Session;
	[SerializeField] int m_PoolStartingSize = 10;

	[Header("Visualizer Settings")]
	[SerializeField] VisualizerMode m_VisualizerMode = VisualizerMode.VM_ALL;
	[SerializeField] int m_UpdateFrequency = 4;

	float bounceTime = 0f;
	float timeBetweenUpdates = 0f;

	List<AugmentedImage> m_OldImages;
	List<AugmentedImage> m_CurrFrameImages;
	List<ImageVisualizer> m_Visualizers;

	[SerializeField] Text debugText;
	

	/// <summary>
	/// Unity Start function.
	/// </summary>
	private void Start()
	{
		//Get the current ARCore session
		m_Session = ARSessionManager.Instance.GetSession();

		//Initialize lists
		m_OldImages = new List<AugmentedImage>();
		m_CurrFrameImages = new List<AugmentedImage>();
		m_Visualizers = new List<ImageVisualizer>();

		//Set the device screen to never timeout
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		//Initialize object pool of ImageVisualizers
		for(int i = 0; i < m_PoolStartingSize; i++)
		{
			//Create new instance of ImageVisualizer
			ImageVisualizer visualizer = Instantiate(m_ImageVisualizerPrefab);
			//Deactivate visualizer's gameobjec
			visualizer.gameObject.SetActive(false);

			//Add instance to object pool
			ObjectPool.Add(visualizer);
		}
	}

	/// <summary>
	/// Unity Update function.
	/// </summary>
	private void Update()
	{
		if(bounceTime <= Time.time)
		{
			UpdateTrackables();

			bounceTime = Time.time + timeBetweenUpdates;
		}
	}

	/// <summary>
	/// Gets tracked AugmentImages from ARCore and either resets the session or creates visualizers.
	/// </summary>
	private void UpdateTrackables()
	{
		//Get all tracked images
		Session.GetTrackables(m_CurrFrameImages);
		if(m_CurrFrameImages.Count > 0)
		{
			//Reset the session if Visualizer is set to only track one image
			if (m_CurrFrameImages.Count > 1 && m_VisualizerMode.Equals(VisualizerMode.VM_SINGLE))
			{
				//Clear the image lists
				m_CurrFrameImages.Clear();
				m_OldImages.Clear();
				//Reset the ARCore session
				ARSessionManager.Instance.ResetSession();

				return;
			}

			//Check for newly tracked images and create visualizers
			UpdateNewVisualizers();
		}

		//Update old visualizer objects
		UpdateOldVisualizers();
	}

	/// <summary>
	/// Checks for new AugmentedImages and creates visualizers for them.
	/// </summary>
	private void UpdateNewVisualizers()
	{
		//List of newly tracked images
		List<AugmentedImage> newImages = new List<AugmentedImage>();
		//Check against previous frame's images to get all the new images
		foreach (AugmentedImage image in m_CurrFrameImages)
		{
			if (!m_OldImages.Contains(image))
			{
				//Save reference to newly tracked image
				newImages.Add(image);
			}
		}

		debugText.text = "Time: " + Time.time + "\n";
		debugText.text += "# Currently Tracked: " + m_CurrFrameImages.Count + "\n";
		debugText.text += "# Old Tracked: " + m_OldImages.Count + "\n";
		debugText.text += "# Newly Tracked: " + newImages.Count + "\n";

		//Create visualizers for new AugmentedImages
		CreateVisualizerObjects(newImages);
	}

	/// <summary>
	/// Updates the anchor of old ImageVisualizers.
	/// </summary>
	private void UpdateOldVisualizers()
	{
		foreach(ImageVisualizer visualizer in m_Visualizers)
		{
			Anchor anchor = visualizer.Image.CreateAnchor(visualizer.Image.CenterPose);
			visualizer.Anchor = anchor;
		}
	}

	/// <summary>
	/// Creates new visualizers for the AugmentedImages in the list.
	/// </summary>
	/// <param name="_imagesToVisualize"></param>
	private void CreateVisualizerObjects(List<AugmentedImage> _imagesToVisualize)
	{
		foreach(AugmentedImage image in _imagesToVisualize)
		{
			//Create an anchor at the centre of the image
			Anchor anchor = image.CreateAnchor(image.CenterPose);
			if (anchor != null)
			{
				//Get a visualizer from the object pool
				ImageVisualizer visualizer = ObjectPool.GetExistingOrNew<ImageVisualizer>();

				//Set the image
				visualizer.Image = image;
				//Set the anchor
				visualizer.Anchor = anchor;

				//Enable the visualizer
				visualizer.gameObject.SetActive(true);
				//Add visualizer to list
				m_Visualizers.Add(visualizer);

				//Only add image to old image list if anchor has been made for it
				m_OldImages.Add(image);
			}
		}
	}
}