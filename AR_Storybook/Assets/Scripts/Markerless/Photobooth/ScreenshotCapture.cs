using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.EventSystem;
using ATXK.CustomVariables;

[RequireComponent(typeof(ES_EventListener))]
public class ScreenshotCapture : MonoBehaviour
{
	[Header("Screenshot")]
	[SerializeField] ES_Event_Default screenshot_Start;
	[SerializeField] ES_Event_Default screenshot_End;

	[Header("Highscore")]
	[SerializeField] ES_Event_String setHighscoreText;
	[SerializeField] CV_Int score;

	public void StartCapture()
	{
		screenshot_Start.RaiseEvent();
		setHighscoreText.RaiseEvent(score.value);

		string filename = System.DateTime.Now.ToString("d_M_yyy_HHmmss");

#if !UNITY_EDITOR
		ScreenCapture.CaptureScreenshot(filename + ".png");
#endif

		screenshot_End.RaiseEvent();
	}
}