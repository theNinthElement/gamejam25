using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScore : MonoBehaviour
{
    [Tooltip("Number added to the score")]
    [SerializeField] int scoreBonus;

    private void OnTriggerEnter(Collider other)
    {
        //Maybe identifier for player
        //if (other.CompareTag("Player"))
        //{
        GameStats.instance.ChangeScore(scoreBonus);
        //}
    }
}
