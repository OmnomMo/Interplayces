using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ToEndScreen : NetworkBehaviour {

    private static ToEndScreen instance;
    public static ToEndScreen Instance
    {
        get { return instance; }
    }

    public bool hasEnded;


    public enum reasonForTermination { hp, energy, player, win}
    reasonForTermination reason;

	// Use this for initialization
	void Start () {
        instance = this;
        hasEnded = false;
        
	}

    public reasonForTermination GetReason()
    {
        return reason;
    }

    public void  EndZeroHP()
    {

        Debug.Log("No HP left");
        reason = reasonForTermination.hp;

        EnterEndScreen();
    }

    public void EndZeroEnergy()
    {
        Debug.Log("No Energy Left");
        reason = reasonForTermination.energy;

        EnterEndScreen();
    }

    public void EndPlayerAction()
    {
        Debug.Log("Player Ended Game");
        reason = reasonForTermination.player;

        EnterEndScreen();
    }

    public void EndWin()
    {
        Debug.Log("Player won level");
        reason = reasonForTermination.win;
        EnterEndScreen();


    }


    private void EnterEndScreen()
    {
        if (GameState.Instance.isPlayerCaptain())
        {
            Debug.Log("Initializing EnterEndScreen");
            NetworkActions.Instance.CmdEnterEndScreen();


            //Sends info to Hololens if connected
            if (GameState.Instance.holoLensConnected)
            {

                Message m = new Message();
                m.commandID = (int)NetworkCommands.CmdSceneToEnd;
                string[] parameters = new string[0];

                m.parameters = parameters;
                TCPSocketServer.Instance.Send(m);


            }
        }
      
    }

    [ClientRpc] 
    public void RpcEnterEndScreen() {
        if (!hasEnded)
        {
            hasEnded = true;
            MultiplayerSetup.Instance.ServerChangeScene("04_EndScene");
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
