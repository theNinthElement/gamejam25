using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float force = 1f;
    //[SerializeField] float radius =1f;
    //[SerializeField] float upward = 1f;
    public UnityEvent<float> collision;
    private float _cooldown;

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time < _cooldown) return;
        bool left = Random.value <= 0.5f;
        if (left)
        {
            _cooldown = Time.time + 1;
            GameManager.instance.PlayerCollision(-force);
        }
        else
        {
            _cooldown = Time.time + 1;
            GameManager.instance.PlayerCollision(force);
        }
    }
}
