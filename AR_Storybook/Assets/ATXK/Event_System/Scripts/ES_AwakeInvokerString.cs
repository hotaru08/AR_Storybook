namespace ATXK.EventSystem
{
    using UnityEngine;

    public class ES_AwakeInvokerString : MonoBehaviour
    {
        [SerializeField] ES_Event_String autoEvent;
        [SerializeField] string autoEventValue;

        private void Awake()
        {
            autoEvent.RaiseEvent(autoEventValue);
        }
    }
}