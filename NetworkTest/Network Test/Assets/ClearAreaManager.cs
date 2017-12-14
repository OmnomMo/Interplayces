using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ClearAreaManager : NetworkBehaviour {

    public GameObject[] clearAreas;

	// Use this for initialization
	void Start () {
		
	}

    [ClientRpc]
    public void RpcClearArea(int n)
    {
        clearAreas[n].GetComponent<ClearArea>().clear = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
