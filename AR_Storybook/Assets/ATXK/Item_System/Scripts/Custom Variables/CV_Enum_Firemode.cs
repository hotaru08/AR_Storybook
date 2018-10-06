namespace ATXK.ItemSystem
{
	using UnityEngine;
	using CustomVariables;

	[CreateAssetMenu(menuName = "Item System/Enum/Fire Mode")]
	public class CV_Enum_Firemode : CV_Enum
	{
		[SerializeField] int numberOfProjectiles;
		[SerializeField] int fireRate;

		public int ProjectileCount { get { return numberOfProjectiles; } }
	}
}