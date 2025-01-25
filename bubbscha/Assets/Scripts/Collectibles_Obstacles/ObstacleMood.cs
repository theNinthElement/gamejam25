using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMood : MonoBehaviour
{
    [SerializeField] GameStats stats;
    [Tooltip("Penalty for mood")]
    [SerializeField] float moodPenalty;

    private void OnTriggerEnter(Collider other)
    {
        //Maybe identifier for player
        //if (other.CompareTag("Player"))
        //{
        stats.DecreaseMood(moodPenalty);
        //}
    }
}
