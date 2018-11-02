namespace ATXK.DialogueSystem
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	public class DS_Button : MonoBehaviour
	{
		DS_Tree dialogueTree;
		public int index;

        /// <summary>
        /// Typewriter Variables
        /// </summary>
		[SerializeField] Text text;
        [SerializeField] private float m_typeSpeed;
		[SerializeField] private bool m_typeComplete;

		private string m_currText;

		#region Unity Methods
		private void Start()
		{
			m_typeComplete = true;

			dialogueTree = DS_Manager.Instance.DialogueTree;
			UpdateText();

			GetComponent<Button>().onClick.AddListener(CheckNode);
		}
		#endregion

		#region Class Methods
		private void CheckNode()
		{
			if(m_typeComplete)
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
        /// Coroutine to produce typewriter effect
        /// </summary>
        private IEnumerator TypeText(string _textToType)
        {
			m_typeComplete = false;
			m_currText = _textToType;
			text.text = "";

			for (int i = 0; i < _textToType.Length; ++i)
            {
                text.text += _textToType[i];
                yield return new WaitForSeconds(m_typeSpeed);
            }
			m_typeComplete = true;
		}
		#endregion
	}
}