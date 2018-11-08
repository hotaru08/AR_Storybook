using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotViewer_Mk2 : MonoBehaviour
{
	public Texture2D CurrentScreenshotTexture { get { return screenshots[currentIndex].fileTexture; } }
	public Sprite CurrentScreenshotSprite { get { return screenshots[currentIndex].fileSprite; } }
	public string CurrentScreenshotURL { get { return screenshots[currentIndex].fileURL; } }

	private string[] fileURLs;
	private int currentIndex;
	private Image image;
	private List<Screenshot> screenshots = new List<Screenshot>();

	private void Start()
	{
		image = GetComponent<Image>();

		ScanForScreenshots();
	}

	private void GetScreenshots()
	{
		foreach(string url in fileURLs)
		{
			Screenshot screenshot = new Screenshot();
			screenshot.fileURL = url;
			screenshot.fileTexture = GetTexture(url);
			screenshot.fileSprite = Sprite.Create(screenshot.fileTexture, new Rect(0, 0, image.sprite.texture.width, image.sprite.texture.height), new Vector2(0.5f, 0.5f));
			screenshots.Add(screenshot);
		}
	}

	private Texture2D GetTexture(string url)
	{
		Texture2D texture = null;
		byte[] fileBytes;

		if(File.Exists(url))
		{
			fileBytes = File.ReadAllBytes(url);
			texture = new Texture2D(1, 1, TextureFormat.RGB24, false);
			texture.LoadImage(fileBytes);
		}
		return texture;
	}

	public void ScanForScreenshots()
	{
		fileURLs = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
		if (fileURLs.Length > 0)
			GetScreenshots();
	}

	public void NextImage()
	{
		currentIndex++;
		if (currentIndex >= screenshots.Count)
			currentIndex = 0;

		image.sprite = CurrentScreenshotSprite;
	}

	public void PrevImage()
	{
		currentIndex--;
		if (currentIndex < 0)
			currentIndex = screenshots.Count - 1;

		image.sprite = CurrentScreenshotSprite;
	}

	public void DeleteCurrentImage()
	{

	}
}
