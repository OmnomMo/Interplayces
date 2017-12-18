using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartProgram : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //game has already started and we entered this scene from play mode beause someone disconnected.
        //Deletes all presistent objects and resets game to setup scene.
		if (GameState.Instance != null && GameState.Instance.gameHasStarted)
        {
            LoadedObjectManager.Instance.ResetPersistentObjects();
            SceneManager.LoadScene(0);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
