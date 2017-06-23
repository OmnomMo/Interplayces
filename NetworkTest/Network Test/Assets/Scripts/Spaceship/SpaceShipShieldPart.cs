using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipShieldPart : MonoBehaviour, ShipPart {

    public float shieldCapacity;

    public int ID;
    public int posX;
    public int posY;
    public bool partEnabled;

    // Use this for initialization
    void Start () {
        partEnabled = true;
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

    public void SetEnabled(bool enabled)
    {
        partEnabled = enabled;
    }

    public bool IsEnabled()
    {
        return partEnabled;
    }
}
