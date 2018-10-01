namespace ATXK.EventSystem
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEditor;
	using UnityEngine;

	[CustomEditor(typeof(ES_GameEvent))]
	public class ES_GameEventInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			ES_GameEvent gameEvent = (ES_GameEvent)target;
			if(GUILayout.Button("Raise Event"))
			{
				gameEvent.Invoke();
			}
		}
	}
}