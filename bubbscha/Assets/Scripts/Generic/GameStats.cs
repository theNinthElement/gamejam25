using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class GameStats : MonoBehaviour
{
    public static GameStats instance;
    [Tooltip("Number for passenger mood")]
    [SerializeField][Range(0, 1)] float mood = 1f;
    [Tooltip("Number for collected points")]
    [SerializeField] int score = 0;
    [Tooltip("Increase of points per second")]
    [SerializeField] int scoreOverTime = 10;
    [Tooltip("Decrase of mood per second")]
    [SerializeField] float moodOverTime = -0.01f;
    
    public UnityEvent<float> moodChanged;
    public UnityEvent<int> scoreChanged;
    public UnityEvent<int> peopleChanged;
    public UnityEvent gameOver;
    
    private void Awake()
    {
        instance = this;
        StartCoroutine(moodDecrease());
        StartCoroutine(scoreIncrease());
    }
    public void ChangeMood(float moodBonus)
    {
        mood = Mathf.Clamp01(mood + moodBonus);
        if (mood <= 0f)
        {
            gameOver.Invoke();
            return;
        }
        moodChanged.Invoke(mood);
    }
    public void ChangeScore(int scoreBonus)
    {
        score += scoreBonus;
        scoreChanged.Invoke(score);
    }
    
    public int GetScore()
    {
        return score;
    }

    IEnumerator moodDecrease()
    {
        while (true)
        {
            yield return null;
            ChangeMood(moodOverTime * Time.deltaTime);
        }
    }
    IEnumerator scoreIncrease()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            ChangeScore(scoreOverTime);
        }
    }
}
