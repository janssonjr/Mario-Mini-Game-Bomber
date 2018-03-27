using System;
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
    float myStartLifeTime;
    TileType myTileType = TileType.Length;
    Vector2 myWalDirection;
    bool wasPressed;
    bool myIsInsideGoal;
    bool myHasScored;
    bool myShouldUpdate;
    Goal myGoalHover;
    bool myHasStartedLowHealthTween;

    public TileType MyTileType
    {
        get { return myTileType; }
    }

	// Use this for initialization
	void Start () {
        wasPressed = false;
        myIsInsideGoal = false;
        myHasScored = false;
        myGoalHover = null;
        myShouldUpdate = true;
        myStartLifeTime = myLifetime;
        myHasStartedLowHealthTween = false;
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

        if (myShouldUpdate == false)
            return;

        Vector2 position = transform.position;
        transform.position = position + myWalDirection * GameManager.DeltaTime;
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
            //GameEventManager.GameLost();
        }
        else if(myLifetime <= (myStartLifeTime / 2f))
        {
            if(myHasStartedLowHealthTween == false)
            {
                StartLowHealthTween();
            }
        }
    }

    private void StartLowHealthTween()
    {
        myHasStartedLowHealthTween = true;
        iTween.ScaleTo(gameObject, iTween.Hash("time", 0.6, "scale", new Vector3(2.5f, 2.5f, 1f), "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
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
                GameEventManager.ScorePoint(this, 1);
                myHasScored = true;
                transform.position = myGoalHover.transform.position;
                myGoalHover = null;
                if(myHasStartedLowHealthTween == true)
                {
                    iTween.Stop(gameObject);
                }
            }
            else
            {
                GameEventManager.GameLost();
            }
        }
        //GetComponent<BoxCollider2D>().enabled = true;
    }

    public bool ShouldUpdate
    {
        set { myShouldUpdate = value; }
    }
}
