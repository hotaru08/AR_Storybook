namespace ATXK.UI
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(CanvasGroup))]
	public class UI_Screen : MonoBehaviour
	{
		public enum ShaderEffect
		{
			SHADER_GREYSCALE,
			SHADER_DISTORT,
			SHADER_FADER
		}

		/// <summary>
		/// Is the window modal (ie popup) or fullscreen.
		/// </summary>
		[SerializeField] bool isModal = false;
		/// <summary>
		/// Background Image for the screen object.
		/// </summary>
		[SerializeField] Image backgroundImage;
		/// <summary>
		/// Parent gameobject for all screen elements.
		/// </summary>
		[SerializeField] GameObject screenElements;
		/// <summary>
		/// Greyscale texture for the screen transition shader to use.
		/// </summary>
		[SerializeField] Texture transitionTexture;
		/// <summary>
		/// Type of shader effect used in transition animation.
		/// </summary>
		[SerializeField] ShaderEffect transitionType;

		/// <summary>
		/// Is the window modal (ie popup) or fullscreen.
		/// </summary>
		public bool IsModal { get { return isModal; } }
		/// <summary>
		/// Background Image for the screen object.
		/// </summary>
		public Image BackgroundImage { get { return backgroundImage; } }
		/// <summary>
		/// Parent gameobject for all screen elements.
		/// </summary>
		public GameObject ScreenElements { get { return screenElements; } }
		/// <summary>
		/// Greyscale texture for the screen transition shader to use.
		/// </summary>
		public Texture TransitionTexture { get { return transitionTexture; } }
		/// <summary>
		/// Type of shader effect used in transition animation.
		/// </summary>
		public ShaderEffect TransitionType { get { return transitionType; } }
	}
}