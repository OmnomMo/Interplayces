using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    Vector3 startPosition;

	// Use this for initialization
	void Start () {
        startPosition = this.transform.position;
	}

    public void ResetPosition ()
    {
        this.transform.position = startPosition;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
