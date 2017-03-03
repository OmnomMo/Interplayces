using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideForNavigator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (GameState.Instance != null && GameState.Instance.isPlayerNavigator())
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
