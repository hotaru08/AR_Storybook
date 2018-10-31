namespace TestSpace.EventSystem.Inspector
{
	using UnityEditor;
	using UnityEngine;
	using UnityEngine.Events;

	[CustomPropertyDrawer(typeof(ES_EventAndResponses))]
	public class ES_Inspector_EventAndResponses : PropertyDrawer
	{
		const float extraLines = 6f;
		const float lineHeight = 16f;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			Rect eventRect = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
			DrawEvent(eventRect, property, label);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return base.GetPropertyHeight(property, label) * extraLines;
		}

		private void DrawEvent(Rect encapsulatingRect, SerializedProperty mainProperty, GUIContent mainLabel)
		{
			// Draw Event
			Rect position = new Rect(encapsulatingRect.x, encapsulatingRect.y, encapsulatingRect.width, encapsulatingRect.height / extraLines);
			SerializedProperty eventToRespondTo = mainProperty.FindPropertyRelative("eventToRespondTo");

			EditorGUI.indentLevel = 0;
			EditorGUI.PropertyField(position, eventToRespondTo, GUIContent.none);

			// Draw response
			Rect responseRect = new Rect(encapsulatingRect.x, encapsulatingRect.y + lineHeight, encapsulatingRect.width, encapsulatingRect.height / (extraLines - 1));
			DrawResponse(responseRect, mainProperty, mainLabel, eventToRespondTo);
		}

		private void DrawResponse(Rect encapsulatingRect, SerializedProperty mainProperty, GUIContent mainLabel, SerializedProperty eventProperty)
		{
			Rect responseRect = new Rect(encapsulatingRect.x, encapsulatingRect.y, encapsulatingRect.width, encapsulatingRect.height);

			// Draw Response
			if (eventProperty.objectReferenceValue is ES_Event_Default)
			{
				SerializedProperty response = mainProperty.FindPropertyRelative("defaultResponse");

				EditorGUI.PropertyField(responseRect, response, GUIContent.none);
			}
			else if (eventProperty.objectReferenceValue is ES_Event_Bool)
			{
				SerializedProperty response = mainProperty.FindPropertyRelative("boolResponse");

				EditorGUI.PropertyField(responseRect, response, GUIContent.none);
			}
		}
	}
}