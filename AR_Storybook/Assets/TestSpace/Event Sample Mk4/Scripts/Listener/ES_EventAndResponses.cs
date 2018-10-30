namespace TestSpace.EventSystem
{
	using UnityEngine;

	[System.Serializable]
	public class ES_EventAndResponses
	{
		[Header("Event")]
		[SerializeField] ES_Event_Base eventToRespondTo;

		[Header("Response Types")]
		[SerializeField] ES_Type_Default defaultReponse;
		[SerializeField] ES_Type_Bool boolResponse;
		[SerializeField] ES_Type_Int intResponse;
		[SerializeField] ES_Type_Float floatResponse;
		[SerializeField] ES_Type_String stringResponse;
		[SerializeField] ES_Type_Vector2 vector2Response;
		[SerializeField] ES_Type_Vector3 vector3Response;
		[SerializeField] ES_Type_Vector4 vector4Response;
		[SerializeField] ES_Type_Quaternion quaternionResponse;
		[SerializeField] ES_Type_UnityObject objectResponse;

		#region Properties
		// Event Properties
		public ES_Event_Base EventToRespondTo { get { return eventToRespondTo; } }

		// Response Properties
		public ES_Type_Default DefaultResponse { get { return defaultReponse; } }
		public ES_Type_Bool BoolResponse { get { return boolResponse; } }
		public ES_Type_Int IntResponse { get { return intResponse; } }
		public ES_Type_Float FloatResponse { get { return floatResponse; } }
		public ES_Type_String StringResponse { get { return stringResponse; } }
		public ES_Type_Vector2 Vector2Response { get { return vector2Response; } }
		public ES_Type_Vector3 Vector3Response { get { return vector3Response; } }
		public ES_Type_Vector4 Vector4Response { get { return vector4Response; } }
		public ES_Type_Quaternion QuaternionResponse { get { return quaternionResponse; } }
		public ES_Type_UnityObject ObjectResponse { get { return objectResponse; } }
		#endregion
	}
}