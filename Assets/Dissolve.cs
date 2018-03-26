using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour {

    public Material myDissolveMaterial;
    MaterialPropertyBlock myMaterialProperty;
    SpriteRenderer myRSpriteRenderer;
    Renderer myRenderer;
	// Use this for initialization
	void Start () {
        myMaterialProperty = new MaterialPropertyBlock();
        myRSpriteRenderer = GetComponent<SpriteRenderer>();
        myRenderer = GetComponent<Renderer>();
	}

    public void StartDissolve()
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", 0f, "to", 1f, "onupdate", "ChangeDissolve", "easetype", iTween.EaseType.linear, "oncomplete", "DissolveComplete", "time", 2f));
        Debug.Log("Starting Dissolve");
    }
	
    void ChangeDissolve(float newValue)
    {
        myMaterialProperty.SetFloat("_Threshold", newValue);
        float currentDissolve = myDissolveMaterial.GetFloat("_Threshold");
        Debug.Log("New Value: " + newValue.ToString() + " CurrentThreshold: " + currentDissolve.ToString());
        myRenderer.SetPropertyBlock(myMaterialProperty);
    }

    void DissolveComplete()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        myRenderer.GetPropertyBlock(myMaterialProperty);
        myMaterialProperty.SetColor("_Color", myRSpriteRenderer.color);
        myMaterialProperty.SetTexture("_MainTex", myRSpriteRenderer.sprite.texture);
        myRenderer.SetPropertyBlock(myMaterialProperty);
    }
}
