﻿namespace ATXK.DialogueSystem
{
    using ATXK.EventSystem;
    using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	/// <summary>
	/// Dialogue specific Button.
	/// </summary>
	public class DS_Button : MonoBehaviour
	{
		DS_Tree dialogueTree;
		public int index;

        [Header("TypeWriter")]
		[SerializeField] Text text;
        [SerializeField] float m_typeSpeed;
		[SerializeField] bool m_typeComplete;
        [SerializeField] Button m_tapToContinue;
        private string m_currText;

        [Header("Audio")]
        [Tooltip("This is the event that will be sent to Audio Manager")]
        [SerializeField] ES_Event_Object m_audio;
        [Tooltip("This is the event containing the audio asset that is sent from node")]
        [SerializeField] ES_Event_Object m_audioEvent;
        private AudioAsset m_audioAsset;

        #region Unity Methods
		/// <summary>
		/// Unity Start function. Is called when the behaviour becomes enabled.
		/// </summary>
        private void Start()
		{
			m_typeComplete = true;
            m_audioAsset = m_audioEvent.Value as AudioAsset;

			dialogueTree = DS_Manager.Instance.DialogueTree;
			UpdateText();

            //GetComponent<Button>().onClick.AddListener(CheckNode);
            m_tapToContinue.onClick.AddListener(CheckNode);
        }
		#endregion

		#region Class Methods
		/// <summary>
		/// Checks if the tree can continue to the next node.
		/// </summary>
		private void CheckNode()
		{
            if (m_typeComplete)
			{
				if (dialogueTree.CurrentNode.IsQuestion && dialogueTree.CurrentNode != dialogueTree.PreviousNode)
				{
					DS_Manager.Instance.RedrawButtons();

					dialogueTree.PreviousNode = dialogueTree.CurrentNode;
				}
				else if (dialogueTree.CurrentNode.IsQuestion && dialogueTree.CurrentNode == dialogueTree.PreviousNode)
				{
					DS_Manager.Instance.TraverseTree(index);
					DS_Manager.Instance.RedrawButtons();
					DS_Manager.Instance.TraverseTree(0);
				}
				else
				{
					DS_Manager.Instance.TraverseTree(index);
				}
            }
			else
			{
				StopCoroutine("TypeText");
				text.text = m_currText;
				m_typeComplete = true;
            }
		}

		/// <summary>
		/// Updates the text of the button.
		/// </summary>
		public void UpdateText()
		{
			if (dialogueTree != null)
			{
                if (dialogueTree.CurrentNode.IsQuestion && dialogueTree.CurrentNode == dialogueTree.PreviousNode)
				{
					if (index < dialogueTree.CurrentNode.NextNodes.Length)
                    {
						if (m_typeComplete)
							StartCoroutine("TypeText", dialogueTree.CurrentNode.NextNodes[index].Sentence);
					}
				}
				else
				{
					if (m_typeComplete)
						StartCoroutine("TypeText", dialogueTree.CurrentNode.Sentence);
				}
			}
		}

        /// <summary>
        /// Coroutine to produce typewriter effect.
        /// </summary>
		/// <param name="_textToType">String of text that will be displayed.</param>
        private IEnumerator TypeText(string _textToType)
        {
			m_typeComplete = false;
            m_currText = _textToType;
			text.text = "";

			for (int i = 0; i < _textToType.Length; ++i)
            {
                text.text += _textToType[i];

                if (m_audio != null && m_audioAsset != null)
                    m_audio.RaiseEvent(m_audioAsset.Sounds[Random.Range(0, m_audioAsset.Sounds.Length)]);

                yield return new WaitForSeconds(m_typeSpeed);
            }
			m_typeComplete = true;
        }
		#endregion
	}
}