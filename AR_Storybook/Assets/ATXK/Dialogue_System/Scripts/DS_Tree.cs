namespace ATXK.DialogueSystem
{

	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using EventSystem;

	[CreateAssetMenu(menuName = "Dialogue/Tree")]
	public class DS_Tree : ScriptableObject
	{
		[Header("Dialogue Nodes")]
		[SerializeField] DS_Node startingNode;
		[SerializeField] DS_Node currentNode;
		[SerializeField] DS_Node previousNode;

		[Header("Game Events")]
		[SerializeField] ES_Event_Base changedNode;
		[SerializeField] ES_Event_Base dialogueEnd;

		#region Property Getters
		public DS_Node StartNode { get { return startingNode; } }
		public DS_Node CurrentNode { get { return currentNode; } }
		public DS_Node PreviousNode { get { return previousNode; } set { previousNode = value; } }
		#endregion

		private void OnEnable()
		{
			if (startingNode != null)
				Reset();
		}

		public void Reset()
		{
			currentNode = startingNode;
		}

		public void TraverseTree(int index = 0)
		{
			if (index >= 0 && index < currentNode.NextNodes.Length)
			{
				previousNode = currentNode;
				currentNode = currentNode.NextNodes[index];

				previousNode.Exit();
				currentNode.Enter();

				changedNode.Invoke();

				if (currentNode.NextNodes.Length == 0)
					dialogueEnd.Invoke();
			}
		}
	}
}