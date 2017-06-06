using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


//Actions for buttons in Lobby

public class JoinGameButtons : MonoBehaviour {

    public Button StartGameButton;

    public GameObject captainCanvas;
    public GameObject navigatorCanvas;

    public Button captainButton;
    public Button navButton;

    private static JoinGameButtons instance;
    public static JoinGameButtons Instance
    {
        get { return instance; }
    }


    public void StartGameButtonPressed()
    {

        //string log = "";

        foreach (NetworkLobbyPlayer nlp in MultiplayerSetup.Instance.lobbySlots)
        {
            nlp.readyToBegin = true;
        }

        
        //foreach (PlayerController pc in ClientScene.localPlayers)
        //{
        //   // log += 
        //    log += pc.ToString();

        //    pc.gameObject.GetComponent<NetworkLobbyPlayer>().readyToBegin = true;
        //}

        //Debug.Log(log);
        //foreach (NetworkLobbyPlayer p in MultiplayerSetup.Instance.lobbySlots)
        //{
        //    p.SendReadyToBeginMessage();
        //}
    }

    // Use this for initialization
    void Start () {

        StartGameButton.interactable = false;

        if (JoinGameButtons.Instance == null)
        {
            instance = this;
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
		//if (MultiplayerSetup.Instance.isCaptainReady && MultiplayerSetup.Instance.isNavigatorReady)
  //      {
  //          StartGameButton.interactable = true;
  //      } else
  //      {
  //          StartGameButton.interactable = false;
  //      }
	}

    public void ColorCaptainGreen()
    {
        captainCanvas.GetComponent<Image>().color = Color.green;
    }

    public void JoinAsCaptain()
    {
       
            MultiplayerSetup.Instance.StartHost();

            if (!GameState.Instance.holoLensConnected)
            {
                MultiplayerSetup.Instance.gameObject.GetComponent<ServerUDP>().StartSendingIP();
            }
            MultiplayerSetup.Instance.StartAsCaptain();

        
        navButton.interactable = false;

        ColorCaptainGreen();

    }

    public void ColorNavigatorGreen()
    {
        navigatorCanvas.GetComponent<Image>().color = Color.green;
    }

    public void JoinAsNavigator()
    {
        MultiplayerSetup.Instance.gameObject.GetComponent<ServerUDP>().StartListener();
        MultiplayerSetup.Instance.StartAsNavigator();

        captainButton.interactable = false;

        ColorNavigatorGreen();
    }
}
