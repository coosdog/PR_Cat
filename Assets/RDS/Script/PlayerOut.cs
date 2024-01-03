using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Temp;

public class PlayerOut : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<TestPlayer>().Die();
    }
}
