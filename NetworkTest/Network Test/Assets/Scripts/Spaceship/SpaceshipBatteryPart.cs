using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceshipBatteryPart : MonoBehaviour, ShipPart {

    public float energy;

    public float capacity;

    public GameObject display;

    public int ID;
    public int posX;
    public int posY;

    public bool partEnabled;

    // Use this for initialization
    void Start()
    {
        energy = capacity;
        partEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Display battery power on object
        //float percentage = (energy / capacity) * 100;
        //string percString = percentage.ToString("0.0");
        //display.GetComponent<Text>().text = percString + "%";
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

    public bool RemoveFromGrid()
    {

        Debug.Log("You tried to remove a Part from grid. This is not working atm. Please contact the aothorities.");
        return false;

    }


}
