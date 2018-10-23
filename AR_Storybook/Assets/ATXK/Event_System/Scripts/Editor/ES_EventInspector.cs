namespace ATXK.EventSystem.Editor
{
	using UnityEditor;
	using UnityEngine;

	[CustomEditor(typeof(ES_Event_Abstract), true)]
	public class ES_EventInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			ES_Event_Abstract gameEvent = (ES_Event_Abstract)target;
			if(GUILayout.Button("Invoke Event"))
			{
				gameEvent.Invoke();
			}
		}
	}
}