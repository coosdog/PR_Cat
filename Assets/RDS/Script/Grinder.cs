using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grinder : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.SetActive(false);
    }
}
