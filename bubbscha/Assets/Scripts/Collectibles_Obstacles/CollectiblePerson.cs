using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePerson : MonoBehaviour
{
    [Tooltip("Number of people picked up")]
    [SerializeField] int people=1;

    private void OnTriggerEnter(Collider other)
    {
        //Maybe identifier for player
        //if (other.CompareTag("Player"))
        //{
        GameStats.instance.ChangePeople(people);
        Renderer.Destroy(gameObject);
        //}
    }
}
