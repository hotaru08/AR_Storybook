using UnityEngine;
using ATXK.EventSystem;
using ATXK.CustomVariables;
using System.Collections;

/// <summary>
/// Captures a screenshot and saves it.
/// </summary>
public class ScreenshotCapture : MonoBehaviour
{
	[Header("Screenshot")]
	[SerializeField] ES_Event_Default screenshot_Start;
	[SerializeField] ES_Event_Default screenshot_End;
	[SerializeField] CV_String fileFormat;
	[SerializeField] float screenshotTime;

	public void StartCapture()
	{
		StartCoroutine("CaptureScreenshot");
	}

	private IEnumerator CaptureScreenshot()
	{
		screenshot_Start.RaiseEvent();
		string filename = System.DateTime.Now.ToString(fileFormat.value);

#if !UNITY_EDITOR
		ScreenCapture.CaptureScreenshot(filename + ".png");
#endif

		yield return new WaitForSeconds(screenshotTime);
		screenshot_End.RaiseEvent();
	}
}