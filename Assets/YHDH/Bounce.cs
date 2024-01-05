using JongWoo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public AudioClip stoneAudio;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //SoundManager.instance.Play(stoneAudio, this.transform);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.forward * 4f * Time.deltaTime,Space.World);
        transform.Rotate(new Vector3(0, -1, 0));
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector3.zero; 
        rb.AddForce(Vector3.up * 2f, ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.GetComponent<Temp.TestPlayer>() != null) 
        {
            other.GetComponent<Temp.TestPlayer>().Die();
        }
    }
}
