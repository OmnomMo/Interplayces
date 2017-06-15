using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerSwitch : MonoBehaviour {


    GameObject multiplayerSetup;

	// Use this for initialization
	void Start () {
        multiplayerSetup = GameObject.Find("MultiplayerSetup");
        MultiplayerSetup.Instance.minPlayers = 2;
    }
	
	// Update is called once per frame
	void Update () {
    
    }

    public void ToggleSinglePlayer(bool single)
    {
        if (single)
        {
            Debug.Log("SwitchSinglePlayer");
            multiplayerSetup.GetComponent<MultiplayerSetup>().minPlayers = 1;
        }
        else
        {
            Debug.Log("SwitchMultiPlayer");
            multiplayerSetup.GetComponent<MultiplayerSetup>().minPlayers = 2;
        }
    }
}
