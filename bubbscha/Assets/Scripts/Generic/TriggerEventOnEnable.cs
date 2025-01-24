using System;
using UnityEngine;
using UnityEngine.Events;

namespace Common
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