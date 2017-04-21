using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Object position is set every frame to follow Camera.
//factor variables enable parallax effect

public class FollowCamera : MonoBehaviour {

    public float xOffset, yOffset, zOffset;
    public float xFactor, yFactor, zFactor;

    public bool dontRescale;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Camera.main.transform.position.x * xFactor + xOffset, Camera.main.transform.position.y * yFactor+ yOffset, Camera.main.transform.position.z * zFactor + zOffset);
        if (!dontRescale)
        {
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
	}
}
