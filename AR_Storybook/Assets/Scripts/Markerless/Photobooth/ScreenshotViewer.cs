using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Loads and displays all screenshots.
/// </summary>
[RequireComponent(typeof(Image))]
public class ScreenshotViewer : MonoBehaviour
{
	[SerializeField] Sprite noImagesSprite;

	private string[] fileURLs;
	private int currentIndex;
	private Image image;
	private List<Screenshot> screenshots = new List<Screenshot>();

	public Screenshot CurrentScreenshot { get { return screenshots[currentIndex]; } }

	private void Awake()
	{
		image = GetComponent<Image>();
	}

	private void Start()
	{
		image.sprite = noImagesSprite;

		currentIndex = 0;
		ScanAndSetImage();
	}

	/// <summary>
	/// Scans for PNG images and converts them into texture2D and sprites.
	/// </summary>
	public void StartScan()
	{
		StartCoroutine(ScanForScreenshots());
	}

	/// <summary>
	/// Scans for PNG images and converts them into texture2D and sprites, then sets the view image to the first screenshot in the list.
	/// </summary>
	public void ScanAndSetImage()
	{
		StartCoroutine(SetImage());
	}

	/// <summary>
	/// Converts all image files into Screenshots, with a Texture2D and Sprite.
	/// </summary>
	private void GetScreenshots()
	{
		foreach (string url in fileURLs)
		{
			Screenshot screenshot = new Screenshot();
			screenshot.fileURL = url;
			screenshot.fileTexture = GetTexture(url);
			screenshot.fileSprite = Sprite.Create(screenshot.fileTexture, new Rect(0, 0, screenshot.fileTexture.width, screenshot.fileTexture.height), new Vector2(0.5f, 0.5f));
			screenshots.Add(screenshot);
		}
	}

	/// <summary>
	/// Creates a Texture2D from the file at the given URL.
	/// </summary>
	/// <param name="url">URL of the file.</param>
	private Texture2D GetTexture(string url)
	{
		Texture2D texture = null;
		byte[] fileBytes;

		if (File.Exists(url))
		{
			fileBytes = File.ReadAllBytes(url);
			texture = new Texture2D(1, 1, TextureFormat.RGB24, false);
			texture.LoadImage(fileBytes);
		}
		return texture;
	}

	/// <summary>
	/// Scans for PNG images and converts them into texture2D and sprites.
	/// </summary>
	private IEnumerator ScanForScreenshots()
	{
		try
		{
			// Get file URLs from storage.
			fileURLs = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
		}
		catch(Exception ex)
		{
			Debug.LogError("ScanForScreenshot() ERROR = " + ex.ToString());
			yield break;
		}

		// Wait while array of file urls are being populated.
		while (fileURLs.Length <= 0)
		{
			Debug.Log("ScanForScreenshots() Is Waiting...");
			yield return new WaitForEndOfFrame();
		}
		// Converts screenshots once array has been populated.
		if (fileURLs.Length > 0)
			GetScreenshots();
	}

	/// <summary>
	/// Changes the viewing image to the next one in the list, with wraparound to the first image.
	/// </summary>
	public void NextImage()
	{
		currentIndex++;
		if (currentIndex >= screenshots.Count)
			currentIndex = 0;

		image.overrideSprite = screenshots[currentIndex].fileSprite;
	}

	/// <summary>
	/// Changes the viewing image to the previous one in the list, with wraparound to the last image.
	/// </summary>
	public void PrevImage()
	{
		currentIndex--;
		if (currentIndex < 0)
			currentIndex = screenshots.Count - 1;

		image.overrideSprite = screenshots[currentIndex].fileSprite;
	}

	/// <summary>
	/// Scans for images and sets the viewing image to the one at the current index.
	/// </summary>
	private IEnumerator SetImage()
	{
		if (screenshots.Count <= 0)
		{
			StartCoroutine(ScanForScreenshots());

			while (screenshots.Count <= 0)
			{
				yield return new WaitForEndOfFrame();
			}
		}

		image.overrideSprite = screenshots[currentIndex].fileSprite;
	}

	/// <summary>
	/// Removes the Screenshot from the list and deletes the image from disk.
	/// </summary>
	public void DeleteCurrentImage()
	{
		if(screenshots.Count > 0)
		{
			File.Delete(screenshots[currentIndex].fileURL);
			screenshots.Remove(screenshots[currentIndex]);

			image.overrideSprite = null;
			//image.sprite = null;
			currentIndex = 0;

			PrevImage();
		}
	}
}
