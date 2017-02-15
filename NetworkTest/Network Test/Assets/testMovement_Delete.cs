using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMovement_Delete : MonoBehaviour {
     public float speed = 10.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.position += move * speed * Time.deltaTime;

    }
}
