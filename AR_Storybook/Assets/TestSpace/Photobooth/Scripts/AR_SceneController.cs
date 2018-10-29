namespace TestSpace
{
	using UnityEngine;
	using ATXK.EventSystem;	
	
	public class AR_SceneController : MonoBehaviour
	{
		[Header("AR Variables")]
		[SerializeField] Camera m_ARCamera;
		[SerializeField] GameObject m_detectedPlanePrefab;

		[Header("Events")]
		[SerializeField] ES_Event_Base m_spawnedVisualizerEvent;
	}
}