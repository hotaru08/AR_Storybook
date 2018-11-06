using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch_Rotating : MonoBehaviour
{
	[SerializeField] float m_rotateSpeed;

	public void ChangeRotate(GameObject _objToChange, Vector3 direction, Touch _firstTouch)
	{
		_objToChange.transform.Rotate(direction * m_rotateSpeed * Time.deltaTime * _firstTouch.deltaPosition);
	}
}
