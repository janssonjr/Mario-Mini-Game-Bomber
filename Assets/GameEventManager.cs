using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEventManager : MonoBehaviour {

    public class GameEvent
    {
        public TileType myTileType;
        public int myScore;
    }

    public class GameStateEvent
    {
        public GameStateEnum myGameState;
    }

    public static Action<GameEvent> OnGameEvent;
    public static Action<GameStateEvent> OnGameStateEvent;
    //private static GameEventManager myInstance;

    //public static GameEventManager Instance
    //{
    //    get
    //    {
    //        if (myInstance == null)
    //            myInstance = new GameEventManager();
    //        return myInstance;
    //    }
    //}
    
    public static void GameLost()
    {
        if (OnGameStateEvent != null)
            OnGameStateEvent.Invoke(new GameStateEvent { myGameState = GameStateEnum.Lose});
    }

    public static void ScorePoint(TileType aTileType, int aScoreAmount)
    {
        if (OnGameEvent != null)
            OnGameEvent.Invoke(new GameEvent { myTileType = aTileType, myScore = aScoreAmount });
    }
}
