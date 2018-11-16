using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ScreenshotViewer : MonoBehaviour
{
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
		currentIndex = 0;

		StartCoroutine(ScanForScreenshots());
		SetImage();
	}

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

	public IEnumerator ScanForScreenshots()
	{
		// Get file URLs from storage.
		fileURLs = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
		// Wait while array of file urls are being populated.
		while (fileURLs.Length == 0)
			yield return new WaitForEndOfFrame();
		// Converts screenshots once array has been populated.
		if (fileURLs.Length > 0)
			GetScreenshots();
	}

	public void NextImage()
	{
		currentIndex++;
		if (currentIndex >= screenshots.Count)
			currentIndex = 0;

		SetImage();
	}

	public void PrevImage()
	{
		currentIndex--;
		if (currentIndex < 0)
			currentIndex = screenshots.Count - 1;

		SetImage();
	}

	public void SetImage()
	{
		if (screenshots.Count > 0)
			image.overrideSprite = screenshots[currentIndex].fileSprite;
		else
			image.overrideSprite = null;
	}

	public void DeleteCurrentImage()
	{
		if(screenshots.Count > 0)
		{
			File.Delete(screenshots[currentIndex].fileURL);
			screenshots.Remove(screenshots[currentIndex]);
			PrevImage();
		}
	}
}
