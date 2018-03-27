using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosePanel : MonoBehaviour {

    public void Retry()
    {
        GameEventManager.Retry();
    }
}