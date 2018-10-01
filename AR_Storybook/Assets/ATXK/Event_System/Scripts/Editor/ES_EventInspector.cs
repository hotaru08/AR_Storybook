namespace ATXK.EventSystem
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEditor;
	using UnityEngine;

	[CustomEditor(typeof(ES_Event), true)]
	public class ES_EventInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			ES_Event gameEvent = (ES_Event)target;
			if(GUILayout.Button("Raise Event"))
			{
				gameEvent.Invoke();
			}
		}
	}
}