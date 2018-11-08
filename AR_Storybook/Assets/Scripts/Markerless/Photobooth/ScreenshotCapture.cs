using UnityEngine;
using ATXK.EventSystem;
using ATXK.CustomVariables;
using System.Collections;

public class ScreenshotCapture : MonoBehaviour
{
	[Header("Screenshot")]
	[SerializeField] ES_Event_Default screenshot_Start;
	[SerializeField] ES_Event_Default screenshot_End;
	[SerializeField] ES_Event_Default screenshot_Start_Arcade;
	[SerializeField] ES_Event_Default screenshot_End_Arcade;
	[SerializeField] CV_String fileFormat;

	[Header("Highscore")]
	[SerializeField] ES_Event_String setHighscoreText;
	[SerializeField] CV_String playerPref_HighScore;

	public void StartCapture(bool isArcade)
	{
		StartCoroutine("CaptureScreenshot", isArcade);
	}

	private IEnumerator CaptureScreenshot(bool isArcade)
	{
		screenshot_Start.RaiseEvent();
		if (isArcade)
		{
			screenshot_Start_Arcade.RaiseEvent();
			int score = PlayerPrefs.GetInt(playerPref_HighScore.value);
			setHighscoreText.RaiseEvent(score);
		}

		string filename = System.DateTime.Now.ToString(fileFormat.value);

#if !UNITY_EDITOR
		ScreenCapture.CaptureScreenshot(filename + ".png");
#endif

		yield return new WaitForSeconds(0.2f);

		if (isArcade)
		{
			screenshot_End_Arcade.RaiseEvent();
		}
		screenshot_End.RaiseEvent();
	}
}