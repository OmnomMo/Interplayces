using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPauseMenu : MonoBehaviour {

    public float timeNeeded;

    public float timeStartPressed;
    public bool bothButtonsPressed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Accelerate") && Input.GetButton("Brake"))
        {
            if (bothButtonsPressed)
            {
                if (Time.time - timeStartPressed > timeNeeded)
                {
                    NetworkActions.Instance.CmdPauseGame();
                }
            } else
            {
                bothButtonsPressed = true;
                timeStartPressed = Time.time;
            }
        } else
        {
            bothButtonsPressed = false;
        }
	}
}
