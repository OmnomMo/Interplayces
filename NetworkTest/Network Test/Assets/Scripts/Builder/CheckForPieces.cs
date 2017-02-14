using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckForPieces : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayingGrid.Instance.HasPieces())
        {
            GetComponent<Button>().interactable = true;
        } else
        {
            GetComponent<Button>().interactable = false;
        }
	}
}
