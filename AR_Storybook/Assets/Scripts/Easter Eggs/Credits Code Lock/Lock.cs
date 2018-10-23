using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.EventSystem;
using ATXK.UI.Mk2;
using System.Linq;

[RequireComponent(typeof(AudioSource))]
public class Lock : MonoBehaviour
{
	[Header("Combination Lock")]
	[SerializeField] List<string> codeLock = new List<string>();

	[Header("Events")]
	[SerializeField] UI_Screen_Mk2 scrumScreen;
	[SerializeField] ES_Event_UnityObject changeScreenEvent;

	[Header("Audio Clips")]
	[SerializeField] AudioClip correctButton;
	[SerializeField] AudioClip correctCombo;
	[SerializeField] AudioClip wrongCombo;

	List<string> runtimeCombination = new List<string>();
	int index;
	AudioSource audioSource;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void AddCharacter(string input)
	{
		if (input == codeLock[index])
		{
			if (index < codeLock.Count)
			{
				runtimeCombination.Add(input);
				index++;

				audioSource.PlayOneShot(correctButton);

				if (index > codeLock.Count - 1)
					index = codeLock.Count;
			}
		}
		else
		{
			runtimeCombination.Clear();
			index = 0;
		}
	}

	public void EnterCombination()
	{
		if (runtimeCombination.SequenceEqual(codeLock))
		{
			audioSource.PlayOneShot(correctCombo);

			StartCoroutine("WaitForAudio");
		}
		else
		{
			audioSource.PlayOneShot(wrongCombo);
		}

		runtimeCombination.Clear();
		index = 0;
	}

	IEnumerator WaitForAudio()
	{
		while(audioSource.isPlaying)
		{
			yield return new WaitForEndOfFrame();
		}

		changeScreenEvent.Invoke(scrumScreen);
	}
}
