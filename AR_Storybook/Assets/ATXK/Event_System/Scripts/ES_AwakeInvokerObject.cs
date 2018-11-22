namespace ATXK.EventSystem
{
    using UnityEngine;

    public class ES_AwakeInvokerObject : MonoBehaviour
    {
        [SerializeField] ES_Event_Object autoEvent;
        [SerializeField] Object autoEventValue;

        public void Start()
        {
            autoEvent.RaiseEvent(autoEventValue);
        }
    }
}