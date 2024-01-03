using System.Collections;
using System.Collections.Generic;
using Temp;
using UnityEditor;
using UnityEngine;
using Photon.Pun;

public class StoneGoal : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        int a = collision.gameObject.GetPhotonView().ViewID;

        for (int i = 0; i < GameManager.instance.playerList.Count; i++)
        {
            if (GameManager.instance.playerList[i].gameObject.GetPhotonView().ViewID != a)
            {
                GameManager.instance.playerList[i].Die();
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 500f);
    }
}
