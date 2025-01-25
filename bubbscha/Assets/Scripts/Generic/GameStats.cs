using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStats : MonoBehaviour
{
    [Tooltip("Number for passenger mood")]
    [SerializeField] float mood;
    [Tooltip("Number for collected points")]
    [SerializeField] int score;
    public UnityEvent moodChanged;
    public UnityEvent scoreChanged;
    public void IncreaseMood(float moodBonus)
    {
        mood += moodBonus;
        moodChanged.Invoke();
    }
    public void DecreaseMood(float moodPenalty)
    {
        mood -= moodPenalty;
        moodChanged.Invoke();
    }
    public void IncreaseScore(int scoreBonus)
    {
        score += scoreBonus;
        scoreChanged.Invoke();
    }
    public void DecreaseScore(int scorePenalty)
    {
        score -= scorePenalty;
        scoreChanged.Invoke();
    }
}
