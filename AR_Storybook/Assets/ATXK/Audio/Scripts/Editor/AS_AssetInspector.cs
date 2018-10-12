namespace ATXK.Audio
{
	using UnityEditor;
	using UnityEngine;

	[CustomEditor(typeof(AS_Asset), true)]
	public class AS_AssetInspector : Editor
	{
		[SerializeField] AudioSource previewSource;

		float minVolumeLimit = 0f;
		float maxVolumeLimit = 1f;

		float minPitchLimit = 0.1f;
		float maxPitchLimit = 3f;

		private void OnEnable()
		{
			previewSource = EditorUtility.CreateGameObjectWithHideFlags("Audio Preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
		}

		private void OnDisable()
		{
			DestroyImmediate(previewSource);
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			EditorGUILayout.Space();

			EditorGUILayout.MinMaxSlider("Volume", ref ((AS_Asset)target).minVolume, ref ((AS_Asset)target).maxVolume, minVolumeLimit, maxVolumeLimit);
			EditorGUILayout.MinMaxSlider("Pitch", ref ((AS_Asset)target).minPitch, ref ((AS_Asset)target).maxPitch, minPitchLimit, maxPitchLimit);

			if(GUILayout.Button("Preview Audio"))
			{
				((AS_Asset)target).Play(previewSource);
			}
		}
	}
}