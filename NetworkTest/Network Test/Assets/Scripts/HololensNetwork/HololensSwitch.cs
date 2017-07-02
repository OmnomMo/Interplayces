using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HololensSwitch : MonoBehaviour {

    //public GameObject multiplayerSetup;
    GameObject multiplayerSetup;
    GameObject hololensParent;
    GameObject hololensNetworking;
    // Use this for initialization
    void Start () {

        multiplayerSetup = GameObject.Find("MultiplayerSetup");
        hololensParent = GameObject.Find("HololensParent");
        hololensNetworking = hololensParent.transform.FindChild("NetworkingComponentHololens").gameObject;

        multiplayerSetup.GetComponent<MultiplayerSetup>().networkPort = 7777;
        multiplayerSetup.GetComponent<MultiplayerSetup>().minPlayers = 2;

        multiplayerSetup.GetComponent<ServerUDP>().enabled = true;

        GameState.Instance.holoLensConnected = false;

        hololensNetworking.SetActive(false);
        hololensNetworking.GetComponent<BroadcastingBehaviour>().enabled = false;
        hololensNetworking.GetComponent<TCPSocketServer>().enabled = false;
        hololensNetworking.GetComponent<ProcessMessages>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleHololens(bool holo)
    {
        


        if (!holo)
        {
            multiplayerSetup.GetComponent<MultiplayerSetup>().networkPort = 7777;
            multiplayerSetup.GetComponent<MultiplayerSetup>().minPlayers = 2;

            multiplayerSetup.GetComponent<ServerUDP>().enabled = true;

            GameState.Instance.holoLensConnected = false;

            hololensNetworking.SetActive(false);
            hololensNetworking.GetComponent<BroadcastingBehaviour>().enabled = false;
            hololensNetworking.GetComponent<TCPSocketServer>().enabled = false;
            hololensNetworking.GetComponent<ProcessMessages>().enabled = false;

        } else
        {
            multiplayerSetup.GetComponent<MultiplayerSetup>().networkPort = 1755;
            multiplayerSetup.GetComponent<MultiplayerSetup>().minPlayers = 1;

            multiplayerSetup.GetComponent<ServerUDP>().enabled = false;

            GameState.Instance.holoLensConnected = true;

            hololensNetworking.SetActive(true);
            hololensNetworking.GetComponent<BroadcastingBehaviour>().enabled = true;
            hololensNetworking.GetComponent<TCPSocketServer>().enabled = true;
            hololensNetworking.GetComponent<ProcessMessages>().enabled = true;
        }
    }
}
