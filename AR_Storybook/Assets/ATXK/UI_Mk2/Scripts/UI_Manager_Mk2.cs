namespace ATXK.UI.Mk2
{
	using System.Collections;
	using UnityEngine;
	using UnityEngine.UI;

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

		private void Start()
		{
			if(PlayerPrefs.GetInt("FirstLaunch") == 0)
			{
				currScreen = splashScreen;
				PlayerPrefs.SetInt("FirstLaunch", 1);
			}
			else
				currScreen = startScreen;

			prevScreen = currScreen;

			currScreen.gameObject.SetActive(true);
			if (currScreen.StartOnAwake)
				StartCoroutine("TransitionIn", currScreen);
		}

		private void OnEnable()
		{
			transitionMaterial.SetFloat("_Cutoff", 0f);
			transitionMaterial.SetFloat("_Fade", 1f);
		}

		private void OnDisable()
		{
			transitionMaterial.SetFloat("_Cutoff", 0f);
			transitionMaterial.SetFloat("_Fade", 1f);
		}

		private void OnApplicationQuit()
		{
			PlayerPrefs.SetInt("FirstLaunch", 0);
		}

		public void ChangeScreen(Object screenObject)
		{
			UI_Screen_Mk2 screen = screenObject as UI_Screen_Mk2;
			if (screen != null)
				ChangeScreen(screen);
		}

		public void ChangeScreen(UI_Screen_Mk2 screen)
		{
			prevScreen = currScreen;
			currScreen = screen;

			// Current screen is an overlay
			if(currScreen.ScreenType == UI_Screen_Mk2.Screen.SCREEN_OVERLAY)
			{
				currScreen.ScreenElements.SetActive(true);
				currScreen.gameObject.SetActive(true);

				//prevScreen.ScreenElements.SetActive(false);

				// Only 1 overlay allowed at a time
				if(prevScreen.ScreenType == UI_Screen_Mk2.Screen.SCREEN_OVERLAY)
				{
					prevScreen.gameObject.SetActive(false);
				}
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
					prevScreen.ScreenElements.SetActive(false);
					prevScreen.gameObject.SetActive(false);

					currScreen.ScreenElements.SetActive(true);
					currScreen.gameObject.SetActive(true);
				}
			}
		}

		private IEnumerator TransitionIn(UI_Screen_Mk2 screen)
		{
			if (screen == null)
				yield break;

			screen.ScreenElements.SetActive(false);
			screen.gameObject.SetActive(true);

			screen.ScreenImage.material.SetTexture("_MaskTex", screen.ScreenTransitionTexture);
			screen.ScreenImage.material.SetInt("_Distort", System.Convert.ToInt32(screen.ScreenTransition.Equals(UI_Screen_Mk2.Transition.TRANSITION_DISTORT)));

			screen.ScreenImage.material.SetFloat("_Cutoff", 1f);
			screen.ScreenImage.material.SetFloat("_Fade", 1f);

			float i = 1f;
			while (i >= 0f - Time.deltaTime)
			{
				if (screen.ScreenTransition == UI_Screen_Mk2.Transition.TRANSITION_FADE)
					screen.ScreenImage.material.SetFloat("_Fade", i);
				else
					screen.ScreenImage.material.SetFloat("_Cutoff", i);

				i -= Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}

			screen.ScreenImage.material.SetFloat("_Cutoff", 0f);
			screen.ScreenImage.material.SetFloat("_Fade", 1f);

			screen.ScreenElements.SetActive(true);
		}

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
			while(i <= 1f + Time.deltaTime)
			{
				if(screen.ScreenTransition == UI_Screen_Mk2.Transition.TRANSITION_FADE)
					screen.ScreenImage.material.SetFloat("_Fade", i);
				else
					screen.ScreenImage.material.SetFloat("_Cutoff", i);

				i += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}

			screen.ScreenImage.material.SetFloat("_Cutoff", 1f);
			screen.ScreenImage.material.SetFloat("_Fade", 1f);

			screen.gameObject.SetActive(false);

			StartCoroutine("TransitionIn", currScreen);
		}
	}
}