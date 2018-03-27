using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public LayerMask pressableTiles;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        bool isMouseDown = Input.GetMouseButtonDown(0);
        if(isMouseDown == true)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 10, pressableTiles);
            if(hit.collider != null)
            {
                Debug.Log(hit.collider.name);
            }
        }
	}
}
