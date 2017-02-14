using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadStartLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (GameState.Instance == null)
        {
            Application.LoadLevel(0);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
