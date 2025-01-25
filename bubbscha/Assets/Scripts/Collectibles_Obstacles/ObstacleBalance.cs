using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBalance : MonoBehaviour
{
    [SerializeField] float force = 1f;
    [SerializeField] float radius =1f;
    [SerializeField] float upward = 1f;
    private void OnCollisionEnter(Collision collision)
    {
        (collision.body as Rigidbody).AddExplosionForce(force,transform.position,radius,upward);
    }
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius, upward);
    }
}
