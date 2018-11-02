namespace ATXK.EventSystem
{
	using UnityEngine;

	public class ES_AwakeInvoker : MonoBehaviour
	{
		[SerializeField] ES_Event_Abstract autoEvent;

		private void Awake()
		{
			autoEvent.RaiseEvent();
		}
	}
}