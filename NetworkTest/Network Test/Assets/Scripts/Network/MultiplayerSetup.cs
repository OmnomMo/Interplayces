using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MultiplayerSetup : NetworkLobbyManager{

    public string levelSelect_Scene;

    public bool isCaptainReady;
    public bool isNavigatorReady;

    public bool singlePlayerAble;



    private static MultiplayerSetup instance;
    public static MultiplayerSetup Instance
    {
        get { return instance; }
    }


    void OnApplicationQuit()
    {
        Debug.Log("Ending Game. Closing network Manager");
        if (TCPSocketServer.Instance != null)
        {
            TCPSocketServer.Instance.stopServer();
        }
        NetworkManager.Shutdown();
        NetworkLobbyManager.Shutdown();
    }


    void OnDestroy()
    {
   
      //  print("MultiplayerSetup was destroyed");
    }

    // Use this for initialization
    void Start () {

        //If gamestate hasnt been initialized yet, jump to initialization scene


        if (GameState.Instance != null)
        {
            //if (GameState.Instance.holoLensConnected)
            //{
            //    networkPort = 1755;
            //}

            if (GameState.Instance.skipBuildingPhase)
            {
                playScene = levelSelect_Scene;
            }
        }

        
            
            if (MultiplayerSetup.Instance == null)
        {
            instance = this;
        }
        else
        {
            //Debug.Log("MultiplayerSetup is destroyed");
            //GameObject.Destroy(gameObject);
        }
            

        isCaptainReady = false;
        isNavigatorReady = false;
	}

    public void StartAsCaptain()
    {
       // Debug.Log("Start as captain!");
        GameState.Instance.setPlayerCaptain();
        SetCaptainReady();
    }

    public void StartAsNavigator()
    {
        GameState.Instance.setPlayerNavigator();
        SetNavigatorReady();
    }

    public void SetCaptainReady()
    {
        isCaptainReady = true;
    }

    public void SetNavigatorReady()
    {
        isNavigatorReady = true;
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        //Debug.Log("Connected! Whew");
        SetCaptainReady();
        JoinGameButtons.Instance.ColorCaptainGreen();
        base.OnClientConnect(conn);
    }


    public override void OnServerConnect(NetworkConnection conn)
    {

        //Debug.Log("Connected: " + conn.address + " to " + networkAddress);

        if (conn.address != "localClient")
        {
            SetNavigatorReady();
            JoinGameButtons.Instance.ColorNavigatorGreen();
        }
        base.OnServerConnect(conn);
    }

    public override void OnLobbyServerSceneChanged(string sceneName)
    {

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void StartGame()
    {
       // ServerChangeScene(playScene);
    }
}
