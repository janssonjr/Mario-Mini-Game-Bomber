using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Panel
{
    InGamePanel,
    LosePanel,
    MainMenuPanel
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
        if(aEvent.myNewState == GameStateEnum.Lose)
        {
            panels[(int)Panel.LosePanel].SetActive(true);
            //panels[(int)Panel.InGamePanel].SetAc
        }
        else if(aEvent.myNewState == GameStateEnum.Playing && aEvent.myOldState == GameStateEnum.MainMenu)
        {
            panels[(int)Panel.MainMenuPanel].SetActive(false);
            panels[(int)Panel.InGamePanel].SetActive(true);

        }
    }
}
