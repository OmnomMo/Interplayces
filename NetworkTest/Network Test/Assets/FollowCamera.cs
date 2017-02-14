using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    public float xOffset, yOffset, zOffset;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Camera.main.transform.position.x + xOffset, Camera.main.transform.position.y + yOffset, Camera.main.transform.position.z + zOffset);
	}
}
