namespace ATXK.DialogueSystem
{
	using UnityEngine;
	using EventSystem;

	/// <summary>
	/// Scene-independent asset that holds a tree of Dialogue Nodes.
	/// </summary>
	[CreateAssetMenu(menuName = "Dialogue/Tree")]
	public class DS_Tree : ScriptableObject
	{
		[Header("Dialogue Nodes")]
		[SerializeField] DS_Node startingNode;
		[SerializeField] DS_Node currentNode;
		[SerializeField] DS_Node previousNode;

		[Header("Game Events")]
		[SerializeField] ES_Event_Abstract changedNode;
		[SerializeField] ES_Event_Abstract dialogueEnd;

		#region Property Getters
		public DS_Node StartNode { get { return startingNode; } }
		public DS_Node CurrentNode { get { return currentNode; } }
		public DS_Node PreviousNode { get { return previousNode; } set { previousNode = value; } }
		#endregion

		/// <summary>
		/// Called on the frame when this object becomes enabled.
		/// </summary>
		private void OnEnable()
		{
			if (startingNode != null)
				Reset();
		}

		/// <summary>
		///	Resets the current node of this tree to the starting node.
		/// </summary>
		public void Reset()
		{
			currentNode = startingNode;
		}

		/// <summary>
		/// Changes the current node to the one at the provided index.
		/// </summary>
		/// <param name="index">Zero-based index of the node.</param>
		public void TraverseTree(int index = 0)
		{
			if (index >= 0 && index < currentNode.NextNodes.Length)
			{
				previousNode = currentNode;
				currentNode = currentNode.NextNodes[index];

				previousNode.Exit();
				currentNode.Enter();

				changedNode.RaiseEvent();

				if (currentNode.NextNodes.Length == 0)
					dialogueEnd.RaiseEvent();
			}
		}
	}
}