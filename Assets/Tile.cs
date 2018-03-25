using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum TileType
{
    Blue,
    Red,
    Length
}

public class Tile : MonoBehaviour {

    public float myLifetime;
    TileType myTileType = TileType.Length;
    Vector2 myWalDirection;
    bool wasPressed;
    bool myIsInsideGoal;
    bool myHasScored;
    Goal myGoalHover;
	// Use this for initialization
	void Start () {
        wasPressed = false;
        myIsInsideGoal = false;
        myHasScored = false;
        myGoalHover = null;
	}

    public void Init(Vector2 aWalkDirection, TileType aTileType)
    {
        myWalDirection = aWalkDirection;
        myTileType = aTileType;
        SpriteRenderer image = GetComponent<SpriteRenderer>();
        switch (myTileType)
        {
            case TileType.Blue:
                image.color = Color.blue;
                break;
            case TileType.Red:
                image.color = Color.red;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update () {


        Vector2 position = transform.position;
        transform.position = position + myWalDirection * 2f * GameManager.DeltaTime;
	    if(wasPressed)
        {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = -1;
            transform.position = newPos;
        }
        if (myHasScored == true)
            return;
        myLifetime -= GameManager.DeltaTime;
        if (myLifetime <= 0)
        {
            GameEventManager.GameLost();
        }
    }

    void OnTriggerEnter2D(Collider2D aCollider)
    {
        if (wasPressed)
        {
            Goal goal = aCollider.GetComponent<Goal>();
            if (goal == null)
                return;

            myIsInsideGoal = true;
            myGoalHover = goal;
        }
        else
        {
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach(ContactPoint2D cp in collision.contacts)
        {
            myWalDirection = Vector2.Reflect(myWalDirection, cp.normal);
            return;
            /*if(cp.normal.x == 0)
            {
                myWalDirection = new Vector2(myWalDirection.x, -myWalDirection.y);
                Debug.Log("MyWalkDirection: " + myWalDirection + " Normal: " + cp.normal);
                return;
            }
            else if(cp.normal.y == 0)
            {
                myWalDirection = new Vector2(-myWalDirection.x, myWalDirection.y);
                Debug.Log("MyWalkDirection: " + myWalDirection + " Normal: " + cp.normal);
                return;
            }*/
        }
    }

    private void OnTriggerExit2D(Collider2D aCollider)
    {
        Goal goal = aCollider.GetComponent<Goal>();
        if (goal == null)
            return;
        myIsInsideGoal = false;
        myGoalHover = null;
    }

    void OnMouseDown()
    {
        if (myHasScored == true)
            return;
        wasPressed = true;
        //GetComponent<BoxCollider2D>().enabled = false;
    }

    void OnMouseUp()
    {
        if (myHasScored == true)
            return;
        wasPressed = false;
        if(myIsInsideGoal == true && myGoalHover != null)
        {
            if(myGoalHover.myGoalType == myTileType)
            {
                GameEventManager.ScorePoint(myTileType, 1);
                myHasScored = true;
                transform.position = myGoalHover.transform.position;
                myGoalHover = null;
            }
            else
            {
                GameEventManager.GameLost();
            }
        }
        //GetComponent<BoxCollider2D>().enabled = true;
    }
}
