namespace ATXK.EventSystem
{
	using System;
	using UnityEngine;
	using UnityEngine.Events;
	using Object = UnityEngine.Object;

	/// <summary>
	/// UnityEvent response for boolean events.
	/// </summary>
	[Serializable] public class BoolEvent : UnityEvent<bool> { }
	/// <summary>
	/// UnityEvent response for integer events.
	/// </summary>
	[Serializable] public class IntEvent : UnityEvent<int> { }
	/// <summary>
	/// UnityEvent response for floating-point events.
	/// </summary>
	[Serializable] public class FloatEvent : UnityEvent<float> { }
	/// <summary>
	/// UnityEvent response for string events.
	/// </summary>
	[Serializable] public class StringEvent : UnityEvent<string> { }
	/// <summary>
	/// UnityEvent response for Unity Object events.
	/// </summary>
	[Serializable] public class ObjectEvent : UnityEvent<Object> { }
	/// <summary>
	/// UnityEvent response for Vector2 events.
	/// </summary>
	[Serializable] public class Vector2Event : UnityEvent<Vector2> { }
	/// <summary>
	/// UnityEvent response for Vector3 events.
	/// </summary>
	[Serializable] public class Vector3Event : UnityEvent<Vector3> { }
	/// <summary>
	/// UnityEvent response for Vector4 events.
	/// </summary>
	[Serializable] public class Vector4Event : UnityEvent<Vector4> { }
	/// <summary>
	/// UnityEvent response for Quaternion events.
	/// </summary>
	[Serializable] public class QuaternionEvent : UnityEvent<Quaternion> { }
}