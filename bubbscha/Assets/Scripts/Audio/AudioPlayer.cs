using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        /// <summary>
        /// multiple collections to allow for more environments with different music
        /// </summary>
        [SerializeField] private List<AudioSource> _audio;

        public void PlayLoopingAudio()
        {
            foreach (var audioSource in _audio)
            {
                audioSource.Play();
            }
            UpdateAudio(.5f);
        }

        public void UpdateAudio(float input)
        {
            var controlledAudioSources = _audio.Count - 1; // -1 to remove the default audio
            var volume = controlledAudioSources * input + 1; //+1 so first audio source always plays at full volume
            for (var i = 0; i < _audio.Count; i++)
            {
                _audio[i].volume = Mathf.Clamp01(volume - i);
            }
        }
        
        public void PauseLoopingAudio()
        {
            foreach (var audioSource in _audio)
            {
                audioSource.Pause();
            }
        }
    }
}