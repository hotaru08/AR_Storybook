using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ATXK.Helper;

/// <summary>
/// System that handles Dialogues, including special effects ( eg. typewriter )
/// </summary>
public class DialogueSystem : SingletonBehaviour<DialogueSystem>
{

    [Tooltip("Drag drop textmeshpro display here")]
    public TextMeshProUGUI dialogueDisplay;

    [Tooltip("Dialogues here. Increase size for more lines")]
    public string[] dialogue;

    [Tooltip("Typing speed")]
    public float typeSpeed;

    [Tooltip("Insert dialogue blips")]
    public AudioClip dialogueVoice;

    [Tooltip("Insert button gameobject here")]
    public GameObject continueButton;

    private int i; //index


    // Use this for initialization
    void Start()
    {
        StartCoroutine(Type());
    }

    //Start typing dilogue in game
    IEnumerator Type()
    {
        foreach (char letter in dialogue[i].ToCharArray())
        {
            dialogueDisplay.text += letter; //typewriter
            GetComponent<AudioSource>().PlayOneShot(dialogueVoice);
            yield return new WaitForSeconds(typeSpeed); //delay speed
        }
    }

    //Continue to next sentence. Call this function in the button
    public void NextSentence()
    {
        continueButton.SetActive(false); //set to be unable to be spammed

        if (i < dialogue.Length - 1)  //if lesser than dialogue length,start typing
        {
            i++;
            dialogueDisplay.text = "";
            StartCoroutine(Type());
        }
        else //else stop typing
        {
            dialogueDisplay.text = "";
            continueButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //checks if this is the latest sentence to be able to go next
        if (dialogueDisplay.text == dialogue[i])
        {
            continueButton.SetActive(true);//set next button to be able to be pressed
        }
    }
}
