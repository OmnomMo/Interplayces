using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (GameState.Instance != null)
        {
            GameState.Instance.gameHasStarted = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
