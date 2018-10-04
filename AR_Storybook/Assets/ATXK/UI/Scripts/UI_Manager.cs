namespace ATXK.UI
{
	using System.Collections;
	using UnityEngine;
	using UnityEngine.UI;
	using ATXK.Helper;
	using ATXK.EventSystem;

	[RequireComponent(typeof(ES_EventListener))]
	/// <summary>
	/// Manager for all User Interface elements.
	/// </summary>
	public class UI_Manager : SingletonBehaviour<UI_Manager>
	{
		[SerializeField] UI_Screen startScreen;

		UI_Screen currentScreen;
		UI_Screen previousScreen;

		/// <summary>
		/// Unity Awake function.
		/// </summary>
		private void Awake()
		{
			//Set starting screen
			if (startScreen != null)
				currentScreen = startScreen;
		}

		/// <summary>
		/// Sets the given screen as the current screen.
		/// </summary>
		/// <param name="screen">Screen to set as current.</param>
		public void ToggleScreens(UI_Screen screen)
		{
			if(currentScreen != null)
			{
				//Save the current screen is the screen is valid
				previousScreen = currentScreen;
				//Set the new current screen
				currentScreen = screen;
				//Only deactivate the screen if the new screen is a fullscreen one
				if (!screen.IsModal)
				{
					//Deactivate the screen
					StartCoroutine(TransitionOut(previousScreen));
				}
			}
		}

		/// <summary>
		/// Coroutine to run shader animation.
		/// </summary>
		/// <param name="useCutoff"></param>
		/// <param name="screen"></param>
		/// <returns></returns>
		IEnumerator TransitionOut(UI_Screen screen = null)
		{
			//Do not continue if screen is not valid
			if (screen == null)
				yield break;

			//Set the appropriate starting values
			screen.BackgroundImage.material.SetFloat("_Cutoff", 1f);
			screen.BackgroundImage.material.SetFloat("_Fade", 1f);
			screen.BackgroundImage.material.SetInt("_Distort", System.Convert.ToInt32(screen.TransitionType.Equals(UI_Screen.ShaderEffect.SHADER_DISTORT)));
			screen.BackgroundImage.material.SetTexture("_TransitionTex", screen.TransitionTexture);

			//Deactivate screen elements so they dont show up on the animation
			screen.ScreenElements.SetActive(false);

			//Loop to adjust shader cutoff values
			float loopValue = 0f;
			while(loopValue <= 1f + Time.deltaTime)
			{
				if (screen.TransitionType.Equals(UI_Screen.ShaderEffect.SHADER_FADER))
				{
					screen.BackgroundImage.material.SetFloat("_Fade", loopValue);
				}
				else
				{
					screen.BackgroundImage.material.SetFloat("_Cutoff", loopValue);
				}


				loopValue += Time.deltaTime;

				yield return new WaitForEndOfFrame();
			}
			//Set shader values to default max
			screen.BackgroundImage.material.SetFloat("_Cutoff", 1f);

			//Deactivate screen
			screen.gameObject.SetActive(false);

			//Start fadein animation
			StartCoroutine(TransitionIn(currentScreen));
		}

		IEnumerator TransitionIn(UI_Screen screen = null)
		{
			//Do not continue if screen is not valid
			if (screen == null)
				yield break;

			//Activate current screen if not animation will not show
			screen.gameObject.SetActive(true);

			//Set the appropriate starting values
			screen.BackgroundImage.material.SetFloat("_Cutoff", 1f);
			screen.BackgroundImage.material.SetFloat("_Fade", 1f);
			screen.BackgroundImage.material.SetInt("_Distort", System.Convert.ToInt32(System.Convert.ToInt32(screen.TransitionType.Equals(UI_Screen.ShaderEffect.SHADER_DISTORT))));
			screen.BackgroundImage.material.SetTexture("_TransitionTex", screen.TransitionTexture);

			//Loop to adjust shader cutoff values
			float loopValue = 1f;
			while (loopValue >= 0f - Time.deltaTime)
			{
				if (screen.TransitionType.Equals(UI_Screen.ShaderEffect.SHADER_FADER))
				{
					screen.BackgroundImage.material.SetFloat("_Fade", loopValue);
				}
				else
				{
					screen.BackgroundImage.material.SetFloat("_Cutoff", loopValue);
				}
				loopValue -= Time.deltaTime;

				yield return new WaitForEndOfFrame();
			}
			//Set shader values to default min
			screen.BackgroundImage.material.SetFloat("_Cutoff", 0f);

			//Activate screen elements so they show up at the end
			screen.ScreenElements.SetActive(true);
		}
	}
}
