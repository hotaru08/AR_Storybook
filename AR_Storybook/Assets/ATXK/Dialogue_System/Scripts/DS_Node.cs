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
			public ES_Event_Abstract nodeEvent;
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
        [SerializeField] [TextArea(1, 10)] string sentence;
		[SerializeField] DS_Node[] nextNodes;

		[Header("Cutscene")]
		[SerializeField] float animStartTime;
		[SerializeField] bool startTimeNull;
		[SerializeField] float animEndTime;
		[SerializeField] bool endTimeNull;
		float? animStartTimeNullable, animEndTimeNullable;

		[Header("Event Settings")]
		[SerializeField] ES_Event_String setAnimTime;
		[SerializeField] NodeEvent[] nodeEvents;

		#region Properties
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
		private void OnEnable()
		{
			if(!startTimeNull)
				animStartTimeNullable = animStartTime;
			if(!endTimeNull)
				animEndTimeNullable = animEndTime;
        }

		public void Enter()
		{
			// Send off event containing any relevant cutscene timing data
			if(setAnimTime != null && animStartTime != null && animEndTime != null)
			{
				setAnimTime.RaiseEvent(animStartTime.ToString() + "," + animEndTime.ToString());
			}

			// Send off any node "enter" events
			for(int i = 0; i < nodeEvents.Length; i++)
			{
				if(nodeEvents[i].nodeInvoke == NodeEventSettings.ON_ENTER)
				{
					nodeEvents[i].nodeEvent.RaiseEvent();
				}
			}
		}

		public void Exit()
		{
			// Send off any node "exit" events
			for (int i = 0; i < nodeEvents.Length; i++)
			{
				if (nodeEvents[i].nodeInvoke == NodeEventSettings.ON_EXIT)
				{
					nodeEvents[i].nodeEvent.RaiseEvent();
				}
			}
		}
		#endregion
	}
}