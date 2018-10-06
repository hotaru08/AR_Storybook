namespace ATXK.UI.Mk2
{
	using System.Collections;
	using UnityEngine;

	public class UI_Manager_Mk2 : MonoBehaviour
	{
		[SerializeField] UI_Screen_Mk2 startScreen;
		[SerializeField] UI_Screen_Mk2 currScreen;
		[SerializeField] UI_Screen_Mk2 prevScreen;
		[SerializeField] Material transitionMaterial;

		private void Start()
		{
			currScreen = startScreen;
			prevScreen = currScreen;
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

		public void ChangeScreen(UI_Screen_Mk2 screen)
		{
			prevScreen = currScreen;
			currScreen = screen;

			// Current screen is an overlay
			if(currScreen.ScreenType == UI_Screen_Mk2.Screen.SCREEN_OVERLAY)
			{
				currScreen.ScreenElements.SetActive(true);
				currScreen.gameObject.SetActive(true);

				prevScreen.ScreenElements.SetActive(false);

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
					StartCoroutine("TransitionOut");
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

		private IEnumerator TransitionIn()
		{
			if (currScreen == null)
				yield break;

			currScreen.ScreenElements.SetActive(false);
			currScreen.gameObject.SetActive(true);

			currScreen.ScreenImage.material.SetTexture("_MaskTex", currScreen.ScreenTransitionTexture);
			currScreen.ScreenImage.material.SetInt("_Distort", System.Convert.ToInt32(currScreen.ScreenTransition.Equals(UI_Screen_Mk2.Transition.TRANSITION_DISTORT)));
			currScreen.ScreenImage.material.SetFloat("_Cutoff", 1f);
			currScreen.ScreenImage.material.SetFloat("_Fade", 1f);

			float i = 1f;
			while (i >= 0f - Time.deltaTime)
			{
				if (currScreen.ScreenTransition == UI_Screen_Mk2.Transition.TRANSITION_FADE)
					currScreen.ScreenImage.material.SetFloat("_Fade", i);
				else
					currScreen.ScreenImage.material.SetFloat("_Cutoff", i);

				i -= Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}

			currScreen.ScreenImage.material.SetFloat("_Cutoff", 0f);
			currScreen.ScreenImage.material.SetFloat("_Fade", 1f);

			currScreen.ScreenElements.SetActive(true);
		}

		private IEnumerator TransitionOut()
		{
			if (prevScreen == null)
				yield break;

			prevScreen.ScreenElements.SetActive(false);
			prevScreen.gameObject.SetActive(true);

			prevScreen.ScreenImage.material.SetTexture("_MaskTex", prevScreen.ScreenTransitionTexture);
			prevScreen.ScreenImage.material.SetInt("_Distort", System.Convert.ToInt32(prevScreen.ScreenTransition.Equals(UI_Screen_Mk2.Transition.TRANSITION_DISTORT)));
			prevScreen.ScreenImage.material.SetFloat("_Cutoff", 0f);
			prevScreen.ScreenImage.material.SetFloat("_Fade", 1f);

			float i = 0f;
			while(i <= 1f + Time.deltaTime)
			{
				if(prevScreen.ScreenTransition == UI_Screen_Mk2.Transition.TRANSITION_FADE)
					prevScreen.ScreenImage.material.SetFloat("_Fade", i);
				else
					prevScreen.ScreenImage.material.SetFloat("_Cutoff", i);

				i += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}

			prevScreen.ScreenImage.material.SetFloat("_Cutoff", 1f);
			prevScreen.ScreenImage.material.SetFloat("_Fade", 1f);

			prevScreen.gameObject.SetActive(false);

			StartCoroutine("TransitionIn");
		}
	}
}