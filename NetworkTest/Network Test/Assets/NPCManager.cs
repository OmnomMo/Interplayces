using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NPCManager : NetworkBehaviour {


    private static NPCManager instance;
    public static NPCManager Instance
    {
        get { return instance; }
    }


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Error when creating NPCManager Instance");
        }
    }

    // Use this for initialization
    void Start () {
		
	}

    [ClientRpc]
    public void RpcCatchNPC(int id)
    {
        GameObject[] allNPCs = GameObject.FindGameObjectsWithTag("AlienShip");

        if (allNPCs.Length != 0 )
        {
            foreach (GameObject ship in allNPCs)
            {
                if (ship.GetComponent<NPC_RandomMovement>().NPC_ID == id)
                {
                    ship.GetComponent<NPC_RandomMovement>().isCaught = true;
                    break;
                }
            }
        } else
        {
            Debug.Log("no NPC spaceship found");
        }

        Debug.Log("No NPC ship with id " + id + "found");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
