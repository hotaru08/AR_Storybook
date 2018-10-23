using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.AI;
using ATXK.EventSystem;

[CreateAssetMenu(menuName = "AI/Decision/Wait for Seconds")]
public class AI_Decision_WaitForSeconds : AI_Decision
{
	[Tooltip("Time to wait before changing to next state.")]
	[SerializeField] float waitTime;
	[Tooltip("Time variance range.")]
	[SerializeField] float randomizer;

	Dictionary<AI_Controller, bool> registeredControllers = new Dictionary<AI_Controller, bool>();

	public override bool Decide(AI_Controller controller)
	{
		return Wait(controller);
	}

	private bool Wait(AI_Controller controller)
	{
		if(!registeredControllers.ContainsKey(controller))
		{
			controller.StartCoroutine(WaitForSeconds(controller));
		}
		else if(registeredControllers[controller])
		{
			registeredControllers.Remove(controller);
			return true;
		}

		return false;
	}

	private IEnumerator WaitForSeconds(AI_Controller controller)
	{
		registeredControllers.Add(controller, false);

		float runtime = waitTime + Random.Range(-randomizer, randomizer);
		Debug.Log("Runtime=" + runtime + " for AI=" + controller.gameObject.name + " " + controller.gameObject.GetInstanceID());
		while(runtime > 0f)
		{
			runtime -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		registeredControllers[controller] = true;
	}
}
