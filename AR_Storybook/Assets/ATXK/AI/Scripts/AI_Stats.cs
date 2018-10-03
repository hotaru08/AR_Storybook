namespace ATXK.AI
{
	using UnityEngine;
	using CustomVariables;

	[CreateAssetMenu(menuName = "AI/Statistics")]
	public class AI_Stats : ScriptableObject
	{
		[Header("Health")]
		public int health;
		public CV_Int startHealth;

		[Header("Attack")]
		public CV_Int damage;

		[Header("Scanning")]
		public float scanTime;
		public float scanSpeed;

		private void OnEnable()
		{
			health = startHealth.value;
		}
	}
}