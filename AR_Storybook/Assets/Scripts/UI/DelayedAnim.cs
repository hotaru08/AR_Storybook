using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DelayedAnim : MonoBehaviour
{
	[SerializeField] float delayTime;
	[SerializeField] string animTrigger;

	public float DelayTime { get { return delayTime; } }

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
