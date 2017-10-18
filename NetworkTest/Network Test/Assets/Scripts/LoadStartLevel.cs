using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Resets game to start level if GameState has not been instantiated yet

public class LoadStartLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (GameState.Instance == null)
        {
            Debug.Break();
            //Application.LoadLevel(0);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
