using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Button function for restarting game

public class RestartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void RestartGame()
    {

        NetworkActions.Instance.CmdRestartGame();

        //GameObject.Destroy(Score.Instance.gameObject);

        //MultiplayerSetup.Instance.ServerChangeScene("SpaceShipEditor_Tracking");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
