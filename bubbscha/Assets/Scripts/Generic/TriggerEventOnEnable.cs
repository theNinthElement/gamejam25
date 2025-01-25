using UnityEngine;
using UnityEngine.Events;

namespace Generic
{
    public class TriggerEventOnEnable : MonoBehaviour
    {
        [SerializeField] private UnityEvent _event;

        private void OnEnable()
        {
            _event.Invoke();
        }
    }
}