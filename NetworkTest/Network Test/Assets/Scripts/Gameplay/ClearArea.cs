using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearArea : MonoBehaviour {

    public bool clear;
    public int nObjects;

    public GameObject spaceStation;
    

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
                if (spaceStation != null)
                {
                    spaceStation.GetComponent<PopUp>().StartPopup();
                }
            }
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}
