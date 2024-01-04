using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Temp;

public class GameManager : Singleton<GameManager>
{
    public List<TestPlayer> playerList;
    public event Action onGameStart;
    public event Action onGameEnd;
    private const int VictorySceneNumber = 5;
    public int PlayerCount
    {
        get => playerCount;
        set
        {
            Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount+" :: pc ---> " +playerCount);
            if(playerCount == 1)
            {
                playerCount = 0;
                Debug.Log("³­°¡?");
                SceneManager.LoadScene(VictorySceneNumber);
            }
            playerCount = value;
        }
    }
    [SerializeField]
    private int playerCount;

    private void Start()
    {
        //playerCount = 0;
        //if (!photonView.IsMine)
        //    return;        
        onGameStart = () => { PlayerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            Debug.Log("onGameStart ---> "+PhotonNetwork.CurrentRoom.PlayerCount);
        };
        // onGameEnd += () => { PlayerCount = 0; }; 
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode lsm) =>
        {
            if (scene.name == "Lobby" || scene.name == "WaitRoom" || scene.name == "VictoryScene")
            {
                GameManager.instance.playerCount = 0;
                return;
            }
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

    private void Update()
    {
       // Debug.Log(PlayerCount);
    }
}
