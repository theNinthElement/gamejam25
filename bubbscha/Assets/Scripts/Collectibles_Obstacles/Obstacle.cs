using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float force = 1f;
    //[SerializeField] float radius =1f;
    //[SerializeField] float upward = 1f;
    public UnityEvent<float> collision;
    private void OnTriggerEnter(Collider other)
    {
        bool left = Random.value <= 0.5f;
        if (left)
        {
            Debug.Log("Left");
            GameManager.instance.PlayerCollision(-force);
        }
        else
        {
            Debug.Log("Right");
            GameManager.instance.PlayerCollision(force);
        }
    }
}
