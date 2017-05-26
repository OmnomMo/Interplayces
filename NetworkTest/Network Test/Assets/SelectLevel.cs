using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SelectLevel : NetworkBehaviour {

    public string level1Name;
    public string level2Name;
    public string level3Name;
    public string level4Name;
    public string level5Name;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EnterLevel(int nLevel)
    {
        if (GameState.Instance.isPlayerCaptain())
        {
            Debug.Log("Selecting Level " + nLevel);
            //NetworkActions.Instance.CmdEnterLevel(nLevel);


            //Sends info to Hololens if connected
            if (GameState.Instance.holoLensConnected)
            {

                Message m = new Message();
                m.commandID = (int)NetworkCommands.CmdSelectLevel;
                string[] nLevelString = new string[1];
                nLevelString[0] = nLevel.ToString();
                m.parameters = nLevelString;
                TCPSocketServer.Instance.Send(m);

                Message m2 = new Message();
                m2.commandID = (int)NetworkCommands.CmdSceneToGame;
                TCPSocketServer.Instance.Send(m2);
            }


            switch (nLevel) {
                case 1: MultiplayerSetup.Instance.ServerChangeScene(level1Name);
                    break;
                case 2:
                    MultiplayerSetup.Instance.ServerChangeScene(level2Name);
                    break;
                case 3:
                    MultiplayerSetup.Instance.ServerChangeScene(level3Name);
                    break;
                case 4:
                    MultiplayerSetup.Instance.ServerChangeScene(level4Name);
                    break;
                case 5:
                    MultiplayerSetup.Instance.ServerChangeScene(level5Name);
                    break;
                default:
                    break;
            }


           // MultiplayerSetup.Instance.ServerChangeScene("04_EndScene");
        }

    }

    //[ClientRpc]
    //public void RpcEnterLevel()
    //{
     
    //}
}
