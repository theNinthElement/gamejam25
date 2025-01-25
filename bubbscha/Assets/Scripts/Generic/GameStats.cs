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
    [Tooltip("Number for collected people. Multiplier for score and mood.")]
    [SerializeField] int people = 1;
    //[Tooltip("Movement speed of the player")]
    //public float speed = 1f;
    //[SerializeField] GameObject scoreboard;
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
            ChangePeople(-1);
            mood = 1f;
            if (people <= 0)
            {
                gameOver.Invoke();
            }
        }
        moodChanged.Invoke(mood);
    }
    public void ChangeScore(int scoreBonus)
    {
        score += scoreBonus;
        scoreChanged.Invoke(score);
    }
    public void ChangePeople(int change)
    {
        people += change;
        peopleChanged.Invoke(people);
    }
    public int GetScore()
    {
        return score;
    }

    IEnumerator moodDecrease()
    {
        while (true)
        {
            Debug.Log("Update Mood " + mood);
            yield return new WaitForSeconds(1);
            ChangeMood(moodOverTime * people);
        }
    }
    IEnumerator scoreIncrease()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            ChangeScore(scoreOverTime * people);
        }
    }
}
