using UnityEngine;
using ATXK.EventSystem;
using ATXK.CustomVariables;
using System.Collections;

public class ScreenshotCapture : MonoBehaviour
{
	[Header("Screenshot")]
	[SerializeField] ES_Event_Default screenshot_Start;
	[SerializeField] ES_Event_Default screenshot_End;
	[SerializeField] CV_String fileFormat;
	[SerializeField] float screenshotTime;

	public void StartCapture(bool isArcade)
	{
		StartCoroutine("CaptureScreenshot", isArcade);
	}

	private IEnumerator CaptureScreenshot(bool isArcade)
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