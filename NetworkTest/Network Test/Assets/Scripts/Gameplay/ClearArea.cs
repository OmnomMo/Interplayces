using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearArea : MonoBehaviour {

    public bool clear;
    public int nObjects;
    

	// Use this for initialization
	void Start () {
		
	}

    
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacles_Damage"))
        {
            nObjects++;
            clear = false;
        }
    }


    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacles_Damage"))
        {
            nObjects--;
            if (nObjects <= 0)
            {
                clear = true;
            }
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}
