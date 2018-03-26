using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEventManager : MonoBehaviour {

    public class GameEvent
    {
        public Tile myScoredTile;
        public int myScore;
    }

    public class GameStateEvent
    {
        public GameStateEnum myNewState;
        public GameStateEnum myOldState;
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
            OnGameStateEvent.Invoke(new GameStateEvent { myNewState = GameStateEnum.Lose, myOldState = GameStateEnum.Playing});
    }

    public static void StartGame()
    {
        if (OnGameStateEvent != null)
            OnGameStateEvent.Invoke(new GameStateEvent { myNewState = GameStateEnum.Playing, myOldState = GameStateEnum.MainMenu});
    }

    public static void ScorePoint(Tile aTile, int aScoreAmount)
    {
        if (OnGameEvent != null)
            OnGameEvent.Invoke(new GameEvent { myScoredTile = aTile, myScore = aScoreAmount });
    }
}
