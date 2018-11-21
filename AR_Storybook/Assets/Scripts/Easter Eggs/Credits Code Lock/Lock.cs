using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.EventSystem;
using ATXK.UI.Mk2;
using ATXK.CustomVariables;
using System.Linq;

/// <summary>
/// Combination lock using scene-independent strings as the lock values.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class Lock : MonoBehaviour
{
	[Header("Combination Lock")]
	[SerializeField] List<CV_String> codeLock = new List<CV_String>();

	[Header("Screen")]
	[SerializeField] UI_Screen_Mk2 scrumScreen;

	[Header("Events")]
	[SerializeField] ES_Event_Object changeScreenEvent;

	[Header("Audio Clips")]
	[SerializeField] AudioClip correctButton;
	[SerializeField] AudioClip correctCombo;

	List<string> runtimeCombination = new List<string>();
	List<string> runtimeLock = new List<string>();

	int index;
	AudioSource audioSource;

	private void Start()
	{
		index = 0;
		audioSource = GetComponent<AudioSource>();

		foreach(CV_String cvString in codeLock)
		{
			runtimeLock.Add(cvString.value);
		}
	}

	/// <summary>
	/// Adds a new string value to the current list of input values.
	/// </summary>
	/// <param name="input">String to add to input values.</param>
	public void AddCharacter(string input)
	{
		if (input == codeLock[index].value)
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

	/// <summary>
	/// Adds a new string value to the current list of input values, if the Object provided is type of CV_String.
	/// </summary>
	/// <param name="input">String to add to input values.</param>
	public void AddCharacter(Object input)
	{
		CV_String cvString = input as CV_String;
		if(cvString != null)
		{
			AddCharacter(cvString.value);
		}
	}

	/// <summary>
	/// Checks if the current input combination is correct, if so then change scene.
	/// </summary>
	public void EnterCombination()
	{
		if (runtimeCombination.SequenceEqual(runtimeLock))
		{
			StartCoroutine("WaitForAudio");
		}

		runtimeCombination.Clear();
		index = 0;
	}

	IEnumerator WaitForAudio()
	{
		if (audioSource.isPlaying)
			yield return null;

		audioSource.PlayOneShot(correctCombo);
		while (audioSource.isPlaying)
		{
			yield return new WaitForEndOfFrame();
		}

		changeScreenEvent.RaiseEvent(scrumScreen);
	}
}
