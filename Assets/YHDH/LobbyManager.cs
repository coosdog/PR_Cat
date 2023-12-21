using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{    
    void Start()
    {
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("서버에 연결");        
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("서버에 해제");
    }
    
    public void JoinRoom()
    {
        Debug.Log("조인룸");
        // 서버에 연결되어있는지 확인하고
        // 안되어있으면 다시 서버에 연결하는 작업
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("방들어가려고 시도");
            // 서버에 연결되어있을때 방에 들어가거나, 만드는 작업
            PhotonNetwork.JoinRandomRoom();
        }
        else
            PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("방 들어옴");
        //if(PhotonNetwork.IsMasterClient)
        //    PhotonNetwork.Instantiate("Bat", transform.position - new Vector3(0, 11, 0), transform.rotation);
        //PhotonNetwork.Instantiate("RagDollPlayer", transform.position, transform.rotation);
        PhotonNetwork.LoadLevel(1);
        //photonView.RPC("OnGameRoom", RpcTarget.AllBuffered);
        // 마스터가 방만든다
        // 대기 씬으로 전환 된다
        // 
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("방생성됨");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 5;
        PhotonNetwork.CreateRoom("1번방 생성", roomOptions);
    }
}
