namespace ATXK.DialogueSystem
{
	using UnityEngine;
	using ATXK.EventSystem;

	/// <summary>
	/// Scene-independent asset that data about a line of dialogue.
	/// </summary>
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

        [Header("Animation")]
        [SerializeField] RuntimeAnimatorController[] AnimatorsToPlay;

        [Header("Sounds")]
        [SerializeField] Sound bgm;

        [Header("Event Settings")]
		[SerializeField] ES_Event_String setAnimTime;
        [SerializeField] ES_Event_Object setBGM;
        [SerializeField] ES_Event_Object[] SetAnimatorsToPlay;
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
		/// <summary>
		/// Called on the frame when this object becomes enabled.
		/// </summary>
		private void OnEnable()
		{
			if (!startTimeNull)
				animStartTimeNullable = animStartTime;
			else
				animStartTimeNullable = null;
			if (!endTimeNull)
				animEndTimeNullable = animEndTime;
			else
				animEndTimeNullable = null;
        }

		/// <summary>
		/// Called when a tree enters this node.
		/// </summary>
		public void Enter()
		{
			// Send off event containing any relevant cutscene timing data
			if(setAnimTime != null && animStartTimeNullable != null && animEndTimeNullable != null)
			{
				setAnimTime.RaiseEvent(animStartTime.ToString() + "," + animEndTime.ToString());
			}

            // Send off any Sound events
            if (setBGM != null && bgm != null)
            {
                setBGM.RaiseEvent(bgm);
            }

            // Send off any animator events
            if (SetAnimatorsToPlay != null && AnimatorsToPlay != null)
            {
                for (int i = 0; i < SetAnimatorsToPlay.Length; ++i)
                {
                    SetAnimatorsToPlay[i].RaiseEvent(AnimatorsToPlay[i]);
                }
            }

            // Send off any node "enter" events
            for (int i = 0; i < nodeEvents.Length; i++)
			{
				if(nodeEvents[i].nodeInvoke == NodeEventSettings.ON_ENTER)
				{
					nodeEvents[i].nodeEvent.RaiseEvent();
				}
			}
		}

		/// <summary>
		/// Called when a tree exits this node.
		/// </summary>
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