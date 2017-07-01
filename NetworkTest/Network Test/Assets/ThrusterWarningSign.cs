using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterWarningSign : MonoBehaviour {


    bool canFire = true;

	// Use this for initialization
	void Start () {
		
	}


	
	// Update is called once per frame
	void Update () {

        //Check if thruster is placed correctly
        SpaceShipPart_Thruster thruster = transform.parent.GetComponent<SpaceShipPart_Thruster>();

        //if (thruster.posY == 0 || transform.parent.parent.GetComponent<FollowSphere>().isOnField == false)
        //{
        //    canFire = true;
        //} else
        {
            if (PlayingGrid.Instance.IsEmpty(thruster.posX, thruster.posY-1))
            {
                canFire = true;
            } else
            {
                canFire = false;
            }
        }

        if (canFire)
        {
            //if yes, disable warning sign
            GetComponent<Canvas>().enabled = false;
            thruster.partEnabled = true;
        }
        else
        {

            //if no, display warning sign. Disable thruster
            GetComponent<Canvas>().enabled = true;
            thruster.partEnabled = false;
        }
        
        	
	}
}
