using System.Collections;
using System.Collections.Generic;
using Temp;
using UnityEngine;

public class Grinder : MonoBehaviour
{
    int correctionNum = 100;
    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime* correctionNum);
    }
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.SetActive(false);
        if(collision.gameObject.TryGetComponent(out TestPlayer player))
        {
            player.Die();
        }
    }
}
