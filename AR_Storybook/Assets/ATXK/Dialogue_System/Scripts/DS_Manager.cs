﻿namespace ATXK.DialogueSystem
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Helper;
	using UnityEngine.UI;

	public class DS_Manager : SingletonBehaviour<DS_Manager>
	{
		[Header("Dialogue Tree")]
		[SerializeField] DS_Tree dialogueTree;

		[Header("Dialogue Buttons")]
		[SerializeField] GameObject buttonArea;
		[SerializeField] GameObject buttonPrefab;
		List<GameObject> buttonList;

		[Header("Character Attributes")]
		[SerializeField] Image npcAvatar;
		[SerializeField] Text npcName;

		/// <summary>
		/// Dialogue Tree that will be used.
		/// </summary>
		public DS_Tree DialogueTree { get { return dialogueTree; } }

		/// <summary>
		/// Unity Start function.
		/// </summary>
		private void Start()
		{
			buttonList = new List<GameObject>();

			TraverseTree();
			RedrawButtons();
		}

		private void Update()
		{
			if(dialogueTree != null)
			{
				if (dialogueTree.CurrentNode.IsQuestion && dialogueTree.CurrentNode != dialogueTree.PreviousNode)
				{
					npcAvatar.sprite = dialogueTree.CurrentNode.Avatar;
					npcName.text = dialogueTree.CurrentNode.Name;
				}
				else if (dialogueTree.CurrentNode.IsQuestion && dialogueTree.CurrentNode == dialogueTree.PreviousNode)
				{
					npcAvatar.sprite = dialogueTree.CurrentNode.NextNodes[0].Avatar;
					npcName.text = dialogueTree.CurrentNode.NextNodes[0].Name;
				}
				else
				{
					npcAvatar.sprite = dialogueTree.CurrentNode.Avatar;
					npcName.text = dialogueTree.CurrentNode.Name;
				}
			}
		}

		/// <summary>
		/// Tells the attached Dialogue Tree to move to to the next node.
		/// </summary>
		/// <param name="index"></param>
		public void TraverseTree(int index = 0)
		{ 
			if(dialogueTree != null)
			{
				dialogueTree.TraverseTree(index);
			}
		}

		/// <summary>
		/// Updates the dialogue buttons.
		/// </summary>
		public void RedrawButtons()
		{
			//Remove old buttons
			for (int i = buttonList.Count - 1; i >= 0; i--)
			{
				Destroy(buttonList[i]);
			}
			buttonList.Clear();

			//Create new buttons
			for (int i = 0; i < dialogueTree.CurrentNode.NextNodes.Length; i++)
			{
				GameObject button = Instantiate(buttonPrefab, buttonArea.transform);
				button.GetComponent<DS_Button>().index = i;

				buttonList.Add(button);
			}
		}
	}
}