namespace ATXK.Audio
{
	using UnityEngine;

	[RequireComponent(typeof(AudioSource))]
	public class AS_AssetHolder : MonoBehaviour
	{
		public AS_Asset audioAsset;
		AudioSource audioSource;

		private void Start()
		{
			audioSource = GetComponent<AudioSource>();
		}

		public void Play()
		{
			audioAsset.Play(audioSource);
		}

		public void PlayOneShot()
		{
			audioAsset.PlayOneShot(audioSource);
		}

		public void Pause()
		{
			audioAsset.Pause();
		}

		public void Resume()
		{
			audioAsset.Resume();
		}

		public void Stop()
		{
			audioAsset.Stop();
		}
	}
}