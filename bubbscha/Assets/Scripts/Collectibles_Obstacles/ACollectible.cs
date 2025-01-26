using UnityEngine;
using UnityEngine.Events;

namespace Collectibles_Obstacles
{
    public abstract class ACollectible : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onCollect;
        [SerializeField] private UnityEvent _onRespawn;
        private void OnTriggerEnter(Collider other)
        {
            _onCollect.Invoke();
            Collect();
        }

        protected abstract void Collect();

        public void Respawn()
        {
            _onRespawn.Invoke();
        }
    }
}