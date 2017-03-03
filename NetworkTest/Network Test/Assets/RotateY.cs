using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateY : MonoBehaviour {

    public float rotationspeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 rotationNew = new Vector3(0, 0, 0);
        rotationNew.y = transform.localEulerAngles.y + rotationspeed * Time.deltaTime;

        transform.localEulerAngles = rotationNew;

	}
}
