using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(TextMeshProUGUI))]

public class TypewriterDialogue : MonoBehaviour {

    public string dialogue = "Dialogue here";
    private TextMeshProUGUI textComp;

    [Tooltip("Delay at the begining")]
    public float startDelay = 0f;

    [Tooltip("Delay with the typing")]
    public float typingDelay = 0.01f;

    public AudioClip dialogueVoice;

	// Use this for initialization
	void Start () {
        StartCoroutine("TypeIn");
	}

    void Awake()
    {
        textComp = GetComponent<TextMeshProUGUI>();
    }

    public IEnumerator TypeIn()
    {
        yield return new WaitForSeconds(startDelay);

        for (int i = 0; i <= dialogue.Length; i++)
        {
            textComp.text = dialogue.Substring(0, i); //start index, index length
            GetComponent<AudioSource>().PlayOneShot(dialogueVoice);
            yield return new WaitForSeconds(typingDelay);
        }
    }

    public IEnumerator TypeOff()
    {
        for (int i = dialogue.Length; i >= 0; i--) 
        {
            textComp.text = dialogue.Substring(0, i);
            yield return new WaitForSeconds(typingDelay);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
