namespace TestSpace
{
	using UnityEngine;
	using ATXK.EventSystem;

	[RequireComponent(typeof(ES_EventListener))]
	public class Lane : MonoBehaviour
	{
		public Transform enemyPosition;
		public Transform playerPosition;
	}
}