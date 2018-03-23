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
    public GameObject objectToSpawn;
    bool isRunning;
	// Use this for initialization
	void Start () {
        isRunning = true;
        StartCoroutine(Spawn());
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
            TileType tileType = (TileType)Random.Range(0, (int)TileType.Length - 1);
            tile.Init(walkDirection, tileType);
            isRunning = false;
        }

    }

    Vector2 GetRandomWalkDirection()
    {
        Vector2 direction = Vector2.zero;
        direction.y = mySpawnDirection == SpawnDirection.Top ? -1 : 1f;
        direction.x = Random.Range(-1f, 1f);
        return direction;
    }
}
