namespace ATXK.Audio
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "Audio/Audio Asset")]
	public class AS_Asset : ScriptableObject
	{
		public AudioClip[] AudioClips;
		public float minVolume = 1f;
		public float maxVolume = 1f;
		public float minPitch = 1f;
		public float maxPitch = 1f;
		public bool loop = false;

		AudioSource currentSource;

		public void SetSource(AudioSource source)
		{
			if (currentSource == source)
				return;

			currentSource = source;
			currentSource.clip = null;
			currentSource.volume = Random.Range(minVolume, maxVolume);
			currentSource.pitch = Random.Range(minPitch, maxPitch);
			currentSource.loop = loop;
		}

		public void Play(AudioSource source)
		{
			SetSource(source);

			if(!currentSource.isPlaying)
			{
				currentSource.clip = AudioClips[Random.Range(0, AudioClips.Length)];
				currentSource.Play();
			}
		}

		public void PlayOneShot(AudioSource source)
		{
			SetSource(source);

			currentSource.PlayOneShot(AudioClips[Random.Range(0, AudioClips.Length)]);
		}

		public void Stop()
		{
			currentSource.Stop();
		}

		public void Pause()
		{
			currentSource.Pause();
		}

		public void Resume()
		{
			currentSource.UnPause();
		}
	}
}