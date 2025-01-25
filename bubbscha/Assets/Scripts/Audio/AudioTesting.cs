using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Audio
{
    public class AudioTesting : MonoBehaviour
    {
        [SerializeField] private AudioPlayer _audioPlayer;
        [SerializeField] private bool _start;
        [SerializeField] private bool _stop;
        [SerializeField, Range(0,1)] private float _happinessValue;
        private void Update()
        {
            if (_start)
            {
                _audioPlayer.PlayLoopingAudio();
                _start = false;
            }

            if (_stop)
            {
                _audioPlayer.StopLoopingAudio();
                _stop = false;
            }
            
            _audioPlayer.UpdateAudio(_happinessValue);
                
        }
    }
}