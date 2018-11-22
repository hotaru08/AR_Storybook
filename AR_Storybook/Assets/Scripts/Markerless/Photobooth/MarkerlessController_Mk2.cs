using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using ATXK.EventSystem;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(ES_EventListener))]
[RequireComponent(typeof(Touch_Scaling))]
[RequireComponent(typeof(Touch_Rotating))]
public class MarkerlessController_Mk2 : MonoBehaviour
{
	[Header("AR Variables")]
	[SerializeField] Camera arCamera;
	[SerializeField] Slider sliderScale;
	[SerializeField] Slider sliderRotation;

	Touch_Scaling touchScaler;
	Touch_Rotating touchRotater;

	[Header("AR Prefabs")]
	[SerializeField] GameObject detectedPlanePrefab;
	[SerializeField] GameObject visualizerPrefab;

	[Header("AR Settings")]
	[SerializeField] int numberOfVisualizersAllowed;

	bool isQuitting;
	int visualizerCount;
	[SerializeField] GameObject arObject;
	List<DetectedPlane> detectedPlanes = new List<DetectedPlane>();

	private void Awake()
	{
		// Use scaling component if slider isn't set/available
		if (sliderScale == null)
			touchScaler = GetComponent<Touch_Scaling>();
		// Use rotation component if slider isn't set/available
		if (sliderRotation == null)
			touchRotater = GetComponent<Touch_Rotating>();
	}

	void Update()
	{
		UpdateApplicationLifecycle();
		UpdateInstantiateOnTouch();
	}

	/// <summary>
	/// Updates the application and checks for error status.
	/// </summary>
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

	/// <summary>
	/// Checks for screen touches and updates accordingly.
	/// </summary>
	void UpdateInstantiateOnTouch()
	{
		if(Input.touchCount > 0)
		{
			InstantiateARVisualizer();
			GestureScale();
			GestureRotate();
		}
	}

	/// <summary>
	/// Shows a popup message with the given string.
	/// </summary>
	/// <param name="message">Message to display.</param>
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

	/// <summary>
	/// Updates the scale of the visualizer.
	/// </summary>
	void GestureScale()
	{
		if (arObject == null)
			return;

		if (sliderScale == null) // Using touch-based scaling
		{
			if (Input.touchCount == 2)
			{
				touchScaler.ChangeScaling(arObject, Input.GetTouch(0), Input.GetTouch(1));
			}
		}
		else if (sliderScale != null) // Using slider-based scaling
		{
			arObject.transform.localScale = new Vector3(sliderScale.value, sliderScale.value, sliderScale.value);
		}
	}

	/// <summary>
	/// Updates the rotation of the visualizer.
	/// </summary>
	void GestureRotate()
	{
		if (arObject == null)
			return;

		if (sliderRotation == null) // Using touch-based rotating
		{
			if (Input.touchCount == 1)
			{
				Touch touch = Input.GetTouch(0);
				if (touch.phase == TouchPhase.Moved)
				{
					touchRotater.ChangeRotate(arObject, Vector3.up, touch);
				}
			}
		}
		else if (sliderRotation != null) // Using slider-based rotating
		{
			//arObject.transform.Rotate(Vector3.up, sliderScale.value);
			arObject.transform.rotation = Quaternion.Euler(0f, sliderRotation.value, 0f);
		}
	}

	/// <summary>
	/// Instantiates a visualizer.
	/// </summary>
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

	/// <summary>
	/// Changes the visualizer object, if the Object provided is a GameObject.
	/// </summary>
	/// <param name="_arObject"></param>
	public void SetObject(Object _arObject)
	{
		Debug.Log("SET OBJECT");

		GameObject arObj = _arObject as GameObject;
		if(arObj != null)
		{
			Vector3 oldPos = Vector3.zero;
			Quaternion oldRot = Quaternion.identity;

			if (arObject != null)
			{
				oldPos = arObject.transform.position;
				oldRot = arObject.transform.rotation;

				DestroyImmediate(arObject);
			}


			arObject = Instantiate(arObj, oldPos, oldRot);
		}
	}

	/// <summary>
	/// Quits the application.
	/// </summary>
	public void DoQuit()
	{
		Application.Quit();
	}
}