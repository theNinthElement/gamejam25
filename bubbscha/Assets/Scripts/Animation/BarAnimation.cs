using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Animation
{
    public class BarAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform _fill;
        [SerializeField] private float _fullHeight;
        [SerializeField] private float _animationSpeed;

        [SerializeField] private UnityEvent _positiveChange;
        [SerializeField] private UnityEvent _negativeChange;

        private Coroutine _activeCoroutine;
        private float _currentValue = 1;
        private float _currentTarget;

        //[Range(0, 1)] public float Testing;

        private void Update()
        {
            //UpdateBar(Testing);
        }

        private void OnValidate()
        {
            if (_fill == null || Application.isPlaying) return;
            _fullHeight = _fill.rect.height;
        }

        public void UpdateBar(float newValue)
        {
            if (Mathf.Approximately(_currentTarget, newValue)) return;
            _currentTarget = newValue;

            if (newValue > _currentTarget)
            {
                _positiveChange.Invoke();
            }

            if (newValue < _currentTarget)
            {
                _negativeChange.Invoke();
            }

            if (_activeCoroutine != null)
                StopCoroutine(_activeCoroutine);
            _activeCoroutine = StartCoroutine(AnimateBar(newValue));
        }

        private IEnumerator AnimateBar(float newValue)
        {
            while (!Mathf.Approximately(_currentValue, newValue))
            {
                var step = _animationSpeed * Time.deltaTime;

                if (_currentValue > newValue)
                {
                    step *= -1;
                }

                _currentValue += step;

                if (Mathf.Abs(_currentValue - newValue) < step)
                    _currentValue = newValue;

                var size = _currentValue * _fullHeight;
                _fill.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
                yield return null;
            }
        }
    }
}