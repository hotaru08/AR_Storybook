namespace ATXK.DialogueSystem.Examples
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using EventSystem;

	public class StartDialogue : MonoBehaviour
	{
		[SerializeField] ES_Event_Base startDialogue;

		// Update is called once per frame
		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Return))
				startDialogue.Invoke();
		}
	}
}