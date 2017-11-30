using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;

public class MultiplayerSetup : NetworkLobbyManager{

    public string levelSelect_Scene;

    public bool isCaptainReady;
    public bool isNavigatorReady;

    public bool singlePlayerAble;

    public bool instantConnect;
    public string roleFilePath;

    public GameObject waitForCaptainPanel;
    public GameObject waitForNavigatorPanel;



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

        if (instantConnect)
        {
            StartCoroutine(InstantConnectDelayed());
        }
	}

    public IEnumerator InstantConnectDelayed()
    {
        yield return null;

        string role = ReadRoleFromFile();

        if (role == "captain")
        {
            Debug.Log("Its a captain!");
            JoinGameButtons.Instance.JoinAsCaptain();
            waitForNavigatorPanel.SetActive(true);

            Debug.Log("displays connected: " + Display.displays.Length);
            // Display.displays[0] is the primary, default display and is always ON.
            // Check if additional displays are available and activate each.
            if (Display.displays.Length > 1)
                Display.displays[1].Activate();
            //if (Display.displays.Length > 2)
            //    Display.displays[2].Activate();
        }
        else
        {
            JoinGameButtons.Instance.JoinAsNavigator();
            waitForCaptainPanel.SetActive(true);
        }

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

    public string ReadRoleFromFile()
    {

        StreamReader reader = new StreamReader(Application.streamingAssetsPath + roleFilePath);

        string json = reader.ReadToEnd();

        return json;
        
        //this = JsonUtility.FromJsonOverwrite()
        //Debug.Log(sourceText.ToString());
    }
}
