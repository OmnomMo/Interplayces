using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipThrusterPart : MonoBehaviour, ShipPart {

    public float maxPower;
    public bool isFiring;

    public int ID;
    public int posX;
    public int posY;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetID(int newID)
    {
        ID = newID;
    }

    public int getID()
    {
        return ID;
    }

    public void SetPosX(int newPosX)
    {
        posX = newPosX;
    }

    public int getPosX()
    {
        return posX;
    }

    public void SetPosY(int newPosY)
    {
        posY = newPosY;
    }

    public int getPosY()
    {
        return posY;
    }
}
