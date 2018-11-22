using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.EventSystem;

public class ES_AwakeInvokerObjectDelay : MonoBehaviour
{
	[SerializeField] ES_Event_Object autoEvent;
	[SerializeField] Object autoEventValue;
	[SerializeField] float delayTime = 0f;

	public void Start()
	{
		StartCoroutine(WaitThenInvoke());
	}

	IEnumerator WaitThenInvoke()
	{
		yield return new WaitForSeconds(delayTime);

		autoEvent.RaiseEvent(autoEventValue);
		Debug.Log("Event is raised");
	}
}
