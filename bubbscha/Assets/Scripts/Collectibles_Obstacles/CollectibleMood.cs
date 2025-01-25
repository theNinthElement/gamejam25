using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleMood : MonoBehaviour
{
    [Tooltip("Number added to the mood")]
    [SerializeField][Range(-1, 1)] float moodBonus = 0f;


    private void OnTriggerEnter(Collider other)
    {
        //Maybe identifier for player
        //if (other.CompareTag("Player"))
        //{
        GameStats.instance.ChangeMood(moodBonus);
        Renderer.Destroy(gameObject);
        //}
    }
}
