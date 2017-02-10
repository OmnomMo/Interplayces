using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipPart_Thruster : MonoBehaviour, ShipPart {

    public float maxPower;
    public bool isFiring;
    public GameObject fireEffect;

    public int ID;
    public int posX;
    public int posY;

    // Use this for initialization
    void Start () {
        fireEffect.SetActive(isFiring);
	}

    public void Fire()
    {
        if (!isFiring)
        {
            isFiring = true;
            fireEffect.SetActive(true);
        }
    }

    public void StopFire()
    {
        if (isFiring)
        {
            isFiring = false;
            fireEffect.SetActive(false);
        }
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

    public int GetPosX()
    {
        return posX;
    }

    public void SetPosY(int newPosY)
    {
        posY = newPosY;
    }

    public int GetPosY()
    {
        return posY;
    }
}
