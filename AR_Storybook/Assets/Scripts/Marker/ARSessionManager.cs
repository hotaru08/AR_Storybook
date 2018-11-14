using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.Helper;
using GoogleARCore;

public class ARSessionManager : SingletonBehaviour<ARSessionManager>
{
	[SerializeField]
	private GameObject m_arCoreSessionPrefab;

	/// <summary>
	/// GameObject holding the AR session component
	/// </summary>
	private GameObject m_sessionObject;

	private void Awake()
	{
		m_sessionObject = Instantiate(m_arCoreSessionPrefab);
		Session = m_sessionObject.GetComponent<ARCoreSession>();
		Session.enabled = true;
	}

	public void ResetSession()
	{
		StartCoroutine("CreateNewSession");
	}

	public ARCoreSession Session { get; private set; }

	IEnumerator CreateNewSession()
	{
		//Disable session and destroy the holding gameobject
		Session.enabled = false;
		DestroyImmediate(m_sessionObject);

		yield return new WaitForEndOfFrame();

		//Create new session
		m_sessionObject = Instantiate(m_arCoreSessionPrefab);
		Session = m_sessionObject.GetComponent<ARCoreSession>();
		Session.enabled = true;
	}
}
