namespace ATXK.DialogueSystem
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[CreateAssetMenu(menuName = "ATXK/Dialogue System/Node")]
	public class DS_Node : ScriptableObject
	{
		[Header("Character")]
		[SerializeField] Sprite charAvatar;
		[SerializeField] string charName;

		[Header("Dialogue")]
		[SerializeField] string sentence;
		[SerializeField] DS_Node[] nextNodes;

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
	}
}