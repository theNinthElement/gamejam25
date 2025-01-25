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
    [SerializeField] float score;
    UnityEvent moodChanged;
    UnityEvent scoreChanged;

    public void increaseMood(float moodBonus)
    {
        mood += moodBonus;
        moodChanged.Invoke();
    }
    public void decreaseMood(float moodPenalty)
    {
        mood -= moodPenalty;
        moodChanged.Invoke();
    }
    public void increaseScore(float scoreBonus)
    {
        score += scoreBonus;
        scoreChanged.Invoke();
    }
    public void decreaseScore(float scorePenalty)
    {
        score -= scorePenalty;
        scoreChanged.Invoke();
    }
}
