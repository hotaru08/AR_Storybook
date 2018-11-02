namespace ATXK.EventSystem.Editor
{
	using UnityEngine;
	using UnityEditor;
	using UnityEngine.Events;
	using System.Collections.Generic;

	[CustomEditor(typeof(ES_EventListener))]
	public class ES_ListenerInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			ES_EventListener listener = (ES_EventListener)target;

			// Event Array
			EditorGUILayout.LabelField("Events", EditorStyles.boldLabel);

			SerializedProperty listeningToEvent = serializedObject.FindProperty("listeningToEvent");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(listeningToEvent, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			// Get Event
			ES_Event_Abstract abstractEvent = listeningToEvent.objectReferenceValue as ES_Event_Abstract;

			// Event Responses
			EditorGUILayout.LabelField("Responses", EditorStyles.boldLabel);

			if (abstractEvent is ES_Event_Default)
			{
				SerializedProperty response = serializedObject.FindProperty("defaultResponse");
				EditorGUILayout.PropertyField(response);
			}
			else if (abstractEvent is ES_Event_Bool)
			{
				SerializedProperty response = serializedObject.FindProperty("boolResponse");
				EditorGUILayout.PropertyField(response);
			}
			else if (abstractEvent is ES_Event_Int)
			{
				SerializedProperty response = serializedObject.FindProperty("intResponse");
				EditorGUILayout.PropertyField(response);
			}
			else if (abstractEvent is ES_Event_Float)
			{
				SerializedProperty response = serializedObject.FindProperty("floatResponse");
				EditorGUILayout.PropertyField(response);
			}
			else if (abstractEvent is ES_Event_String)
			{
				SerializedProperty response = serializedObject.FindProperty("stringResponse");
				EditorGUILayout.PropertyField(response);
			}
			else if (abstractEvent is ES_Event_Object)
			{
				SerializedProperty response = serializedObject.FindProperty("objectResponse");
				EditorGUILayout.PropertyField(response);
			}
			else if(abstractEvent is ES_Event_Vector2)
			{
				SerializedProperty response = serializedObject.FindProperty("vector2Response");
				EditorGUILayout.PropertyField(response);
			}
			else if (abstractEvent is ES_Event_Vector3)
			{
				SerializedProperty response = serializedObject.FindProperty("vector3Response");
				EditorGUILayout.PropertyField(response);
			}
			else if (abstractEvent is ES_Event_Vector4)
			{
				SerializedProperty response = serializedObject.FindProperty("vector4Response");
				EditorGUILayout.PropertyField(response);
			}
			else if (abstractEvent is ES_Event_Quaternion)
			{
				SerializedProperty response = serializedObject.FindProperty("quaternionResponse");
				EditorGUILayout.PropertyField(response);
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}