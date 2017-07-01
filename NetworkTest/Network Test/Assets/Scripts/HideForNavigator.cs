using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Hides Object only for Navigator (or for capatin if bool is actice)

public class HideForNavigator : MonoBehaviour {


    public bool hideForCaptain;

	// Use this for initialization
	void Start () {

        if (!hideForCaptain)
        {
            if (GameState.Instance != null && GameState.Instance.isPlayerNavigator())
            {
                if (GetComponent<MeshRenderer>() != null)
                {
                    GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }else
        {
            if (GameState.Instance != null && GameState.Instance.isPlayerCaptain())
            {
                if (GetComponent<MeshRenderer>() != null)
                {
                    GetComponent<MeshRenderer>().enabled = false;
                }

                if (GetComponent<Canvas>() != null)
                {
                    GetComponent<Canvas>().enabled = false;
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
