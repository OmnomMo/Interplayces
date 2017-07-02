using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupObjective : Objective {

    public int nRequired;

    public int nCurrent;

    //public ResourcePickup[] pickups;


	// Use this for initialization
	void Start () {
        nCurrent = 0;
        base.Start();
        descriptionObject.GetComponent<Text>().text = description + " (" + nCurrent + "/" + nRequired + ")";
    }

    new public bool Complete()
    {

        Debug.Log("Trying to complete pickup objective");

        descriptionObject.GetComponent<Text>().text = description + " (" + nCurrent + "/" + nRequired + ")";

        if (nCurrent >= nRequired)
        {
            base.Complete();
            //OnCompletion();
            return true;
        }
        else
        {
            return false;
        }
    }


    new public void OnCompletion()
    {
        base.OnCompletion();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
