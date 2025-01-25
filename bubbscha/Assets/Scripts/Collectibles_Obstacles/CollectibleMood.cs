using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleMood : MonoBehaviour
{
    [SerializeField] GameStats stats;
    [Tooltip("Number added to the mood")]
    [SerializeField] float moodBonus;


    private void OnTriggerEnter(Collider other)
    {
        //Maybe identifier for player
        //if (other.CompareTag("Player"))
        //{
            GameStats.instance.ChangeMood(moodBonus);
        //}
    }
}
