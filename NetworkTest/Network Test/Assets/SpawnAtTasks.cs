using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAtTasks : MonoBehaviour {

    public Transform SpawnTask1;
    public Transform SpawnTask2;
    public Transform SpawnTask3;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (GameState.Instance.isPlayerCaptain())
        {

            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                transform.position = SpawnTask1.position;
            }

            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                transform.position = SpawnTask2.position;
            }

            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                transform.position = SpawnTask3.position;
            }

        }
    }
}
