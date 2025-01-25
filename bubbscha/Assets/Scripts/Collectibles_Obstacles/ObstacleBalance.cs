using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBalance : MonoBehaviour
{
    [SerializeField] GameStats stats;
    [Tooltip("Change of Balance")]
    [SerializeField] float balanceChange;

    private void OnTriggerEnter(Collider other)
    {
        //Maybe identifier for player
        //if (other.CompareTag("Player"))
        //{
        
        //}
    }
}
