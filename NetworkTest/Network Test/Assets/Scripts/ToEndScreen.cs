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


    public enum reasonForTermination { hp, energy, player}
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
        reason = reasonForTermination.hp;

        EnterEndScreen();
    }

    public void EndZeroEnergy()
    {
        reason = reasonForTermination.energy;

        EnterEndScreen();
    }

    public void EndPlayerAction()
    {
        reason = reasonForTermination.player;

        EnterEndScreen();
    }


    private void EnterEndScreen()
    {
        if (GameState.Instance.isPlayerCaptain())
        {
            Debug.Log("Initializing EnterEndScreen");
            NetworkActions.Instance.CmdEnterEndScreen();
        }
      
    }

    [ClientRpc] 
    public void RpcEnterEndScreen() {
        if (!hasEnded)
        {
            hasEnded = true;
            MultiplayerSetup.Instance.ServerChangeScene("EndScene");
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
