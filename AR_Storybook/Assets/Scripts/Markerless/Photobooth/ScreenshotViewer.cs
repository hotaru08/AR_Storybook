using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ScreenshotViewer : MonoBehaviour
{
	//public Texture2D CurrScreenshot { get { return screenshots[currIndex]; } }

	//string[] fileURLs;
	//List<Texture2D> screenshots = new List<Texture2D>();
	//int currIndex = 0;
	//Image imageToShowScreenshot;

	//private void Start()
	//{
	//	imageToShowScreenshot = GetComponent<Image>();

	//	ScanForScreenshots();
	//}

	//private void GetScreenshots()
	//{
	//	// Load all screenshots from files into Texture
	//	foreach (string fileURL in fileURLs)
	//	{
	//		screenshots.Add(GetTexture(fileURL));
	//	}
	//}

	//private Texture2D GetTexture(string fileURL)
	//{
	//	Texture2D texture = null;
	//	byte[] fileBytes;
	//	if(File.Exists(fileURL))
	//	{
	//		fileBytes = File.ReadAllBytes(fileURL);
	//		texture = new Texture2D(1, 1, TextureFormat.RGB24, false);
	//		texture.LoadImage(fileBytes);
	//	}
	//	return texture;
	//}

	//public void ScanForScreenshots()
	//{
	//	fileURLs = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
	//	if (fileURLs.Length > 0)
	//	{
	//		GetScreenshots();
	//	}
	//}

	//public void NextImage()
	//{
	//	currIndex++;
	//	if (currIndex >= screenshots.Count)
	//		currIndex = 0;

	//	imageToShowScreenshot.sprite = Sprite.Create(CurrScreenshot, new Rect(0, 0, imageToShowScreenshot.sprite.texture.width, imageToShowScreenshot.sprite.texture.height), new Vector2(0.5f, 0.5f));
	//}

	//public void PrevImage()
	//{
	//	currIndex--;
	//	if (currIndex < 0)
	//		currIndex = screenshots.Count - 1;

	//	imageToShowScreenshot.sprite = Sprite.Create(CurrScreenshot, new Rect(0, 0, imageToShowScreenshot.sprite.texture.width, imageToShowScreenshot.sprite.texture.height), new Vector2(0.5f, 0.5f));
	//}

	//public void DeleteCurrentImage()
	//{

	//}

	private string[] fileURLs;
	private int currentIndex;
	private Image image;
	private List<Screenshot> screenshots = new List<Screenshot>();

	private void Awake()
	{
		image = GetComponent<Image>();
	}

	private void Start()
	{
		ScanForScreenshots();
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

		if (screenshots.Count > 0)
			image.overrideSprite = screenshots[currentIndex].fileSprite;
		else
			image.overrideSprite = null;
	}

	public void PrevImage()
	{
		currentIndex--;
		if (currentIndex < 0)
			currentIndex = screenshots.Count - 1;

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
