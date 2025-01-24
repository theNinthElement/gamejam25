using UnityEditor;
using UnityEngine;

namespace Audio.Editor
{
    [CustomEditor(typeof(AudioPlayer))]
    public class AudioPlayerEditor : UnityEditor.Editor
    {
        private float _happinessValue; // Local slider value for the editor

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            // Get a reference to the AudioPlayer instance
            var audioPlayer = (AudioPlayer)target;

            // Check if the editor is in play mode
            if (Application.isPlaying)
            {
                // Display a message indicating runtime mode
                EditorGUILayout.HelpBox("Audio controls are available during runtime.", MessageType.Info);

                // Start button
                if (GUILayout.Button("Start"))
                {
                    audioPlayer.PlayLoopingAudio();
                }

                // Stop button
                if (GUILayout.Button("Stop"))
                {
                    audioPlayer.StopLoopingAudio();
                }

                // Happiness Value slider
                EditorGUILayout.LabelField("Happiness Value", EditorStyles.boldLabel);
                var newHappinessValue = EditorGUILayout.Slider(_happinessValue, 0f, 1f);

                // Update audio if the slider value changes
                if (Mathf.Approximately(newHappinessValue, _happinessValue)) return;
                Undo.RecordObject(this, "Happiness Value Changed"); // Enable Undo functionality
                _happinessValue = newHappinessValue; // Update the local value
                audioPlayer.UpdateAudio(_happinessValue); // Call the method with the new value
                EditorUtility.SetDirty(target); // Ensure changes are reflected
            }
            else
            {
                // Display a message indicating the controls are disabled outside runtime
                EditorGUILayout.HelpBox("Audio controls are only available during runtime.", MessageType.Warning);
            }
        }
    }
}