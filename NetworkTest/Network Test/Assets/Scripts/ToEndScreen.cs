using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ToEndScreen : NetworkBehaviour {

    public GameObject endingMessage;
    public GameObject endingMessageText;


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

            StartCoroutine(DelayedEnterEndScreen());

            NetworkActions.Instance.CmdShowEndMessage((int)reason);

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

    public void showEndMessage(int message)
    {
        endingMessage.SetActive(true);

        reason = (reasonForTermination)message;

        SpaceshipGameplay.Instance.gameObject.GetComponent<SpaceshipMovement>().controllable = false;

        if ((reasonForTermination)message == ToEndScreen.reasonForTermination.hp)
        {
            endingMessageText.GetComponent<Text>().text = "Raumschiff zerstört!";
        }
        else
        {
            if ((reasonForTermination) message == ToEndScreen.reasonForTermination.energy)
            {
                endingMessageText.GetComponent<Text>().text = "Keine Energie mehr!";
            }
            else
            {
                if ((reasonForTermination) message == ToEndScreen.reasonForTermination.player)
                {
                    endingMessageText.GetComponent<Text>().text = "Die Expedition wurde abgebrochen.";
                }
                else
                {
                    if ((reasonForTermination) message == ToEndScreen.reasonForTermination.win)
                    {
                        endingMessageText.GetComponent<Text>().text = "Geschafft!";
                    }
                    else
                    {
                        endingMessageText.GetComponent<Text>().text = "Ende.";
                    }

                }
            }
        }
    }

    public IEnumerator DelayedEnterEndScreen()
    {
        yield return new WaitForSeconds(5.0f);
        Debug.Log("Initializing EnterEndScreen");
        NetworkActions.Instance.CmdEnterEndScreen();


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
