using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tingida : MonoBehaviour
{
    public Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Bounce>() != null)
        {
            rb.isKinematic = false;
            rb.AddForce(new Vector3(100, 600, 1000)*Time.deltaTime,ForceMode.Impulse);
        }
    }
}
