namespace TestSpace
{
	using UnityEngine;
	using ATXK.EventSystem;
	using System.Collections;

	public class Photobooth_Photographer : MonoBehaviour
	{
		[SerializeField] ES_Event_Base takePhotoStartEvent;
		[SerializeField] ES_Event_Base takePhotoEndEvent;

		public void TakeScreenshot()
		{
			StartCoroutine(Screenshot());
		}

		private IEnumerator Screenshot()
		{
			takePhotoStartEvent.Invoke();

			yield return new WaitForSeconds(0.2f);

			string fileName = "";
			fileName = System.DateTime.Now.ToString("d_M_yyyy_HHmmss");
			Debug.Log("Filename:" + fileName);

#if !UNITY_EDITOR
			ScreenCapture.CaptureScreenshot(fileName + ".png");
#endif

			yield return new WaitForSeconds(0.2f);

			takePhotoEndEvent.Invoke();
		}
	}
}