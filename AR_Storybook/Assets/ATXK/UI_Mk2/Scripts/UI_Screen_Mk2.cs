namespace ATXK.UI.Mk2
{
	using UnityEngine;
	using UnityEngine.UI;

	[RequireComponent(typeof(CanvasGroup))]
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
		[Tooltip("Background Image of the screen.")]
		[SerializeField] Image screenImage;
		[Tooltip("Parent object for all UI elements on this screen. All buttons/text/etc should be parented under this gameobject.")]
		[SerializeField] GameObject screenElements;

		[Header("Screen Settings")]
		[Tooltip("Screen Type. Affects the transition.")]
		[SerializeField] Screen screenType;
		[Tooltip("Transition Style that the screen will use. Only applies to fullscreens.")]
		[SerializeField] Transition transitionStyle;
		[Tooltip("Greyscale texture that the shader will use. Only applies to screens that are using TRANSITION_ANIMATED.")]
		[SerializeField] Texture transitionTexture;
		[Tooltip("Flag if this screen will run on start. Should only be checked for the first screen.")]
		[SerializeField] bool startOnAwake;

		#region Property Getters
		public Image ScreenImage { get { return screenImage; } }
		public GameObject ScreenElements { get { return screenElements; } }
		public Screen ScreenType { get { return screenType; } }
		public Transition ScreenTransition { get { return transitionStyle; } }
		public Texture ScreenTransitionTexture { get { return transitionTexture; } }
		public bool StartOnAwake { get { return startOnAwake; } }
		#endregion
	}
}