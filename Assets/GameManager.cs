using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStateEnum
{
    Playing,
    Lose,
    MainMenu
}

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameStateEnum myGameState;
    private static float deltaTime;
    private static List<Tile> myGameObjects = new List<Tile>();
    private void OnEnable()
    {
        deltaTime = 0f;
        GameEventManager.OnGameStateEvent += OnGameState;
        GameEventManager.OnGameEvent += OnGameEvent;
    }

    private void OnDisable()
    {
        GameEventManager.OnGameStateEvent -= OnGameState;
        GameEventManager.OnGameEvent -= OnGameEvent;

    }

    private void OnGameEvent(GameEventManager.GameEvent obj)
    {
        Tile temp = myGameObjects.Find(ob => ob == obj.myScoredTile);
        if(temp != null)
        {
            myGameObjects.Remove(temp);
        }
    }

    private void OnGameState(GameEventManager.GameStateEvent obj)
    {
        if(obj.myNewState == GameStateEnum.Playing && obj.myOldState == GameStateEnum.Lose)
        {
            ClearAndDestroyObjects();
        }
        else if(obj.myNewState == GameStateEnum.Lose && obj.myOldState == GameStateEnum.Playing)
        {
            StopAllMovingGameObjects();
        }
    }

    private void Update()
    {
        deltaTime = Time.deltaTime;
    }

    public static float DeltaTime
    {
        get { return deltaTime; }
    }

    public static void AddGameObject(Tile aObject)
    {
        myGameObjects.Add(aObject);
    }

    void ClearAndDestroyObjects()
    {
        foreach(Tile ob in myGameObjects)
        {
            Destroy(ob.gameObject);
        }
        myGameObjects.Clear();
    }

    void StopAllMovingGameObjects()
    {
        foreach(Tile tile in myGameObjects)
        {
            tile.ShouldUpdate = false;
        }
    }
}
