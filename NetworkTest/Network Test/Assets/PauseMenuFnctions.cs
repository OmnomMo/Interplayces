using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuFnctions : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RestartGame()
    {

        NetworkActions.Instance.CmdRestartGame();

        //GameObject.Destroy(Score.Instance.gameObject);

        //MultiplayerSetup.Instance.ServerChangeScene("SpaceShipEditor_Tracking");
    }

    public void RestartLevel()
    {
        NetworkActions.Instance.CmdRestartLevel();
    }
}
