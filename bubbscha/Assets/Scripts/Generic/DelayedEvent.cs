using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    public class DelayedEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _event;
        [SerializeField] private float _delay;

        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        /// Invokes the UnityEvent after a specified delay.
        /// </summary>
        public async void InvokeDelayed()
        {
            Cancel();
            
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

            try
            {
                await Task.Delay((int)(_delay * 1000), token);
                
                if (!token.IsCancellationRequested)
                {
                    _event?.Invoke();
                }
            }
            catch (TaskCanceledException)
            {
            }
        }

        /// <summary>
        /// Cancels any ongoing delayed invocation.
        /// </summary>
        private void Cancel()
        {
            if (_cancellationTokenSource == null) return;
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }
    }
}