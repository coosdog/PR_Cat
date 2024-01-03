using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviourPun
{
    [SerializeField] Transform[] playerPos;    

    void Start()
    {
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        //    {
        //        PhotonNetwork.Instantiate("Player", playerPos[i].position, Quaternion.identity);
        //    }
        //}
        GameObject playerObj = PhotonNetwork.Instantiate("Player", transform.position, Quaternion.identity);
        GameManager.instance.playerList.Add(playerObj.GetComponent<Temp.TestPlayer>());
        // 게임매니저가 TestPlayer든 게임오브젝트 리스트라던지 가지고 있어서 담아준다.
    }
}
