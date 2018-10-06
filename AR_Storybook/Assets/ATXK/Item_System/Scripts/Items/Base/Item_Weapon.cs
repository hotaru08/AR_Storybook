namespace ATXK.ItemSystem
{
	using UnityEngine;
	using System.Collections.Generic;

	public class Item_Weapon : Item_Base //IPickable, IEquipable
	{
		[SerializeField] int weaponDamage;
		[SerializeField] int firemodeIndex;
		[SerializeField] CV_Enum_Firemode currentFireMode;
		[SerializeField] List<CV_Enum_Firemode> weaponFireModes;

		#region Property Getters
		public int Damage { get { return weaponDamage; } }
		public CV_Enum_Firemode CurrentFireMode { get { return currentFireMode; } }
		#endregion

		private void Awake()
		{
			firemodeIndex = 0;
			currentFireMode = weaponFireModes[firemodeIndex];
		}

		public override bool OnCollide(GameObject collidingObject)
		{
			return false;
		}

		public void ToggleFireMode()
		{
			firemodeIndex++;
			if (firemodeIndex > weaponFireModes.Count - 1)
				firemodeIndex = 0;

			currentFireMode = weaponFireModes[firemodeIndex];
		}
	}
}