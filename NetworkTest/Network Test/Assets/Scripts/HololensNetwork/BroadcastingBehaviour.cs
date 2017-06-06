using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BroadcastingBehaviour : NetworkDiscovery {

	public bool broadcasting = false;

	// Use this for initialization
	public void StartBroadcasting () {

		broadcastData = "1755";
		Initialize ();
		bool ableToBroadcast = StartAsServer ();
		broadcasting = true;
		Debug.Log ("ABLE TO BROADCAST = " + ableToBroadcast);
	}

	public void StopBroadcasting() {
		StopBroadcast();
		broadcasting = false;
		Debug.Log ("Broadcasting stopped");
	}

    void OnApplicationQuit()
    {
        Debug.Log("Quit Application, stop Broadcasting");
        StopBroadcasting();
    }
}
