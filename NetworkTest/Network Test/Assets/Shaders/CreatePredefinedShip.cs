using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CreatePredefinedShip : NetworkActions {


    public GameObject[,] parts;
    public GameObject spaceshipContainer;
    public GameObject colorTracker;
    public GameObject playingField;

    public GameObject batteryPrefab;
    public GameObject scannerPrefab;
    public GameObject shieldPrefab;
    public GameObject thrusterPrefab;
    public GameObject noPartPrefab;

    public bool shipVersion1;


    private static CreatePredefinedShip instance;

    public static CreatePredefinedShip Instance { get { return instance; } }


    int nRows;
    int nCols;


    void Awake()
    {
        instance = this;

        nRows = colorTracker.GetComponent<ColorPickerNew>().nCols;
        nCols = colorTracker.GetComponent<ColorPickerNew>().nRows;

    }

    // Use this for initialization
    void Start () {
        parts = new GameObject[nCols, nRows];

        CreateContainers();

        if (shipVersion1)
        {
            //Debug.Break();
            StartCoroutine(PresetStarship());


        }


    }

    public IEnumerator PresetStarship()
    {
        yield return null;
        NetworkActions.Instance.CmdPreSetPartTypes(2, 3, 0);
        NetworkActions.Instance.CmdPreSetPartTypes(2, 4, 0);
        NetworkActions.Instance.CmdPreSetPartTypes(1, 5, 3);
        NetworkActions.Instance.CmdPreSetPartTypes(3, 5, 3);
        NetworkActions.Instance.CmdPreSetPartTypes(1, 3, 2);
        NetworkActions.Instance.CmdPreSetPartTypes(3, 3, 2);
        NetworkActions.Instance.CmdPreSetPartTypes(1, 2, 1);
        NetworkActions.Instance.CmdPreSetPartTypes(3, 2, 1);
    }


    public void CreateContainers()
    {

        Vector3 newPos = new Vector3();


        for (int x = 0; x < nCols; x++)
        {
            for (int y = 0; y < nRows; y++)
            {
                GameObject newPart = GameObject.Instantiate(spaceshipContainer);
                newPart.transform.parent = this.transform;



                newPos.x = x * 1.1f;
                newPos.y = y * 1.1f;
                newPart.transform.localPosition = newPos;
                parts[x, y] = newPart;
                newPart.GetComponentInChildren<ShipPart>().SetPosX(x);//colorTracker.GetComponent<ColorPickerNew>().nCols - 1 - x);
                newPart.GetComponentInChildren<ShipPart>().SetPosY(y);

            }
        }
    }

    //public void CreateContainers()
    //{

    //    Vector3 newPos = new Vector3();


    //    for (int x = 0; x < colorTracker.GetComponent<ColorPickerNew>().nCols; x++)
    //    {
    //        for (int y = 0; y < colorTracker.GetComponent<ColorPickerNew>().nRows; y++)
    //        {
    //            GameObject newPart = GameObject.Instantiate(spaceshipContainer);
    //            newPart.transform.parent = this.transform;



    //            newPos.x = x * 1.1f;
    //            newPos.y = y * -1.1f;
    //            newPart.transform.localPosition = newPos;
    //            parts[x, y] = newPart;
    //            newPart.GetComponentInChildren<ShipPart>().SetPosX(x); // colorTracker.GetComponent<ColorPickerNew>().nCols - 1 - x);
    //            newPart.GetComponentInChildren<ShipPart>().SetPosY(colorTracker.GetComponent<ColorPickerNew>().nRows - 1 - y);

    //            newPart.SetActive(true);


    //            //if (GameState.Instance.isPlayerCaptain())
    //            //{
    //            //    NetworkActions.Instance.CmdPreSetPartTypes(x, y, 4);
    //            //}

    //        }
    //    }
    //}

    [ClientRpc]
    internal void RpcSetPT(int x, int y, int newID)
    {
        //GameObject newPart = IDToPart(newID);
        //GameObject oldPart = parts[x, y].transform.GetChild(0).gameObject;

        ////Debug.Log(x + "/" + y + ": Teil + " + newPart.GetComponent<ShipPart>().getID() + " ersetzt Teil " + oldPart.GetComponent<ShipPart>().getID() +" at " + oldPart.GetComponent<ShipPart>().GetPosX()+ "/" + oldPart.GetComponent<ShipPart>().GetPosY());

        ////if part changed from last frame, create new Part
        ////if (newPart.GetComponent<ShipPart>().getID() != oldPart.GetComponent<ShipPart>().getID())
        //{

        //    playingField.GetComponent<PlayingGrid>().RemovePiece(oldPart);

        //    GameObject.Destroy(oldPart);

        //    //if (parts[x,y].transform.childCount != 0)
        //    //{
        //    //    GameObject.Destroy(parts[x, y].transform.GetChild(0));
        //    //    Debug.Log("Error, container " + x + "/" + y + "still has children!");
        //    //}



        //    playingField.GetComponent<PlayingGrid>().AddPiece(newPart, x, y);
        //    newPart.transform.parent = parts[x, y].transform;
        //    newPart.transform.localPosition = Vector3.zero;
        //    newPart.GetComponentInChildren<ShipPart>().SetPosX(x);// colorTracker.GetComponent<ColorPickerNew>().nCols - 1 - x);
        //    newPart.GetComponentInChildren<ShipPart>().SetPosY(colorTracker.GetComponent<ColorPickerNew>().nRows - 1 - y);

        GameObject newPart = IDToPart(newID);
        GameObject oldPart = parts[x, y].transform.GetChild(0).gameObject;


        playingField.GetComponent<PlayingGrid>().RemovePiece(oldPart);

        playingField.GetComponent<PlayingGrid>().AddPiece(newPart, x, y);
        newPart.transform.parent = parts[x, y].transform;
        newPart.transform.localPosition = Vector3.zero;
        newPart.transform.GetChild(0).localEulerAngles = Vector3.zero;
        newPart.transform.GetChild(0).localScale = new Vector3(11f, 11f, 11f);
        newPart.GetComponentInChildren<ShipPart>().SetPosX(x);//colorTracker.GetComponent<ColorPickerNew>().nCols - 1 - x);
        newPart.GetComponentInChildren<ShipPart>().SetPosY(y);// colorTracker.GetComponent<ColorPickerNew>().nRows - 1 - y);

        //Debug.Log("SwitchPart (" + x + "/" + y + ") from " + oldPart.GetComponent<ShipPart>().getID() + "to" + newID);
        
        //else
        //{
        //    GameObject.Destroy(newPart);
        //}
    }

    // Update is called once per frame
    void Update () {
		
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

}
