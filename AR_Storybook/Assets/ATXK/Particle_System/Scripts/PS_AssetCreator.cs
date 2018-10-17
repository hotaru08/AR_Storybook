namespace ATXK.Particles
{
	using UnityEngine;

	public class PS_AssetCreator : MonoBehaviour
	{
		[SerializeField] ParticleSystem particleSystemToSave;

		#region Property Getters
		public ParticleSystem ParticleSystemToSave { get { return particleSystemToSave; } }
		#endregion

		public void SaveParticleSystem(ParticleSystem systemToSave)
		{

		}
	}
}