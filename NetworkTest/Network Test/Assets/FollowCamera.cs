using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    public float xOffset, yOffset, zOffset;
    public float xFactor, yFactor, zFactor;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Camera.main.transform.position.x * xFactor + xOffset, Camera.main.transform.position.y * yFactor+ yOffset, Camera.main.transform.position.z * zFactor + zOffset);
	}
}
