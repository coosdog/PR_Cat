using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Temp;

public class PlayerOut : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TestPlayer>() != null)
            other.gameObject.GetComponent<Temp.TestPlayer>().Die();
    }
}
