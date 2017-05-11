using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyPLayerAutoReady : NetworkLobbyPlayer {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnClientEnterLobby()
    {
        base.OnClientEnterLobby();
       
        StartCoroutine(CheckReady());

    }

    public IEnumerator CheckReady()
    {
        yield return null;
       // Debug.Log(playerControllerId);


        if (isLocalPlayer)
        {
           // Debug.Log("I entered a Lobby!");
            readyToBegin = true;

            if ((MultiplayerSetup.Instance.isCaptainReady && MultiplayerSetup.Instance.isNavigatorReady) || MultiplayerSetup.Instance.singlePlayerAble)
            {
                  SendReadyToBeginMessage();
            }
        }
    }
}
