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

        StartCoroutine(DelayedToggle(MultiplayerSetup.Instance.singlePlayerAble));
    }

    public IEnumerator DelayedToggle(bool toggleState)
    {
        yield return null;
        ToggleSinglePlayer(toggleState);

    }
	
	// Update is called once per frame
	void Update () {
    
    }

    public void ToggleSinglePlayer(bool single)
    {
        if (single)
        {
            //Debug.Log("SwitchSinglePlayer");
            multiplayerSetup.GetComponent<MultiplayerSetup>().minPlayers = 1;
            GameState.Instance.singlePlayer = true;
        }
        else
        {
            //Debug.Log("SwitchMultiPlayer");
            multiplayerSetup.GetComponent<MultiplayerSetup>().minPlayers = 2;
            GameState.Instance.singlePlayer = false;
        }
    }
}
