using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] Transform[] playerPos;

    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            for(int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
            {
                PhotonNetwork.Instantiate("Player", playerPos[i].position, Quaternion.identity);
            }            
        }        
    }        
}
