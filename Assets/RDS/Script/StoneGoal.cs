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
        
        
        List<Collider> listCol = new List<Collider>();
        Collider[] cols = Physics.OverlapSphere(transform.position, 500f, 1 << 6);
        
        if(collision.gameObject.TryGetComponent(out TestPlayer player))
        {
            int a = collision.gameObject.GetPhotonView().ViewID;
            Dictionary<int, Photon.Realtime.Player> DIC = PhotonNetwork.CurrentRoom.Players;

            GameManager.instance.playerList


            for(int i = 0; i < DIC.Count; i++)
            {
                
            }

            foreach(var p in DIC)
            {
                
            }
        }

        foreach (Collider col in cols)
        {
            if(col.GetComponent<TestPlayer>() != null)
            {
                listCol.Add(col);
            }
            
        }
        foreach(Collider col in listCol)
        {
            if (col.gameObject.GetPhotonView().ViewID != a)
            {
                if (col.GetComponent<TestPlayer>() != null)
                    col.GetComponent<TestPlayer>().Die();
            }
        }
        
    }
    public int listSort(Vector3 a, Vector3 b)
    {
        return a.magnitude < b.magnitude ? 1 : -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 500f);
    }
}
