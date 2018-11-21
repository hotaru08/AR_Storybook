using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareScreenshot : MonoBehaviour
{
	[SerializeField] ScreenshotViewer gallery;
	Screenshot currScreenshot;
	bool isFocus;

	/// <summary>
	/// Grabs the current image allows sharing of the image to any other app that supports the file format.
	/// </summary>
	public void Share()
	{
#if UNITY_EDITOR
		Debug.Log("ShareScreenshot.Share() has been called in a non-Android OS.");
#else
		StartCoroutine("AndroidShare");
#endif
	}

	/// <summary>
	/// Coroutine that opens a selection page using native Android code to share the current image.
	/// </summary>
	private IEnumerator AndroidShare()
	{
		// Get current screenshot
		currScreenshot = gallery.CurrentScreenshot;

		// Create intent
		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
		// Fill intent with screenshot
		AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
		AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "content://" + currScreenshot.fileURL);
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
		intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
		// Get unity player
		AndroidJavaClass unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject unityObject = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
		// Show chooser
		AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share your image!");
		unityObject.Call("startActivity", chooser);

		// Wait till game is back in focus
		yield return new WaitUntil(() => isFocus);
	}

	private void OnApplicationFocus(bool focus)
	{
		isFocus = focus;
	}
}
