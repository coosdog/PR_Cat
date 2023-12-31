using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JongWoo;
public class R4_Road : MonoBehaviour
{
    public GameObject BackUp;
    Vector3 ReLocate;
    private void Start()
    {
        ReLocate = new Vector3(BackUp.transform.position.x, BackUp.transform.position.y+1, BackUp.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Temp.TestPlayer>() != null)
        {
            other.transform.position = ReLocate;
        }
    }
}
