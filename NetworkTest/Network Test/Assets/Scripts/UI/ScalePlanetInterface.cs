using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    //Transform object to have same scale independent of parent.

public class ScalePlanetInterface : MonoBehaviour {



    //public float scale;

	// Use this for initialization
	void Start () {
        transform.localScale = new Vector3(1 / transform.parent.parent.localScale.x, 1 / transform.parent.parent.localScale.y, 1 / transform.parent.parent.localScale.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
