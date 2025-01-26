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
    [Tooltip("Decrase of mood while bubble is on the ground")]
    [SerializeField][Range(-1,0)] float moodBubbleOnStreet = -0.1f;

    public UnityEvent<float> moodChanged;
    public UnityEvent<int> scoreChanged;
    public UnityEvent<int> peopleChanged;
    public UnityEvent gameOver;
    private bool bubbleOnGround;
    
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
            if (GameManager.instance.isRunning)
            {
                gameOver.Invoke();
            }

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

    public void GroundBubble(bool onGround)
    {
        bubbleOnGround = onGround;
    }

    IEnumerator moodDecrease()
    {
        while (true)
        {
            yield return null;
            if (bubbleOnGround)
            {
                ChangeMood(moodBubbleOnStreet * Time.deltaTime);
            }
            else
            {
                ChangeMood(moodOverTime * Time.deltaTime);
            }
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
