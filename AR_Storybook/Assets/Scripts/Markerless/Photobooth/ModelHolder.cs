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
		changedModel.RaiseEvent(models[currIndex]);
	}

	public void NextModel()
	{
		currIndex++;
		if(currIndex >= models.Count)
		{
			currIndex = 0;
		}

		changedModel.RaiseEvent(models[currIndex]);
	}

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
