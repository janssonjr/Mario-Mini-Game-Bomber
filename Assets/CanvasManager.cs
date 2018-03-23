using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Panel
{
    InGamePanel,
    LosePanel
}

public class CanvasManager : MonoBehaviour {

    public List<GameObject> panels;

    private void OnEnable()
    {
        GameEventManager.OnGameStateEvent += OnGameStateEvent;
    }

    private void OnDisable()
    {
        GameEventManager.OnGameStateEvent -= OnGameStateEvent;
    }

    private void OnGameStateEvent(GameEventManager.GameStateEvent aEvent)
    {
        if(aEvent.myGameState == GameStateEnum.Lose)
        {
            panels[(int)Panel.LosePanel].SetActive(true);
            //panels[(int)Panel.InGamePanel].SetAc
        }
    }
}
