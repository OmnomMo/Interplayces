﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameState : MonoBehaviour {


    public enum PlayerTypes { Captain, Navigator, NavigatorAR, None }

    PlayerTypes playerType;

    public GameObject networkManager;

    private static GameState instance;
    public static GameState Instance
    {
        get { return instance; }
    }



    // Use this for initialization
    void Start () {
        if (GameState.Instance == null) {
            instance = this;
        } else
        {
            GameObject.Destroy(gameObject);
        }


        Object.DontDestroyOnLoad(gameObject);
        playerType = PlayerTypes.None;
    }

    public PlayerTypes getPlayerType()
    {
        return playerType;
    }

    public void StartAsCaptain()
    {
        setPlayerCaptain();
        networkManager.GetComponent<NetworkManager>().StartHost();
  


    }


    public void StartAsNavigator()
    {
        setPlayerNavigator();
        networkManager.GetComponent<NetworkManager>().StartClient();



    }


    public void setPlayerCaptain()
    {


        //Debug.Log("Set Player Captain");
        playerType = PlayerTypes.Captain;
    }

    public bool isPlayerCaptain()
    {
        return (playerType == PlayerTypes.Captain);
    }

    public void setPlayerNavigator()
    {

        // Debug.Log("Set Player Navigator");
        playerType = PlayerTypes.Navigator;

        //Navigator inizialization stuff: 
        //Camera.main.gameObject.GetComponent<CameraBehaviour>().ChangeCameraToNavigator();

        //Camera.main.GetComponent<References>().navigatorInterface.SetActive(true);
        //Camera.main.GetComponent<References>().energyBar.GetComponent<EnergyBar>().Initialize();
       // __________________

        //transform.Find("Interface Navigator").gameObject.SetActive(true);
        //GameObject.Find("EnergyBar").GetComponent<EnergyBar>().Initialize();
    }

    public bool isPlayerNavigator()
    {
        return (playerType == PlayerTypes.Navigator);
    }

    public void setPlayerNavigatorAR()
    {
        playerType = PlayerTypes.NavigatorAR;
    }

    public bool isPlayerNavigatorAR()
    {
        return (playerType == PlayerTypes.NavigatorAR);
    }

    // Update is called once per frame
    void Update () {
		
	}
}