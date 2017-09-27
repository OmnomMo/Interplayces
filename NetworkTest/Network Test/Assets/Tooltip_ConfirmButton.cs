using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip_ConfirmButton : MonoBehaviour {

    public Objective confirmObjective;


	// Use this for initialization
	void Start () {
		
	}
	
    public void Confirm()
    {
        confirmObjective.Complete();
    }


	// Update is called once per frame
	void Update () {
		
	}
}
