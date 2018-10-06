namespace ATXK.UI.Mk2
{
	using UnityEngine;
	using UnityEngine.UI;

	public class UI_Screen_Mk2 : MonoBehaviour
	{
		public enum Screen
		{
			SCREEN_FULL,
			SCREEN_OVERLAY
		}

		public enum Transition
		{
			TRANSITION_FADE,
			TRANSITION_ANIMATED,
			TRANSITION_DISTORT
		}

		[Header("Screen Objects")]
		[SerializeField] GameObject screenElements;
		Image screenImage;

		[Header("Screen Settings")]
		[SerializeField] Screen screenType;
		[SerializeField] Sprite screenTexture;
		[SerializeField] Transition transitionStyle;
		[SerializeField] Texture transitionTexture;

		#region Property Getters
		public Image ScreenImage { get { return screenImage; } }
		public GameObject ScreenElements { get { return screenElements; } }
		public Screen ScreenType { get { return screenType; } }
		public Transition ScreenTransition { get { return transitionStyle; } }
		public Texture ScreenTransitionTexture { get { return transitionTexture; } }
		#endregion

		private void OnEnable()
		{
			screenImage = GetComponent<Image>();
			screenImage.sprite = screenTexture;
		}
	}
}