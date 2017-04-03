using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithCameraDistance : MonoBehaviour {

    public float scaleFactor;

    Vector3 originalScale;
    // Use this for initialization
    void Start () {
        originalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        float scale = Camera.main.transform.position.y * scaleFactor;
        transform.localScale = originalScale * scale;
    }
}
