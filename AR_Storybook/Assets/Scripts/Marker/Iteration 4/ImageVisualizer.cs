﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class ImageVisualizer : MonoBehaviour
{
	/// <summary>
	/// Scale multiplier for the visualizer.
	/// </summary>
	//[SerializeField] float scaleMultiplier = 0.2f;

	/// <summary>
	/// List of GameObjects that will be spawned to represent different images.
	/// </summary>
	[SerializeField] List<GameObject> visualizerPrefabs;
	/// <summary>
	/// The tracked image that this ImageVisualizer is representing.
	/// </summary>
	AugmentedImage m_Image;
	/// <summary>
	/// Anchor that the ImageVisualizer will reference for positioning.
	/// </summary>
	Anchor m_anchor;
	/// <summary>
	/// The GameObject to represent the tracked image.
	/// </summary>
	GameObject m_GeneratedObject;

	/// <summary>
	/// The tracked image that this ImageVisualizer is representing.
	/// </summary>
	public AugmentedImage Image { get { return m_Image; } set { m_Image = value; } }
	/// <summary>
	/// Anchor that the ImageVisualizer will reference for positioning.
	/// </summary>
	public Anchor Anchor { set { DestroyImmediate(m_anchor); m_anchor = value; } }
    
    private void VisualizeImage()
	{
		//Disable the placeholder render
		GetComponent<Renderer>().enabled = false;

		//Create a gameobject visualizer
		m_GeneratedObject = Instantiate(visualizerPrefabs[m_Image.DatabaseIndex], transform);
        m_GeneratedObject.transform.position = m_anchor.transform.position;
        m_GeneratedObject.transform.rotation = transform.rotation;

        Debug.LogWarning("Pos: " + m_GeneratedObject.transform.position);
        Debug.LogWarning("Rotation: " + m_GeneratedObject.transform.rotation);
    }

	private void OnEnable()
	{
        if (m_Image != null)
		{
			VisualizeImage();
		}
	}

	private void OnDisable()
	{
		//Destroy the generated GameObject
		Destroy(m_GeneratedObject);
	}

	private void OnDestroy()
	{
		//Destroy the generated GameObject
		Destroy(m_GeneratedObject);
		//Destroy the GameObject holding this component
		Destroy(gameObject);
	}
}
