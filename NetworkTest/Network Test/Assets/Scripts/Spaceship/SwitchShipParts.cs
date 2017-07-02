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

    public GameObject[,] shipContainers;

    int checkX;
    int checkY;

    public int switchedshipParts;
    public bool testingSetup;
    public bool trackColor;

    public bool lightBackground; 


    int nRows;
    int nCols;



    private static SwitchShipParts instance;

    public static SwitchShipParts Instance { get { return instance; } }

    private void Awake()
    {
        
        //Debug.Log("Set instance of SwitchParts");

        nRows = colorTracker.GetComponent<ColorPickerNew>().nCols;
        nCols = colorTracker.GetComponent<ColorPickerNew>().nRows;

        if (SwitchShipParts.Instance != null)
        {
           // Debug.Log("SwitchParts not null!");
            GameObject.Destroy(this.gameObject);

        } else
        {
            instance = this;
        }

        parts = new GameObject[nCols, nRows];

        CreateContainers();

    }

    // Use this for initialization
    void Start () {

      
	}
	
	// Update is called once per frame
	void Update () {

        // NetworkActions.Instance.CmdCreateContainers();
        if (trackColor)
        {
            SetPartTypes();
        }
       // CheckAllPartTypes();

        // Debug.Log()
        
    }

    public void CreateContainers()
    {

        Vector3 newPos = new Vector3();

        shipContainers = new GameObject[nCols , nRows];


        for (int x = 0; x < nCols; x++)
        {
            for (int y = 0; y < nRows; y++)
            {
                GameObject newPart = GameObject.Instantiate(spaceshipContainer);
                newPart.transform.parent = this.transform;

                shipContainers[x, y] = newPart;

                newPos.x = x * 1.1f;
                newPos.y = y * 1.1f;
                newPart.transform.localPosition = newPos;
                parts[x, y] = newPart;
                newPart.GetComponentInChildren<ShipPart>().SetPosX(x);//colorTracker.GetComponent<ColorPickerNew>().nCols - 1 - x);
                newPart.GetComponentInChildren<ShipPart>().SetPosY(y);

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

    //Periodically updates all parts to mitigate errors
    public void CheckAllPartTypes()
    {
        if (GameState.Instance.isPlayerCaptain())
        {

            //update Part on x/y;
            NetworkActions.Instance.CmdSetPartTypes(checkX, checkY, parts[checkX, checkY].transform.GetChild(0).GetComponent<ShipPart>().getID());


            //increase x, if x is max, increase y
            if (++checkX >= nCols)
            {
                checkX = 0;
                if (++checkY >= nRows)
                {
                    checkY = 0;
                }
            }

        }
    }

    public void SetPartTypes()
    {

        if (GameState.Instance.isPlayerCaptain())
        {

            if (colorTracker.GetComponent<ColorPickerNew>() != null)
            {



                for (int x = 0; x < nCols; x++)
                {
                    for (int y = 0; y < nRows; y++)
                    {
                        

                        //Here x and y have to be swapped, because rect coordinates flip y and the recorded texture is in wide format
                        //Get average color inside a specified rect from the camera texture

                       
                           int  newID = ColorToID(colorTracker.GetComponent<ColorPickerNew>().averageColors[((nRows - 1) - y), x]);
                        


                        
                        

                        GameObject oldPart = parts[nCols - 1 -x,y].transform.GetChild(0).gameObject;


                       // If the color inside the rect changed, instantiate new ship part of new type.

                        if (newID != oldPart.GetComponent<ShipPart>().getID())
                        {
                            NetworkActions.Instance.CmdSetPartTypes(nCols - 1 - x, y, newID);
                            

                            //Debug.Log("SwitchPart (" + x + "/" + y +  ") from " + oldPart.GetComponent<ShipPart>().getID() + "to" + newID);

                            

                   
                        }

                  


                    }
                }
            }
            else
            {
                Debug.Log("Webcam not found. Please connect webcam and resrtart game");
            }

            //StartCoroutine(TestClass.DelayedBreak());
        }

    }



    [ClientRpc]
    internal void RpcSetPT(int x, int y, int newID)
    {
        GameObject newPart = IDToPart(newID);
        GameObject oldPart = parts[x, y].transform.GetChild(0).gameObject;

        var oldPartType = oldPart.GetComponent<ShipPart>().GetType();

       // if (newPart.GetComponent<ShipPart>() is typeof(oldPart.GetComponent<ShipPart>().GetType()))

        playingField.GetComponent<PlayingGrid>().RemovePiece(oldPart);

        playingField.GetComponent<PlayingGrid>().AddPiece(newPart, x, y);
        newPart.transform.parent = parts[x, y].transform;
        newPart.transform.localPosition = Vector3.zero;
        newPart.GetComponentInChildren<ShipPart>().SetPosX(x);//colorTracker.GetComponent<ColorPickerNew>().nCols - 1 - x);
        newPart.GetComponentInChildren<ShipPart>().SetPosY(y);// colorTracker.GetComponent<ColorPickerNew>().nRows - 1 - y);

        GameObject.Destroy(oldPart);


    }


    public GameObject IDToPart(int id)
    {
        switch (id)
        {
            case 0:
                return GameObject.Instantiate(batteryPrefab);
                break;
            case 1:
                return GameObject.Instantiate(thrusterPrefab);
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

        //Color codes are stored in settings.txt, if not found, use default values

        float absoluteFactorThreshold = Dweiss.Settings.Instance.absoluteFactorThreshold;
        float specificFactorthreshold = Dweiss.Settings.Instance.specificFactorThreshold;

        if (testingSetup)
        {

            bool basicColorDIfference = c.r / c.b > absoluteFactorThreshold || c.r / c.g > absoluteFactorThreshold ||
                       c.g / c.b > absoluteFactorThreshold || c.g / c.r > absoluteFactorThreshold ||
                       c.b / c.r > absoluteFactorThreshold || c.b / c.g > absoluteFactorThreshold;

            if (!basicColorDIfference)
            {
                return 4;
            }


            if (lightBackground)
            {
                if ((c.r + c.g + c.b > 1.7f)) {

                    bool significantColorDIfference = c.r / c.b > specificFactorthreshold || c.r / c.g > specificFactorthreshold ||
                        c.g / c.b > specificFactorthreshold || c.g / c.r > specificFactorthreshold ||
                        c.b / c.r > specificFactorthreshold || c.b / c.g > specificFactorthreshold;


                    if (!significantColorDIfference)
                    {
                        return 4;
                    }
                }
            } else
            {

                if ((c.r + c.g + c.b < 1.4f))
                {
                    return 4;
                }
            }


            if (c.r > 0.7f)
            {
                if (c.b > c.g * 1.15f || c.b - c.g > 0.1f)
                {
                    //magenta
                    return 3;
                } else
                {
                    //Red or orange
                    return 1;
                }
            } else
            {
                if (c.b > 1.1f*c.g)
                {
                    //blue

                    if (c.b > c.r * 1.3 && c.b > 0.6f)
                    {
                        return 2;
                    } else { return 4; }
                } else
                {
                    if (c.g > c.r * 1f && c.g > c.b * 1f)
                    {
                        //green
                        return 0;
                    } else
                    {
                        return 4;
                    }
                }
            }
        }
        else
        {

            if (GameState.Instance.settingsFound)
            {

                if (c.r + c.g + c.b > 0.5)
                {


                    //magenta
                    if (c.r > c.g * Dweiss.Settings.Instance.Magenta_RgtG && c.b > c.g * Dweiss.Settings.Instance.Magenta_BgtG && c.r > Dweiss.Settings.Instance.Magenta_Rgt && c.b > Dweiss.Settings.Instance.Magenta_Bgt)
                    //if (c.r > c.b * 1.2f && c.g > c.b * 1.2f && c.b < 0.5)
                    {
                        return 3;
                    }

                    //red
                    if (c.r > c.g * Dweiss.Settings.Instance.Red_RgtG && c.r > c.b * Dweiss.Settings.Instance.Red_RgtB && c.r > Dweiss.Settings.Instance.Red_Rgt)
                    {
                        return 1;
                    }

                    //green
                    if (c.g > c.r * Dweiss.Settings.Instance.Green_GgtR && c.g > c.b * Dweiss.Settings.Instance.Green_GgtB && c.r < Dweiss.Settings.Instance.Green_Rst && c.g < Dweiss.Settings.Instance.Green_Gst && c.g > Dweiss.Settings.Instance.Green_Gbt)
                    {
                        return 0;
                    }

                    //blue
                    if (c.b > c.g * Dweiss.Settings.Instance.Blue_BgtG && c.b > c.r * Dweiss.Settings.Instance.Blue_BgtR && c.b > Dweiss.Settings.Instance.Blue_Bgt)
                    {
                        return 2;
                    }

                    //yellow


                }
            }
            else
            {

                // old variant with fixed values
                if (c.r + c.g + c.b > 0.5)
                {



                    if (c.r > c.g * 1.2f && c.b > c.g && c.r > 0.4 && c.b > 0.35)
                    //if (c.r > c.b * 1.2f && c.g > c.b * 1.2f && c.b < 0.5)
                    {
                        return 3;
                    }

                    //red
                    if (c.r > c.g * 1.2 && c.r > c.b * 1.2)
                    {
                        return 1;
                    }

                    //green
                    if (c.g > c.r * 1.1 && c.g > c.b * 1.1 && c.r < 0.38 && c.g < 0.48)
                    {
                        return 0;
                    }

                    //blue
                    if (c.b > c.g * 1.2 && c.b > c.r * 1.2)
                    {
                        return 2;
                    }

                    //yellow


                }
            }

            return 4;
        }
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
