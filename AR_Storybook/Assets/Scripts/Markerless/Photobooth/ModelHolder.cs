using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.EventSystem;

[RequireComponent(typeof(ES_EventListener))]
public class ModelHolder : MonoBehaviour
{
	[SerializeField] ES_Event_Object changedModel;
	[SerializeField] List<GameObject> models = new List<GameObject>();

	int currIndex;

	private void Start()
	{
		currIndex = 0;
	}

	/// <summary>
	/// Changes to the next model in the list, with wraparound to the first model in the list.
	/// </summary>
	public void NextModel()
	{
		currIndex++;
		if(currIndex >= models.Count)
		{
			currIndex = 0;
		}

		changedModel.RaiseEvent(models[currIndex]);
	}

	/// <summary>
	/// Changes to the previous model in the list, with wraparound to the last model in the list.
	/// </summary>
	public void PrevModel()
	{
		currIndex--;
		if (currIndex < 0)
		{
			currIndex = models.Count - 1;
		}

		changedModel.RaiseEvent(models[currIndex]);
	}
}
