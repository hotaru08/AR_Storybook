namespace ATXK.AI
{
	using UnityEngine;

	/// <summary>
	/// Base class for all AI decisions.
	/// </summary>
	public abstract class AI_Decision : ScriptableObject
	{
		public abstract bool Decide(AI_Controller controller);
	}
}