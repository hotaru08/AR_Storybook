namespace ATXK.DialogueSystem
{

	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using EventSystem;

	[CreateAssetMenu(menuName = "ATXK/Dialogue System/Tree")]
	public class DS_Tree : ScriptableObject
	{
		[Header("Dialogue Nodes")]
		[SerializeField] DS_Node startingNode;
		[SerializeField] DS_Node currentNode;
		[SerializeField] DS_Node previousNode;

		[Header("Game Events")]
		[SerializeField] ES_GameEvent changedNode;
		[SerializeField] ES_GameEvent dialogueEnd;

		/// <summary>
		/// Current dialogue node.
		/// </summary>
		public DS_Node CurrentNode { get { return currentNode; } }
		/// <summary>
		/// Previous dialogue node.
		/// </summary>
		public DS_Node PreviousNode { get { return previousNode; } set { previousNode = value; } }

		private void OnEnable()
		{
			if (startingNode != null)
				currentNode = startingNode;
		}

		public void TraverseTree(int index = 0)
		{
			if (index >= 0 && index < currentNode.NextNodes.Length)
			{
				previousNode = currentNode;
				currentNode = currentNode.NextNodes[index];

				changedNode.Invoke();

				if (currentNode.NextNodes.Length == 0)
					dialogueEnd.Invoke();
			}
		}
	}
}