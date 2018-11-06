using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using ATXK.EventSystem;
using System.Collections.Generic;

[RequireComponent(typeof(ES_EventListener))]
[RequireComponent(typeof(Touch_Scaling))]
[RequireComponent(typeof(Touch_Rotating))]
public class MarkerlessController_Mk2 : MonoBehaviour
{
	[Header("AR Variables")]
	[SerializeField] Camera arCamera;
	[SerializeField] Touch_Scaling touchScaler;
	[SerializeField] Touch_Rotating touchRotater;

	[Header("AR Prefabs")]
	[SerializeField] GameObject detectedPlanePrefab;
	[SerializeField] GameObject visualizerPrefab;

	[Header("AR Settings")]
	[SerializeField] int numberOfVisualizersAllowed;

	bool isQuitting;
	int visualizerCount;
	GameObject arObject;
	List<DetectedPlane> detectedPlanes = new List<DetectedPlane>();

	void Update()
	{
		UpdateApplicationLifecycle();
		UpdateInstantiateOnTouch();
	}

	void UpdateApplicationLifecycle()
	{
		if(Session.Status != SessionStatus.Tracking)
			Screen.sleepTimeout = 15;
		else
			Screen.sleepTimeout = SleepTimeout.NeverSleep;

		if (isQuitting)
			return;

		if(Session.Status == SessionStatus.ErrorApkNotAvailable)
		{
			ShowAndroidToastMessage("Camera permission is needed to run this application.");
			isQuitting = true;
			Invoke("DoQuit", 0.5f);
		}
		else if (Session.Status.IsError())
		{
			ShowAndroidToastMessage("ARCore encountered a problem, please restart this application.");
			isQuitting = true;
			Invoke("DoQuit", 0.5f);
		}
	}

	void UpdateInstantiateOnTouch()
	{
		if(Input.touchCount > 0)
		{
			InstantiateARVisualizer();
			GestureScale();
			GestureRotate();
		}
	}

	void ShowAndroidToastMessage(string message)
	{
		AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

		if (unityActivity != null)
		{
			AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
			unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
			{
				AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
				toastObject.Call("show");
			}));
		}
	}

	void GestureScale()
	{
		// Two fingers on screen
		//if(Input.touchCount == 2)
		//{
		//	Touch touchOne = Input.GetTouch(0);
		//	Touch touchTwo = Input.GetTouch(1);

		//	Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
		//	Vector2 touchTwoPrevPos = touchTwo.position - touchTwo.deltaPosition;

		//	float prevTouchDeltaMagnitude = (touchOnePrevPos - touchTwoPrevPos).magnitude;
		//	float currTouchDeltaMagnitude = (touchOne.position - touchTwo.position).magnitude;
		//	float deltaMagnitudeDiff = prevTouchDeltaMagnitude - currTouchDeltaMagnitude;

		//	float scaleAmount = deltaMagnitudeDiff * scaleSpeed * Time.deltaTime;
		//	arObject.transform.localScale += new Vector3(scaleAmount, scaleAmount, scaleAmount);
		//}

		// SWAP OUT TO CALL TOUCH_SCALING SCRIPT FUNCTION INSTEAD

		// Two fingers on screen
		if(Input.touchCount == 2 && arObject != null)
		{
			touchScaler.ChangeScaling(arObject, Input.GetTouch(0), Input.GetTouch(1));
		}
	}

	void GestureRotate()
	{
		// One moving finger on screen
		//if(Input.touchCount == 1)
		//{
		//	Touch touch = Input.GetTouch(0);
		//	if(touch.phase == TouchPhase.Moved)
		//	{
		//		arObject.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * touch.deltaPosition);
		//	}
		//}

		// SWAP OUT TO CALL TOUCH_ROTATING SCRIPT FUNCTION INSTEAD

		// One moving finger on screen
		if(Input.touchCount == 1 && arObject != null)
		{
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Moved)
			{
				touchRotater.ChangeRotate(arObject, Vector3.up, touch);
			}
		}
	}

	void InstantiateARVisualizer()
	{
		Touch touch;
		touch = Input.GetTouch(0);

		TrackableHit hit;
		TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;

		if(touch.phase == TouchPhase.Began)
		{
			if(Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
			{
				if(visualizerCount < numberOfVisualizersAllowed)
				{
					Destroy(arObject);

					if((hit.Trackable is DetectedPlane) && Vector3.Dot(arCamera.transform.position - hit.Pose.position, hit.Pose.rotation * Vector3.up) < 0)
					{
						return;
					}
					else
					{
						arObject = Instantiate(visualizerPrefab, hit.Pose.position, hit.Pose.rotation);
						arObject.transform.Rotate(0, 180f, 0, Space.Self);

						Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);
						arObject.transform.parent = anchor.transform;
						visualizerCount++;
						
						foreach(GameObject plane in DetectedPlaneGenerator_Mk2.Instance.DetectedPlanes)
						{
							plane.SetActive(false);
						}
					}
				}
			}
		}
	}

	public void SetObject(Object _arObject)
	{
		GameObject arObj = _arObject as GameObject;
		if(arObj != null)
		{
			Vector3 oldPos = arObject.transform.position;
			Quaternion oldRot = arObject.transform.rotation;

			DestroyImmediate(arObject);
			arObject = Instantiate(arObj, oldPos, oldRot);
		}
	}

	public void DoQuit()
	{
		Application.Quit();
	}
}