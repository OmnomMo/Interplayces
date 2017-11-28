using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToLobby : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(ToLobby());
	}

    public  IEnumerator ToLobby()
    {
        yield return new WaitForSeconds(0.2f);
        Application.LoadLevel(1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
