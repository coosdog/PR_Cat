using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public event Action onGameStart;
    public event Action onGameEnd;

    private void Start()
    {
        //if (!photonView.IsMine)
        //    return;

        onGameStart += () => { PhotonNetwork.Instantiate("Player", new Vector3(10, 0, 10), Quaternion.identity); };
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
