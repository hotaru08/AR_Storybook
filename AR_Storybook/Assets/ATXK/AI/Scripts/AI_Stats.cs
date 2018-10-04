namespace ATXK.AI
{
	using UnityEngine;
	using CustomVariables;

	[CreateAssetMenu(menuName = "AI/Statistics")]
	public class AI_Stats : ScriptableObject
	{
		[Header("Health")]
		public CV_Int startHealth;
		public int health;

		[Header("Attack")]
		public CV_Int damage;

		[Header("Scanning")]
		public float scanTime;
		public float scanSpeed;
		public float scanRange;

		private void OnEnable()
		{
			health = startHealth.value;
		}
	}
}