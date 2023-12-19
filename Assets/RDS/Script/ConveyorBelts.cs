using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelts : MonoBehaviour
{
    int extraForce = 50;
    public float speed;
    public Vector3 direction;
    public List<GameObject> belts;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < belts.Count; i++)
        {
            if (belts[i].GetComponent<Temp.TestPlayer>() != null)
                belts[i].GetComponent<Rigidbody>().AddForce(direction * speed * Time.deltaTime * extraForce);
            else
                belts[i].GetComponent<Rigidbody>().velocity = direction * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        belts.Add(collision.gameObject);
    }
    private void OnCollisionExit(Collision collision)
    {
        belts.Remove(collision.gameObject);
    }
}
