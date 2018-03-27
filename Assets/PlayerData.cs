using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    public int myScore;

    private void OnEnable()
    {
        GameEventManager.OnGameEvent += OnGmeEvent;
        GameEventManager.OnGameStateEvent += OnGameStateEvent;
        Init();
    }

    private void OnDisable()
    {
        GameEventManager.OnGameEvent -= OnGmeEvent;
        GameEventManager.OnGameStateEvent -= OnGameStateEvent;

    }

    private void OnGameStateEvent(GameEventManager.GameStateEvent obj)
    {
        if (obj.myNewState == GameStateEnum.Playing)
            myScore = 0;
    }

    private void Init()
    {
        myScore = 0;    
    }

    private void OnGmeEvent(GameEventManager.GameEvent aEvent)
    {
        myScore += aEvent.myScore;
        Debug.Log("Score: " + myScore);
    }
}
