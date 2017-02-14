﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

        if (GameState.Instance == null)
        {
            Application.LoadLevel(0);
        }
        else
        {
            if (GameState.Instance.isPlayerNavigator())
            {

              //  Debug.Log("I'm da Navigataa");
                //Camera.main.gameObject.GetComponent<CameraBehaviour>().ChangeCameraToNavigator();

                Camera.main.GetComponent<References>().navigatorInterface.SetActive(true);
                Camera.main.GetComponent<References>().energyBar.GetComponent<EnergyBar>().Initialize();
            }
        }

    }

    public void RebuildSpaceShip()
    {
        GameObject.Find("MultiplayerSetup").GetComponent<NetworkLobbyManager>().ServerChangeScene("SpaceShipEditor");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
