using TMPro;
using UnityEngine;

namespace Animation
{
    public class HighScoreAnimation : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _multiplier;

        public void UpdateMultiplier(int multiplier)
        {
            _multiplier.text = multiplier.ToString() + "x";
        }

        public void UpdateScore(int score)
        {
            _score.text = score.ToString();
        }
    }
}