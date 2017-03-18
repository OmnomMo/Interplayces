using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithCameraDistance : MonoBehaviour {

    public float scaleFactor;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float scale = Camera.main.transform.position.y * scaleFactor;
       transform.localScale = new Vector3(scale, scale, scale);
    }
}
