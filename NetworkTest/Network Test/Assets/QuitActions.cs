using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitActions : MonoBehaviour {

    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");
        Application.Quit();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
