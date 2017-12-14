﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameras : MonoBehaviour {

    public Canvas questDisplay;

	// Use this for initialization
	void Start () {
        if (GameState.Instance.isPlayerCaptain())
        {
            Camera.main.targetDisplay = 1;
            GetComponent<Camera>().targetDisplay = 0;
            GameObject.Find("Overlay_Camera").GetComponent<Camera>().targetDisplay = 1;

            foreach (Canvas ca in FindObjectsOfType<Canvas>())
            {
                ca.targetDisplay = 1;
            }

            questDisplay.targetDisplay = 1;
        } else
        {
            GameObject.Find("Overlay_Camera").GetComponent<Camera>().targetDisplay = 0;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}