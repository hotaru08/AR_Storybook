namespace ATXK.UI.Mk2
{
	using UnityEngine;
	using EventSystem;

	public class UI_Screen_Countdown : MonoBehaviour
	{
		[Tooltip("Time in seconds before this screen pings for a screen change.")]
		[SerializeField] float screenLifetime;
		[Tooltip("Screen to change to after time is up.")]
		[SerializeField] UI_Screen_Mk2 screenToChangeTo;
		[Tooltip("Object event that the UI manager will be listening for.")]
		[SerializeField] ES_Event_Object changeScreenEvent;

		float runtimeLifetime;

		private void Start()
		{
			runtimeLifetime = screenLifetime;
		}

		private void Update()
		{
			runtimeLifetime -= Time.deltaTime;

			if (runtimeLifetime <= 0f)
			{
				runtimeLifetime = 0f;

				changeScreenEvent.value = screenToChangeTo;
				changeScreenEvent.Invoke();

				enabled = false;
			}
		}
	}
}
