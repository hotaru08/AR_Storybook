namespace ATXK.ItemSystem
{
	using UnityEngine;

	/// <summary>
	/// Items that can receive Update calls.
	/// </summary>
	public interface IUpdateable
	{
		/// <summary>
		/// Called every frame.
		/// </summary>
		/// <param name="itemObject">Item gameObject that will be updated.</param>
		void UpdateItem(GameObject itemObject);
	}
}
