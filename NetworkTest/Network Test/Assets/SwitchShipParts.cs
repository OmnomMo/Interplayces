using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchShipParts : MonoBehaviour {

    public GameObject[,] parts;
    public GameObject spaceshipContainer;
    public GameObject colorTracker;
    public GameObject playingField;

    public GameObject batteryPrefab;
    public GameObject scannerPrefab;
    public GameObject shieldPrefab;
    public GameObject thrusterPrefab;
    public GameObject noPartPrefab;

	// Use this for initialization
	void Start () {
        parts = new GameObject[colorTracker.GetComponent<ColorPickerNew>().nCols, colorTracker.GetComponent<ColorPickerNew>().nRows];

        CreateContainers();	
	}
	
	// Update is called once per frame
	void Update () {

        SetPartTypes();

	}

    public void CreateContainers()
    {

        Vector3 newPos = new Vector3();

        for (int x = 0; x < colorTracker.GetComponent<ColorPickerNew>().nCols; x++)
        {
            for (int y = 0; y < colorTracker.GetComponent<ColorPickerNew>().nRows; y++)
            {
                GameObject newPart = GameObject.Instantiate(spaceshipContainer);
                newPart.transform.parent = this.transform;

                newPos.x = x * 1.1f;
                newPos.y = y * -1.1f;
                newPart.transform.localPosition = newPos;
                parts[x, y] = newPart;
                newPart.GetComponentInChildren<ShipPart>().SetPosX(colorTracker.GetComponent<ColorPickerNew>().nCols -1 - x);
                newPart.GetComponentInChildren<ShipPart>().SetPosY(colorTracker.GetComponent<ColorPickerNew>().nRows -1 -y);

            }
        }
    }


    public void SetPartTypes()
    {
        for (int x = 0; x < colorTracker.GetComponent<ColorPickerNew>().nCols; x++)
        {
            for (int y = 0; y < colorTracker.GetComponent<ColorPickerNew>().nRows; y++)
            {
                GameObject newPart = ColorToPart(colorTracker.GetComponent<ColorPickerNew>().averageColors[x, y]);
                GameObject oldPart = parts[x, y].transform.GetChild(0).gameObject;

                //if part changed from last frame, create new Part
                if (newPart.GetComponent<ShipPart>().getID() != oldPart.GetComponent<ShipPart>().getID())
                {

                    playingField.GetComponent<PlayingGrid>().RemovePiece(oldPart);
                    
                    GameObject.Destroy(oldPart);


                    playingField.GetComponent<PlayingGrid>().AddPiece(newPart, x, y);
                    newPart.transform.parent = parts[x, y].transform;
                    newPart.transform.localPosition = Vector3.zero;
                    newPart.GetComponentInChildren<ShipPart>().SetPosX(colorTracker.GetComponent<ColorPickerNew>().nCols - 1 - x);
                    newPart.GetComponentInChildren<ShipPart>().SetPosY(colorTracker.GetComponent<ColorPickerNew>().nRows - 1 - y);
                } else
                {
                    GameObject.Destroy(newPart);
                }

            }
        }

    }

    public GameObject ColorToPart(Color c)
    {

        if (c.r + c.g + c.b > 0.5)
        {
            //red
            if (c.r > c.g * 1.2 && c.r > c.b * 1.2)
            {
                return GameObject.Instantiate(thrusterPrefab);
            }

            //green
            if (c.g > c.r /** 1.2*/ && c.g > c.b /** 1.2*/)
            {
                return GameObject.Instantiate(batteryPrefab);
            }

            //blue
            if (c.b > c.g * 1.2 && c.b > c.r * 1.2)
            {
                return GameObject.Instantiate(shieldPrefab);
            }

            //yellow
            if (c.r > c.b && c.g > c.b)
            {
                return GameObject.Instantiate(scannerPrefab);
            }

        }

        return GameObject.Instantiate(noPartPrefab);


        // compareParts


        //remove old part, store x, y



    }
}
