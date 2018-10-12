namespace ATXK.Cutscene
{
	using UnityEngine;
	using UnityEngine.Timeline;

	[CreateAssetMenu(menuName = "Cutscene/Cutscene Asset")]
	public class CS_CutsceneAsset : ScriptableObject
	{
		[SerializeField] TimelineAsset[] timelines;
		[SerializeField] int currentIndex;

		private void OnEnable()
		{
			currentIndex = 0;
		}

		public TimelineAsset CurrentTimeline { get { return timelines[currentIndex]; } }

		public TimelineAsset NextTimeline()
		{
			currentIndex++;
			currentIndex = Mathf.Clamp(currentIndex, 0, timelines.Length - 1);

			return CurrentTimeline;
		}
	}
}