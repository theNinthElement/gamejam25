using UnityEngine;
using UnityEngine.Events;

namespace Collectibles_Obstacles
{
    public abstract class ACollectible : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onCollect;
        private void OnTriggerEnter(Collider other)
        {
            _onCollect.Invoke();
            Collect();
            Destroy(gameObject, 1);
        }

        protected abstract void Collect();
    }
}