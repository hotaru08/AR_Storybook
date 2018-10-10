namespace ATXK.DialogueSystem
{
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using Helper;

	public class DS_Manager : SingletonBehaviour<DS_Manager>
	{
		[Header("Dialogue Tree")]
		[SerializeField] DS_Tree dialogueTree;

		[Header("Dialogue Buttons")]
		[SerializeField] GameObject buttonArea;
		[SerializeField] GameObject buttonPrefab;
		List<GameObject> buttonList = new List<GameObject>();

		[Header("Character Attributes")]
		[SerializeField] Image npcAvatar;
		[SerializeField] Text npcName;

		#region Property Getters
		/// <summary>
		/// Dialogue Tree that will be used.
		/// </summary>
		public DS_Tree DialogueTree { get { return dialogueTree; } }
		#endregion

		#region Unity Methods
		private void OnEnable()
		{
			ResetTree();
		}

		private void Update()
		{
			if (dialogueTree != null)
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
		#endregion

		#region Class Methods
		public void TraverseTree(int index = 0)
		{
			if (dialogueTree != null)
			{
				dialogueTree.TraverseTree(index);
			}
		}

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

		public void ResetTree()
		{
			dialogueTree.Reset();
			TraverseTree();
			RedrawButtons();
		}
		#endregion
	}
}