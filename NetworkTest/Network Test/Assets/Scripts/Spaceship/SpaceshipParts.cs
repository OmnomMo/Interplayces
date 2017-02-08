using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipParts : MonoBehaviour {


    public GameObject[] partPrefabs;
    public GameObject partsParent;

    public GameObject[,] parts;

    private static SpaceshipParts instance;
    public static SpaceshipParts Instance
    {
        get { return instance; }
    }


    //public void CreateParts()
    //{
    //    for (int x = 0; x < parts.GetLength(0); x++)
    //    {
    //        for (int y = 0; y < parts.GetLength(1); y++)
    //        {
    //            GameObject.Instantiate(parts[x, y]);
    //        }
    //    }
    //}

    public GameObject AddPart (int id, int x, int y)
    {

        if (id - 1 > partPrefabs.Length || id < 0)
        {
            Debug.Log("No Part with that ID found.");
            return null;
        }
        else 
        {
            if (x - 1 > parts.GetLength(0) || x < 0 || y - 1 > parts.GetLength(1) || y < 0) {

                Debug.Log("Part out of field");
                return null;
            } else
            {
                GameObject newPart = GameObject.Instantiate(partPrefabs[id]);
                newPart.GetComponent<ShipPart>().SetID(id);
                newPart.GetComponent<ShipPart>().SetPosX(x);
                newPart.GetComponent<ShipPart>().SetPosY(y);

                newPart.transform.parent = partsParent.transform;

                newPart.transform.localPosition = new Vector3(x, 0, y);
                newPart.transform.localRotation = Quaternion.identity;

                parts[x, y] = newPart;




                return newPart;
            }
        }
        
    }


    public void CenterPivot()
    {

        Vector2 center = new Vector2(0,0);
        int nParts = 0;

        for (int x = 0; x < parts.GetLength(0); x++)
        {
            for (int y = 0; y < parts.GetLength(1); y++)
            {
             //   Debug.Log("Iteration: " + x + "/" + y);
                if (parts[x, y] != null)
                {

                   // Debug.Log(" center = " + parts[x, y].transform.localPosition.x + "/" + parts[x, y].transform.localPosition.z);
                    nParts++;
                    center.x = center.x + parts[x, y].transform.localPosition.x;
                    center.y = center.y + parts[x, y].transform.localPosition.z;
                   // Debug.Log("nParts=" + nParts + " center = " + center.x + "/" + center.y);
                }
            }
        }

        center.x = center.x / nParts;
        center.y = center.y / nParts;

        if (nParts > 0)
        {
            partsParent.transform.localPosition = new Vector3(-1 * center.x, 0, -1 * center.y);
        }
    }

    // Use this for initialization
    void Start () {
        parts = new GameObject[5,7];
        instance = this;
        SpaceShipPlans.Instance.BuildSpaceShip();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
