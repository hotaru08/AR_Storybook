namespace ATXK.Particles
{
	using UnityEngine;
	using EventSystem;

	[RequireComponent(typeof(ParticleSystem))]
	[RequireComponent(typeof(ES_EventListener))]
	public class PS_Controller : MonoBehaviour
	{
		ParticleSystem particleSystem;
		ParticleSystem.MainModule mainModule;

		private void Awake()
		{
			particleSystem = GetComponent<ParticleSystem>();
			mainModule = particleSystem.main;
		}

		public void Play(bool looped)
		{
			mainModule.loop = looped;
			particleSystem.Play();
		}

		public void PlayOneShot()
		{
			mainModule.loop = false;
			particleSystem.Stop();
			particleSystem.Play();
		}
	}
}