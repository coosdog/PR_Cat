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
    public event Action onGameStart;
    public event Action onGameEnd;      
    public int PlayerCount
    {
        get => playerCount;
        set
        {
            playerCount = value;            
            if(playerCount == 1)
            {
                // ½Â¸®¾ÀÀüÈ¯
                Debug.Log("½Â¸®");
            }
        }
    }
    private int playerCount;

    private void Start()
    {
        //if (!photonView.IsMine)
        //    return;        
        onGameStart += () => { PlayerCount = PhotonNetwork.CurrentRoom.PlayerCount; };        
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
}
