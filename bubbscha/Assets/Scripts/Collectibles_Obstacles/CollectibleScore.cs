using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScore : MonoBehaviour
{
    [SerializeField] GameStats stats;
    [Tooltip("Number added to the score")]
    [SerializeField] int scoreBonus;

    private void OnTriggerEnter(Collider other)
    {
        //Maybe identifier for player
        //if (other.CompareTag("Player"))
        //{
            stats.IncreaseScore(scoreBonus);
        //}
    }
}
