namespace ATXK.UI.Mk2
{
	using System.Collections;
	using UnityEngine;
	using UnityEngine.UI;

	/// <summary>
	/// Manager that manages all UI Screens on a canvas.
	/// </summary>
	public class UI_Manager_Mk2 : MonoBehaviour
	{
		[Header("Splash Screen")]
		[Tooltip("Splash Screen.")]
		[SerializeField] UI_Screen_Mk2 splashScreen;

		[Header("Starting Screen")]
		[Tooltip("Starting Screen.")]
		[SerializeField] UI_Screen_Mk2 startScreen;

		[Header("Runtime Screens")]
		[SerializeField] UI_Screen_Mk2 currScreen;
		[SerializeField] UI_Screen_Mk2 prevScreen;

		[Header("Global Material for all screens.")]
		[Tooltip("Global Material for all screens.")]
		[SerializeField] Material transitionMaterial;

		[Header("Animation Settings")]
		[Tooltip("Speed of which the scale animation will play for Overlay screens.")]
		[Range(0.1f, 2f)] [SerializeField] float popSpeed = 1f;
		[Tooltip("Speed of which the shader animation will play for Full screens.")]
		[Range(0.1f, 2f)] [SerializeField] float transitSpeed = 1f;

		/// <summary>
		/// Called when this object first becomes enabled.
		/// </summary>
		private void Start()
		{
			if(PlayerPrefs.GetInt("FirstLaunch") == 0 && splashScreen != null)
			{
				currScreen = splashScreen;
				PlayerPrefs.SetInt("FirstLaunch", 1);
			}
			else
				currScreen = startScreen;

			prevScreen = currScreen;

			if (currScreen.StartOnAwake)
				StartCoroutine("TransitionIn", currScreen);
		}

		/// <summary>
		/// Called when this object becomes enabled.
		/// </summary>
		private void OnEnable()
		{
			transitionMaterial.SetFloat("_Cutoff", 0f);
			transitionMaterial.SetFloat("_Fade", 1f);
		}

		/// <summary>
		/// Called when this object becomes disabled.
		/// </summary>
		private void OnDisable()
		{
			transitionMaterial.SetFloat("_Cutoff", 0f);
			transitionMaterial.SetFloat("_Fade", 1f);
		}

		/// <summary>
		/// Called before the application ends.
		/// </summary>
		private void OnApplicationQuit()
		{
			PlayerPrefs.SetInt("FirstLaunch", 0);
		}

		/// <summary>
		/// Changes the current screen to the one provided, if the Object provided is of type UI_Screen_Mk2.
		/// </summary>
		/// <param name="screenObject">Screen to change to.</param>
		public void ChangeScreen(Object screenObject)
		{
			UI_Screen_Mk2 screen = screenObject as UI_Screen_Mk2;
			if (screen != null)
				ChangeScreen(screen);
		}

		/// <summary>
		/// Changes the current screen to the provided.
		/// </summary>
		/// <param name="screen">Screen to change to.</param>
		public void ChangeScreen(UI_Screen_Mk2 screen)
		{
			prevScreen = currScreen;
			currScreen = screen;

			// Scale animate if previous screen was an overlay
			if(prevScreen.ScreenType == UI_Screen_Mk2.Screen.SCREEN_OVERLAY)
			{
				StartCoroutine("PopOut", prevScreen);
			}

			// Current screen is an overlay
			if(currScreen.ScreenType == UI_Screen_Mk2.Screen.SCREEN_OVERLAY)
			{
				StartCoroutine("PopIn", currScreen);
			}
			// Current screen is fullscreen
			else 
			{
				// Previous screen was not an overlay
				if (prevScreen.ScreenType != UI_Screen_Mk2.Screen.SCREEN_OVERLAY)
				{
					// Play shader animation
					StartCoroutine("TransitionOut", prevScreen);
				}
				// Previous screen was an overlay
				else
				{
					currScreen.ScreenElements.SetActive(true);
					currScreen.gameObject.SetActive(true);
				}
			}
		}

		/// <summary>
		/// Coroutine to scale-animate the provided Overlay screen.
		/// </summary>
		/// <param name="screen">Screen to animate.</param>
		private IEnumerator PopIn(UI_Screen_Mk2 screen)
		{
			if (screen == null)
				yield break;

			screen.gameObject.SetActive(true);
			screen.ScreenElements.SetActive(true);

			screen.ScreenImage.rectTransform.localScale = Vector3.zero;

			float index = 0f;
			while(index < 1f)
			{
				screen.ScreenImage.rectTransform.localScale += Vector3.Lerp(Vector3.zero, Vector3.one, Time.unscaledDeltaTime * popSpeed);
				index += Time.unscaledDeltaTime * popSpeed;

                yield return new WaitForEndOfFrame();
            }

			screen.ScreenImage.rectTransform.localScale = Vector3.one;
		}

		/// <summary>
		/// Coroutine to scale-animate the provided Overlay screen.
		/// </summary>
		/// <param name="screen">Screen to animate.</param>
		private IEnumerator PopOut(UI_Screen_Mk2 screen)
		{
			if (screen == null)
				yield break;

			screen.ScreenImage.rectTransform.localScale = Vector3.one;

			float index = 1f;
			while (index > 0f)
			{
				screen.ScreenImage.rectTransform.localScale -= Vector3.Lerp(Vector3.zero, Vector3.one, Time.unscaledDeltaTime * popSpeed);
				index -= Time.unscaledDeltaTime * popSpeed;

				yield return new WaitForEndOfFrame();
			}

			screen.ScreenImage.rectTransform.localScale = Vector3.zero;

			screen.gameObject.SetActive(false);
			screen.ScreenElements.SetActive(false);
		}

		/// <summary>
		/// Coroutine to shader-animate the provided Full screen.
		/// </summary>
		/// <param name="screen">Screen to animate.</param>
		private IEnumerator TransitionIn(UI_Screen_Mk2 screen)
		{
			if (screen == null)
			{
				yield break;
			}
			if(screen.ScreenImage.sprite == null)
			{
				screen.ScreenImage.material.SetFloat("_Cutoff", 0f);
				screen.ScreenImage.material.SetFloat("_Fade", 1f);

				screen.gameObject.SetActive(true);
				screen.ScreenElements.SetActive(false);

				if (screen.IsAnimated)
					yield return new WaitForSeconds(screen.gameObject.GetComponent<DelayedAnim>().DelayTime);

				screen.ScreenElements.SetActive(true);

				yield break;
			}

			screen.ScreenElements.SetActive(false);
			screen.gameObject.SetActive(true);

			screen.ScreenImage.material.SetTexture("_MaskTex", screen.ScreenTransitionTexture);
			screen.ScreenImage.material.SetInt("_Distort", System.Convert.ToInt32(screen.ScreenTransition.Equals(UI_Screen_Mk2.Transition.TRANSITION_DISTORT)));

			screen.ScreenImage.material.SetFloat("_Cutoff", 1f);
			screen.ScreenImage.material.SetFloat("_Fade", 1f);

			float i = 1f;
			while (i >= 0f - Time.unscaledDeltaTime)
			{
				if (screen.ScreenTransition == UI_Screen_Mk2.Transition.TRANSITION_FADE)
					screen.ScreenImage.material.SetFloat("_Fade", i);
				else
					screen.ScreenImage.material.SetFloat("_Cutoff", i);

				i -= Time.unscaledDeltaTime * transitSpeed;
				yield return new WaitForEndOfFrame();
			}

			screen.ScreenImage.material.SetFloat("_Cutoff", 0f);
			screen.ScreenImage.material.SetFloat("_Fade", 1f);

			screen.ScreenElements.SetActive(true);
		}

		/// <summary>
		/// Coroutine to shader-animate the provided Full screen.
		/// </summary>
		/// <param name="screen">Screen to animate.</param>
		private IEnumerator TransitionOut(UI_Screen_Mk2 screen)
		{
			if (screen == null)
				yield break;

			screen.ScreenElements.SetActive(false);
			screen.gameObject.SetActive(true);

			screen.ScreenImage.material.SetTexture("_MaskTex", screen.ScreenTransitionTexture);
			screen.ScreenImage.material.SetInt("_Distort", System.Convert.ToInt32(screen.ScreenTransition.Equals(UI_Screen_Mk2.Transition.TRANSITION_DISTORT)));

			screen.ScreenImage.material.SetFloat("_Cutoff", 1f);
			screen.ScreenImage.material.SetFloat("_Fade", 1f);

			float i = 0f;
			while(i <= 1f + Time.unscaledDeltaTime)
			{
				if(screen.ScreenTransition == UI_Screen_Mk2.Transition.TRANSITION_FADE)
					screen.ScreenImage.material.SetFloat("_Fade", i);
				else
					screen.ScreenImage.material.SetFloat("_Cutoff", i);

				i += Time.unscaledDeltaTime * transitSpeed;
				yield return new WaitForEndOfFrame();
			}

			screen.ScreenImage.material.SetFloat("_Cutoff", 1f);
			screen.ScreenImage.material.SetFloat("_Fade", 1f);

			screen.gameObject.SetActive(false);

			StartCoroutine("TransitionIn", currScreen);
		}
	}
}