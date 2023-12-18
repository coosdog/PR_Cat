using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public event Action onGameStart;
    public event Action onGameEnd;

    public void GameStart()
    {
        onGameStart?.Invoke();  
    }

    public void GameEnd()
    {
        onGameEnd?.Invoke();
    }
}
