namespace ATXK.DialogueSystem
{
	using UnityEngine;
	using ATXK.EventSystem;

	[CreateAssetMenu(menuName = "Dialogue/Node")]
	public class DS_Node : ScriptableObject
	{
		[System.Serializable]
		class NodeEvent
		{
			public ES_Event nodeEvent;
			public NodeEventSettings nodeInvoke;
		}

		enum NodeEventSettings
		{
			ON_ENTER,
			ON_EXIT
		}

		[Header("Character")]
		[SerializeField] Sprite charAvatar;
		[SerializeField] string charName;

		[Header("Dialogue")]
		[SerializeField] string sentence;
		[SerializeField] DS_Node[] nextNodes;

		[Header("Event Settings")]
		//[SerializeField] ES_Event nodeEvent;
		//[SerializeField] NodeEventSettings nodeInvoke;
		[SerializeField] NodeEvent[] nodeEvents;

		#region Property Getters
		/// <summary>
		/// Image of the speaker.
		/// </summary>
		public Sprite Avatar { get { return charAvatar; } }
		/// <summary>
		/// Name of the speaker.
		/// </summary>
		public string Name { get { return charName; } }
		/// <summary>
		/// Sentence that the character will say.
		/// </summary>
		public string Sentence { get { return sentence; } }
		/// <summary>
		/// Possible replies/sentences to this node.
		/// </summary>
		public DS_Node[] NextNodes { get { return nextNodes; } }
		/// <summary>
		/// Flag if this node is a question or a normal dialogue node.
		/// </summary>
		public bool IsQuestion { get { return nextNodes.Length > 1; } }
		#endregion

		#region Class Methods
		public void Enter()
		{
			//if (nodeInvoke == NodeEventSettings.ON_ENTER && nodeEvent != null)
			//	nodeEvent.Invoke();

			for(int i = 0; i < nodeEvents.Length; i++)
			{
				if(nodeEvents[i].nodeInvoke == NodeEventSettings.ON_ENTER)
				{
					nodeEvents[i].nodeEvent.Invoke();
				}
			}
		}

		public void Exit()
		{
			//if (nodeInvoke == NodeEventSettings.ON_EXIT && nodeEvent != null)
			//	nodeEvent.Invoke();

			for (int i = 0; i < nodeEvents.Length; i++)
			{
				if (nodeEvents[i].nodeInvoke == NodeEventSettings.ON_EXIT)
				{
					nodeEvents[i].nodeEvent.Invoke();
				}
			}
		}
		#endregion
	}
}