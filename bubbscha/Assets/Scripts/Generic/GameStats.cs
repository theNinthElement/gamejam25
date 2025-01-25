using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStats : MonoBehaviour
{
    public static GameStats instance;
    [Tooltip("Number for passenger mood")]
    [SerializeField][Range(0, 1)] float mood;
    [Tooltip("Number for collected points")]
    [SerializeField] int score;
    public UnityEvent<float> moodChanged;
    public UnityEvent scoreChanged;

    private void Awake()
    {
        instance = this;        
    }
    public void ChangeMood(float moodBonus)
    {
        mood += moodBonus;
        moodChanged.Invoke(mood);
    }
    public void ChangeScore(int scoreBonus)
    {
        score += scoreBonus;
        scoreChanged.Invoke();
    }
}
