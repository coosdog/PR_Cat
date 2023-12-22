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

    private void Start()
    {
        //if (!photonView.IsMine)
        //    return;        
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode lsm) => { GameStart(); };        
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
