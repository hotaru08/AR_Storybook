using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK;
using GoogleARCore;

public class ARSessionManager : SingletonMono<ARSessionManager>
{
	[SerializeField]
	private GameObject m_arCoreSessionPrefab;

	/// <summary>
	/// GameObject holding the AR session component
	/// </summary>
	private GameObject m_sessionObject;
	/// <summary>
	/// Main AR session in the scene
	/// </summary>
	private ARCoreSession m_arSession;

	private void Start()
	{
		m_sessionObject = Instantiate(m_arCoreSessionPrefab);
		m_arSession = m_sessionObject.GetComponent<ARCoreSession>();
		m_arSession.enabled = true;
	}

	public void ResetSession()
	{
		StartCoroutine("CreateNewSession");
	}

	IEnumerator CreateNewSession()
	{
		//Disable session and destroy the holding gameobject
		m_arSession.enabled = false;
		if (m_sessionObject != null)
			Destroy(m_sessionObject);

		yield return new WaitForEndOfFrame();

		//Create new session
		m_sessionObject = Instantiate(m_arCoreSessionPrefab);
		m_arSession = m_sessionObject.GetComponent<ARCoreSession>();
		m_arSession.enabled = true;
	}
}
