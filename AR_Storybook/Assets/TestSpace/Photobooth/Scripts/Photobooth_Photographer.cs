namespace TestSpace
{
	using UnityEngine;
	using ATXK.EventSystem;
	using System.Collections;

	public class Photobooth_Photographer : MonoBehaviour
	{
		[SerializeField] ES_Event_Abstract takePhotoStartEvent;
		[SerializeField] ES_Event_Abstract takePhotoEndEvent;

		public void TakeScreenshot()
		{
			StartCoroutine(Screenshot());
		}

		private IEnumerator Screenshot()
		{
			takePhotoStartEvent.RaiseEvent();

			yield return new WaitForSeconds(0.2f);

			string fileName = "";
			fileName = System.DateTime.Now.ToString("d_M_yyyy_HHmmss");
			Debug.Log("Filename:" + fileName);

#if !UNITY_EDITOR
			ScreenCapture.CaptureScreenshot(fileName + ".png");
#endif

			yield return new WaitForSeconds(0.2f);

			takePhotoEndEvent.RaiseEvent();
		}
	}
}