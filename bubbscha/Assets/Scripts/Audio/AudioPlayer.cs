using System;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        /// <summary>
        /// multiple collections to allow for more environments with different music
        /// </summary>
        [SerializeField] private List<AudioCollection> _audioCollections;

        private AudioCollection _playingCollection;

        public void PlayLoopingAudio()
        {
            var index = 0;
            //index = EnvironmentController.GetAudioIndex();
            var collection = _audioCollections[index];
            _playingCollection?.Stop();
            collection.Play();


            _playingCollection = collection;
        }

        public void UpdateAudio(float input)
        {
            var mappedInput = (input * 2f) - 1f;

            var negativeVolume = Math.Max(0f, -mappedInput);
            var positiveVolume = Math.Max(0f, mappedInput);

            _playingCollection?.SetVolume(positiveVolume, negativeVolume);
        }

        [Serializable]
        private class AudioCollection
        {
            [SerializeField] public AudioSource defaultAudio;
            [SerializeField] public AudioSource positiveAudio;
            [SerializeField] public AudioSource negativeAudio;

            public void SetVolume(float positiveVolume, float negativeVolume)
            {
                positiveAudio.volume = positiveVolume;
                negativeAudio.volume = negativeVolume;
            }
            
            public void Stop()
            {
                defaultAudio.Stop();
                positiveAudio.Stop();
                negativeAudio.Stop();
            }

            public void Play()
            {
                defaultAudio.Play();
                positiveAudio.Play();
                negativeAudio.Play();
            }
        }
    }
}