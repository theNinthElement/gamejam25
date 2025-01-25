using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBalance : MonoBehaviour
{
    [SerializeField] float force = 1f;
    //[SerializeField] float radius =1f;
    //[SerializeField] float upward = 1f;
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 5, 0));
        //other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius, upward);
    }
    private void OnBecameInvisible()
    {
        Renderer.Destroy (gameObject);
    }
}
