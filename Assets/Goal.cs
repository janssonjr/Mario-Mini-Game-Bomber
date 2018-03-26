using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Goal : MonoBehaviour {

    public TileType myGoalType = TileType.Length;
    public int MaxScoredObjects;
    List<GameObject> myScoredObjects = new List<GameObject>();
    int myMaxScoredObjects;

    private void OnEnable()
    {
        GameEventManager.OnGameStateEvent += OnGameStateEvent;
        GameEventManager.OnGameEvent += OnGmeEvent;
        myMaxScoredObjects = 5;
    }

    private void OnDisable()
    {
        GameEventManager.OnGameStateEvent -= OnGameStateEvent;
        GameEventManager.OnGameEvent -= OnGmeEvent;
    }

    private void OnGmeEvent(GameEventManager.GameEvent obj)
    {
        if(obj.myScoredTile.MyTileType == myGoalType)
        {
            AddScoredObject(obj.myScoredTile.gameObject);
        }
    }

    private void OnGameStateEvent(GameEventManager.GameStateEvent obj)
    {
        if(obj.myNewState == GameStateEnum.Playing && obj.myOldState == GameStateEnum.MainMenu)
        {
            ClearScoredObjects();
        }
        else if(obj.myNewState == GameStateEnum.Playing && obj.myOldState == GameStateEnum.Lose)
        {
            ClearScoredObjects();
        }
    }

    void ClearScoredObjects()
    {
        foreach (GameObject go in myScoredObjects)
        {
            Destroy(go);
        }
        myScoredObjects.Clear();
    }
    
    public void AddScoredObject(GameObject aScoredObject)
    {
        myScoredObjects.Add(aScoredObject);
        if(myScoredObjects.Count >= myMaxScoredObjects)
        {
           Dissolve dissolve =  myScoredObjects[0].GetComponent<Dissolve>();
            if(dissolve != null)
            {
                dissolve.StartDissolve();
                myScoredObjects.Remove(dissolve.gameObject);
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
