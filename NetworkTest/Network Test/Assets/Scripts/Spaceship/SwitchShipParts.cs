using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SwitchShipParts : NetworkBehaviour {

    public GameObject[,] parts;
    public GameObject spaceshipContainer;
    public GameObject colorTracker;
    public GameObject playingField;

    public GameObject batteryPrefab;
    public GameObject scannerPrefab;
    public GameObject shieldPrefab;
    public GameObject thrusterPrefab;
    public GameObject noPartPrefab;



    private static SwitchShipParts instance;

    public static SwitchShipParts Instance { get { return instance; } }

    private void Awake()
    {
        
        Debug.Log("Set instance of SwitchParts");

        if (SwitchShipParts.Instance != null)
        {
            Debug.Log("SwitchParts not null!");
            GameObject.Destroy(this.gameObject);

        } else
        {
            instance = this;
        }

        parts = new GameObject[colorTracker.GetComponent<ColorPickerNew>().nCols, colorTracker.GetComponent<ColorPickerNew>().nRows];

        CreateContainers();

    }

    // Use this for initialization
    void Start () {

      
	}
	
	// Update is called once per frame
	void Update () {

       // NetworkActions.Instance.CmdCreateContainers();
        
        SetPartTypes();

        // Debug.Log()
        
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
                newPart.GetComponentInChildren<ShipPart>().SetPosX(x);//colorTracker.GetComponent<ColorPickerNew>().nCols - 1 - x);
                newPart.GetComponentInChildren<ShipPart>().SetPosY(colorTracker.GetComponent<ColorPickerNew>().nRows - 1 - y);

            }
        }
    }

    [ClientRpc]
    internal void RpcDebug()
    {
        Debug.Log("text");
    }

    public static void StaticDebug()
    {
        Debug.Log("debug");
    }

    [ClientRpc]
    internal void RpcCreateContainers()
    {
       


    } 


    public void SetPartTypes()
    {

        if (GameState.Instance.isPlayerCaptain())
        {

            if (colorTracker.GetComponent<ColorPickerNew>() != null)
            {

                for (int x = 0; x < colorTracker.GetComponent<ColorPickerNew>().nCols; x++)
                {
                    for (int y = 0; y < colorTracker.GetComponent<ColorPickerNew>().nRows; y++)
                    {

                        //Debug.Log("1:" + (colorTracker.GetComponent<ColorPickerNew>() != null));
                        //Debug.Log("2:" + (parts != null));
                        //Debug.Log("3:" + (playingField.GetComponent<PlayingGrid>() !=  null));

                        // Debug.Log(colorTracker.GetComponent<ColorPickerNew>().averageColors[x, y]);

                        int newID = ColorToID(colorTracker.GetComponent<ColorPickerNew>().averageColors[x, y]);

                        NetworkActions.Instance.CmdSetPartTypes(x, y, newID);


                    }
                }
            }
        }

    }

    [ClientRpc]
    internal void RpcSetPT(int x, int y, int newID)
    {
        GameObject newPart = IDToPart(newID);
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
        }
        else
        {
            GameObject.Destroy(newPart);
        }
    }


    public GameObject IDToPart(int id)
    {
        switch (id)
        {
            case 0:
                return GameObject.Instantiate(thrusterPrefab);
                break;
            case 1:
                return GameObject.Instantiate(batteryPrefab);
                break;
            case 2:
                return GameObject.Instantiate(shieldPrefab);
                break;
            case 3:
                return GameObject.Instantiate(scannerPrefab);
                break;
            case 4:
                return GameObject.Instantiate(noPartPrefab);
                break;
            default:
                return GameObject.Instantiate(noPartPrefab);
                break;
        }
    }

    public int ColorToID(Color c)
    {
        if (c.r + c.g + c.b > 0.5)
        {
            //red
            if (c.r > c.g * 1.2 && c.r > c.b * 1.2)
            {
                return 0;
            }

            //green
            if (c.g > c.r /** 1.2*/ && c.g > c.b /** 1.2*/)
            {
                return 1;
            }

            //blue
            if (c.b > c.g * 1.2 && c.b > c.r * 1.2)
            {
                return 2;
            }

            //yellow
            if (c.r > c.b && c.g > c.b)
            {
                return 3;
            }

        }

        return 4;
    }

    //public GameObject ColorToPart(Color c)
    //{

    //    if (c.r + c.g + c.b > 0.5)
    //    {
    //        //red
    //        if (c.r > c.g * 1.2 && c.r > c.b * 1.2)
    //        {
    //            return GameObject.Instantiate(thrusterPrefab);
    //        }

    //        //green
    //        if (c.g > c.r /** 1.2*/ && c.g > c.b /** 1.2*/)
    //        {
    //            return GameObject.Instantiate(batteryPrefab);
    //        }

    //        //blue
    //        if (c.b > c.g * 1.2 && c.b > c.r * 1.2)
    //        {
    //            return GameObject.Instantiate(shieldPrefab);
    //        }

    //        //yellow
    //        if (c.r > c.b && c.g > c.b)
    //        {
    //            return GameObject.Instantiate(scannerPrefab);
    //        }

    //    }

    //    return GameObject.Instantiate(noPartPrefab);


        // compareParts


        //remove old part, store x, y



    //}
}
