namespace ATXK.EventSystem
{
	using UnityEngine;

	public class ES_AwakeInvoker : MonoBehaviour
	{
		[SerializeField] ES_Event autoEvent;

		private void Awake()
		{
			autoEvent.Invoke();
		}
	}
}