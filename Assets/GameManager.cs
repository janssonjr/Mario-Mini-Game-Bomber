using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStateEnum
{
    Playing,
    Lose
}

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameStateEnum myGameState;
    private static float deltaTime;
    private void OnEnable()
    {
        deltaTime = 0f;
        GameEventManager.OnGameStateEvent += OnGameState;
    }

    private void OnDisable()
    {
        GameEventManager.OnGameStateEvent += OnGameState;
    }

    private void OnGameState(GameEventManager.GameStateEvent obj)
    {
        deltaTime = 0;
    }

    private void Update()
    {
        deltaTime = Time.deltaTime;
    }

    public static float DeltaTime
    {
        get { return deltaTime; }
    }
}
