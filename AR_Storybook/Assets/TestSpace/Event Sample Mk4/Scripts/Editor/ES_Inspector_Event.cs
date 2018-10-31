namespace TestSpace.EventSystem.Inspector
{
	using UnityEngine;
	using UnityEditor;

	[CustomEditor(typeof(ES_Event_Base), true)]
	public class ES_Inspector_Event : Editor
	{
		//xd I suck dick
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			ES_Event_Base gameEvent = (ES_Event_Base)target;
			if (GUILayout.Button("Invoke Event"))
			{
				gameEvent.Invoke();
			}
		}
	}
}