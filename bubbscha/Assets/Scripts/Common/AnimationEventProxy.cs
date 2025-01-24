using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    public class AnimationEventProxy
    {
        [SerializeField]private string _key;
        [SerializeField]private UnityEvent _event;

        public void TriggerEvent(string key)
        {
            if (key == _key)
                _event.Invoke();
        }
    }
}