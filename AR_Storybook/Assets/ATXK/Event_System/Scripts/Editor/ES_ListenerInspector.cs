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

			SerializedProperty events = serializedObject.FindProperty("eventsToListenFor");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(events, true);
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();

			// Get List of Events
			List<ES_Event_Abstract> eventList = new List<ES_Event_Abstract>();
			SerializedProperty eventsCopy = events.Copy();
			if (eventsCopy.isArray)
			{
				int listLength = 0;
				eventsCopy.Next(true);		// skip generic field
				eventsCopy.Next(true);      // advance to array size field

				listLength = eventsCopy.intValue;    // Get the array size

				eventsCopy.Next(true);      // advance to first array index

				int lastIndex = listLength - 1;
				for(int i = 0; i < listLength; i++)
				{
					ES_Event_Abstract temp = eventsCopy.objectReferenceValue as ES_Event_Abstract;
					if (temp != null)
						eventList.Add(temp);

					if (i < lastIndex)
						eventsCopy.Next(false);
				}
			}

			// Event Responses
			EditorGUILayout.LabelField("Responses", EditorStyles.boldLabel);

			if (eventList.Find(x => x is ES_Event_Default))
			{
				SerializedProperty defaultResponse = serializedObject.FindProperty("responseToDefaultEvent");
				EditorGUILayout.PropertyField(defaultResponse);
			}

			if (eventList.Find(x => x is ES_Event_Bool))
			{
				SerializedProperty boolResponse = serializedObject.FindProperty("responseToBoolEvent");
				EditorGUILayout.PropertyField(boolResponse);
			}

			if(eventList.Find(x => x is ES_Event_Int))
			{
				SerializedProperty intResponse = serializedObject.FindProperty("responseToIntEvent");
				EditorGUILayout.PropertyField(intResponse);
			}

			if(eventList.Find(x => x is ES_Event_Float))
			{
				SerializedProperty floatResponse = serializedObject.FindProperty("responseToFloatEvent");
				EditorGUILayout.PropertyField(floatResponse);
			}

			if(eventList.Find(x => x is ES_Event_String))
			{
				SerializedProperty stringResponse = serializedObject.FindProperty("responseToStringEvent");
				EditorGUILayout.PropertyField(stringResponse);
			}

			if(eventList.Find(x => x is ES_Event_UnityObject))
			{
				SerializedProperty objectResponse = serializedObject.FindProperty("responseToObjectEvent");
				EditorGUILayout.PropertyField(objectResponse);
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}