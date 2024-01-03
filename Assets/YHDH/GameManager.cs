using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Temp;

public class GameManager : Singleton<GameManager>, IPunObservable
{
    public event Action onGameStart;
    public event Action onGameEnd;
    private const int VictorySceneNumber = 5;
    public int PlayerCount
    {
        get => playerCount;
        set
        {
            playerCount = value;
            if(playerCount == 1)
            {
                if (PhotonNetwork.IsMasterClient)
                    PhotonNetwork.LoadLevel(VictorySceneNumber);
            }
        }
    }
    private int playerCount;

    private void Start()
    {
        //if (!photonView.IsMine)
        //    return;        
        onGameStart += () => {  PlayerCount = PhotonNetwork.CurrentRoom.PlayerCount; };
        onGameEnd += () => { PlayerCount = 0; }; 
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode lsm) => 
        {
            if (scene.name == "Lobby" || scene.name == "WaitRoom")
                return;
            GameStart(); 
        };        
    }    

    public void GameStart()
    {
        onGameStart?.Invoke();
    }
    public void GameEnd()
    {
        onGameEnd?.Invoke();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(PlayerCount);
        }
        else
        {
            PlayerCount = (int)stream.ReceiveNext();
        }
    }

    private void Update()
    {
        Debug.Log(PlayerCount);
    }
}
