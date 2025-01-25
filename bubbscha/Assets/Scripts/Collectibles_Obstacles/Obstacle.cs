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
        collision.Invoke(force);
        //GetComponent<Rigidbody>().AddForce(new Vector3(0, 100, 0));
        //other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius, upward);
    }
    private void OnBecameInvisible()
    {
        //Renderer.Destroy(gameObject);
    }
}
