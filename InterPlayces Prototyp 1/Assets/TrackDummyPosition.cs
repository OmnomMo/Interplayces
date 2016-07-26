using UnityEngine;
using System.Collections;

public class TrackDummyPosition : MonoBehaviour {

    public GameObject dummy;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {

        this.transform.localPosition = dummy.transform.localPosition;

	}
}
