using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * Highscore over time
 * people multiplicator
 * people as lives
 */

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
    [Tooltip("Number for collected people")]
    [SerializeField] int people = 1;
    public UnityEvent<float> moodChanged;
    public UnityEvent<int> scoreChanged;
    public UnityEvent peopleChanged;

    private void Awake()
    {
        instance = this;
        StartCoroutine(moodDecrease());
        StartCoroutine(scoreIncrease());
    }

    public void ChangeMood(float moodBonus)
    {
        mood += moodBonus * people;
        if (mood <= 0f)
        {
            ChangePeople(-1);
            if (people <= 0)
            {
                //TODO: End Game, Show Scoreboard
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
        peopleChanged.Invoke();
    }

    IEnumerator moodDecrease()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            ChangeMood(moodOverTime);
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
