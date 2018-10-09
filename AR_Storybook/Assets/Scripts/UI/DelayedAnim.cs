using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedAnim : MonoBehaviour
{
	[SerializeField] float delayTime;
	[SerializeField] string animTrigger;

	private void Update()
	{
		delayTime -= Time.deltaTime;

		if(delayTime <= 0f)
		{
			delayTime = 0f;
			GetComponent<Animator>().SetTrigger(animTrigger);
			enabled = false;
		}
	}
}
