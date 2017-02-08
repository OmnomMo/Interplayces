using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipBatteryPart : MonoBehaviour, ShipPart {

    public float energy { get; set; }

    public int ID;
    public int posX;
    public int posY;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
