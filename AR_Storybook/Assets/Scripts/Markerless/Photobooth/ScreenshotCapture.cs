using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.EventSystem;

[RequireComponent(typeof(ES_EventListener))]
public class ScreenshotCapture : MonoBehaviour
{
	[SerializeField] ES_Event_Default screenshot_Start;
	[SerializeField] ES_Event_Default screenshot_End;

	public void StartCapture()
	{
		screenshot_Start.RaiseEvent();

		string filename = System.DateTime.Now.ToString("d_M_yyy_HHmmss");

#if !UNITY_EDITOR
		ScreenCapture.CaptureScreenshot(filename + ".png");
#endif

		screenshot_End.RaiseEvent();
	}
}
