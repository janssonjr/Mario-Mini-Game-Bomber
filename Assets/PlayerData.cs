using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

    //private static PlayerData myInstance;

    //public static PlayerData Instance
    //{
    //    get
    //    {
    //        if (myInstance == null)
    //            myInstance = new PlayerData();
    //        return myInstance;
    //    }
    //}

    public int myScore;

    private void OnEnable()
    {
        GameEventManager.OnGameEvent += OnGmeEvent;
        Init();
    }

    private void OnDisable()
    {
        GameEventManager.OnGameEvent -= OnGmeEvent;
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
