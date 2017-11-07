using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerSwitch : MonoBehaviour {


    GameObject multiplayerSetup;

	// Use this for initialization
	void Start () {
        multiplayerSetup = GameObject.Find("MultiplayerSetup");
        MultiplayerSetup.Instance.minPlayers = 2;

        ToggleSinglePlayer(GetComponent<Toggle>().isOn);
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
            GameState.Instance.singlePlayer = true;
        }
        else
        {
            Debug.Log("SwitchMultiPlayer");
            multiplayerSetup.GetComponent<MultiplayerSetup>().minPlayers = 2;
            GameState.Instance.singlePlayer = false;
        }
    }
}
