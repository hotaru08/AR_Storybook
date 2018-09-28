namespace ATXK.DialogueSystem.Examples
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class StartDialogue : MonoBehaviour
	{
		[SerializeField] EventSystem.ES_GameEvent startDialogue;

		// Update is called once per frame
		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Return))
				startDialogue.Invoke();
		}
	}
}