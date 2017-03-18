using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideForNavigator : MonoBehaviour {


    public bool hideForCaptain;

	// Use this for initialization
	void Start () {

        if (!hideForCaptain)
        {
            if (GameState.Instance != null && GameState.Instance.isPlayerNavigator())
            {
                GetComponent<MeshRenderer>().enabled = false;
            }
        }else
        {
            if (GameState.Instance != null && GameState.Instance.isPlayerCaptain())
            {
                GetComponent<MeshRenderer>().enabled = false;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
