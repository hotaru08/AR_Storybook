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
	/// <summary>
	/// Main AR session in the scene
	/// </summary>
	private ARCoreSession m_arSession;

	private void Awake()
	{
		m_sessionObject = Instantiate(m_arCoreSessionPrefab);
		m_arSession = m_sessionObject.GetComponent<ARCoreSession>();
		m_arSession.enabled = true;
	}

	/// <summary>
	/// Destroys the current session and re-instantiates a new instance of the session.
	/// </summary>
	public void ResetSession()
	{
		StartCoroutine("CreateNewSession");
	}

	public ARCoreSession GetSession()
	{
		if (m_arSession != null)
		{
			return m_arSession;
		}
		else
		{
			return null;
		}
	}

	/// <summary>
	/// Destroys the current session and re-instantiates a new instance of the session.
	/// </summary>
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
