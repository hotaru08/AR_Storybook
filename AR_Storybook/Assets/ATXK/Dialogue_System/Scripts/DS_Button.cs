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

		#region Unity Methods
		private void Start()
		{
			dialogueTree = DS_Manager.Instance.DialogueTree;
			UpdateText();

			//GetComponent<Button>().onClick.AddListener(delegate { DS_Manager.Instance.TraverseTree(index); });
			GetComponent<Button>().onClick.AddListener(CheckNode);
		}
		#endregion

		#region Class Methods
		private void CheckNode()
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

		public void UpdateText()
		{
            //Update the button's text
            //text.text = dialogueTree.CurrentNode.Sentence;

            // Reset text to be empty
            text.text = string.Empty;

			if (dialogueTree != null)
			{
                if (dialogueTree.CurrentNode.IsQuestion && dialogueTree.CurrentNode == dialogueTree.PreviousNode)
				{
					if (index < dialogueTree.CurrentNode.NextNodes.Length)
                    {
                        //text.text = dialogueTree.CurrentNode.NextNodes[index].Sentence;
                        StartCoroutine(TypeText(dialogueTree.CurrentNode.NextNodes[index].Sentence));
                    }
				}
				else
				{
                    //text.text = dialogueTree.CurrentNode.Sentence;
                    StartCoroutine(TypeText(dialogueTree.CurrentNode.Sentence));
				}
			}
		}

        /// <summary>
        /// Coroutine to produce typewriter effect
        /// </summary>
        private IEnumerator TypeText(string _textToType)
        {
            for (int i = 0; i < _textToType.Length; ++i)
            {
                text.text += _textToType[i];
                yield return new WaitForSeconds(m_typeSpeed);
            }
        }
		#endregion
	}
}