using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBehaviour : MonoBehaviour
{
    private Rigidbody rigi;

    public float gravityMultiplier = 0.1f;
    public LayerMask groundLayerMask;
    public LayerMask bowlLayerMask;

    private float airTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody>();
        if (rigi == null)
        {
            rigi = gameObject.AddComponent<Rigidbody>();
            rigi.useGravity = false;
            rigi.constraints = RigidbodyConstraints.FreezePositionZ;
        }
        rigi.sleepThreshold = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigi.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration);
    }

    private void Update()
    {
        airTime += Time.deltaTime;
        Debug.Log("AIRTIME: " + airTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (groundLayerMask == (groundLayerMask | (1 << collision.gameObject.layer)))
        {
            Debug.Log("POP");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (bowlLayerMask == (bowlLayerMask | (1 << collision.gameObject.layer)))
        {
            Debug.Log("Resting");
            airTime = 0;
        }
    }

    public float GetAirTime()
    {
        return airTime;
    }

    public float GetVelocity()
    {
        return rigi.velocity.magnitude;
    }
}