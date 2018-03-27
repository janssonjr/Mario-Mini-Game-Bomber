using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnDirection
{
    Top,
    Bottom
}

public class Spawner : MonoBehaviour {

    public SpawnDirection mySpawnDirection;
    public float mySpawnDelay;
    private float myOriginalSpawnDeley;
    public GameObject objectToSpawn;
    bool isRunning;
	// Use this for initialization
	void Start () {
       
	}

    private void OnEnable()
    {
        Init();
        GameEventManager.OnGameStateEvent += OnGameStateChange;
    }

    private void OnDisable()
    {
        GameEventManager.OnGameStateEvent -= OnGameStateChange;
    }

    void Init()
    {
        myOriginalSpawnDeley = mySpawnDelay;
    }


    private void OnGameStateChange(GameEventManager.GameStateEvent obj)
    {
        if(obj.myNewState == GameStateEnum.Lose)
        {
            isRunning = false;
            StopAllCoroutines();
        }
        else if(obj.myNewState == GameStateEnum.Playing)
        {
            isRunning = true;
            mySpawnDelay = myOriginalSpawnDeley;
            StartCoroutine(Spawn());
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator Spawn()
    {
        while(isRunning)
        {
            yield return new WaitForSeconds(mySpawnDelay);
            GameObject ob = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            Tile tile = ob.GetComponent<Tile>();
            Vector2 walkDirection = GetRandomWalkDirection();
            TileType tileType = (TileType)UnityEngine.Random.Range(0, (int)TileType.Length);
            tile.Init(walkDirection, tileType);
            if (mySpawnDelay <= 1.2f)
                mySpawnDelay = 1.2f;
            else
                mySpawnDelay -= GameManager.DeltaTime * 1;

            GameManager.AddGameObject(tile);
            //isRunning = false;
        }

    }

    Vector2 GetRandomWalkDirection()
    {
        Vector2 direction = Vector2.zero;
        direction.y = mySpawnDirection == SpawnDirection.Top ? -1 : 1f;
        direction.x = UnityEngine.Random.Range(-1f, 1f);
        return direction;
    }
}
