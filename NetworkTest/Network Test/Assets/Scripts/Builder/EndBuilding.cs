using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EndBuilding : NetworkBehaviour {

    private static EndBuilding instance;

    public static EndBuilding Instance { get { return instance; } }


    // Use this for initialization
    void Start () {
        if (Instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

	}

    [ClientRpc]
    public void RpcEndPhase()
    {


        if (NetworkActions.Instance.logActions)
        {
            Debug.Log(NetworkActions.Instance.nLocalActionsTaken++ + ". Enter end phase.");
        }


        PuzzlePartPositions.Instance.SavePartPositions(); 

        SpaceShipPlans.Instance.ClearAllParts();


        //Get List of Ship parts on grid;
        LinkedList<GameObject> shipParts = PlayingGrid.Instance.GetAllParts();

        var currentNode = shipParts.First;

        //Iterate over list and add to plan;
        while ((currentNode != null))
        {
            GameObject part = currentNode.Value;
            ShipPart sPart = null;

            //Gets all Components of type shippart
            Component[] partComponents = part.GetComponentsInChildren(typeof(ShipPart));

            for (int i = 0; i < partComponents.Length; i++)
            {
                if (partComponents[i] is ShipPart)
                {
                    sPart = partComponents[i] as ShipPart;
                }
            }

            //Debug.Log(sPart);

            SpaceShipPlans.Instance.AddPart(sPart.getID(), sPart.GetPosX(), sPart.GetPosY(), sPart.IsEnabled());
            currentNode = currentNode.Next;
        }

        // Debug.Log(SpaceShipPlans.Instance.ToString());


        MultiplayerSetup.Instance.ServerChangeScene("03_Level_Select");
        //GameObject.Find("MultiplayerSetup").GetComponent<NetworkLobbyManager>().ServerChangeScene("03_Main");
        // UnityEngine.SceneManagement.SceneManager.LoadScene(2);


        //if hololens is connected, send end scene command to hololens

        if (GameState.Instance.holoLensConnected)
        {
            Debug.Log("Try to send Message (End Building)");
            Message m = new Message();
            m.parameters = new string[0];
            m.commandID = (int)NetworkCommands.CmdSceneToLevelSelect;
            Debug.Log(TCPSocketServer.Instance.Send(m));
        }
    }

    

    public void EndPhase()
    {
        NetworkActions.Instance.CmdEndPhase();
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}


}
