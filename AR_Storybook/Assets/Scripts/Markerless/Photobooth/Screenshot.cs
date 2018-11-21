using UnityEngine;

/// <summary>
/// Holds data regarding screenshots.
/// </summary>
[System.Serializable]
public class Screenshot : Object
{
	public string fileURL;
	public Texture2D fileTexture;
	public Sprite fileSprite;
}
