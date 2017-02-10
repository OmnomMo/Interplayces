using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipParts : MonoBehaviour {


    public GameObject[] partPrefabs;
    public GameObject partsParent;
    public GameObject[] allBatteries;
    public GameObject[] allThrusters;
    public GameObject[] allShields;

    public GameObject[,] parts;

    public GameObject shieldObject;

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

    public GameObject[] GetActiveParts()
    {
        //Debug.Log("GetActiveSpaceshipparts!");

        int nParts = 0;

        for (int x = 0; x < parts.GetLength(0); x++)
        {
            for (int y = 0; y < parts.GetLength(1); y++)
            {
                if (parts[x, y] != null)
                {
                    nParts++;
                }
            }
        }

        if (nParts > 0)
        {
            GameObject[] activeParts = new GameObject[nParts];
            int i = 0;

            for (int x = 0; x < parts.GetLength(0); x++)
            {
                for (int y = 0; y < parts.GetLength(1); y++)
                {
                    if (parts[x, y] != null)
                    {
                        activeParts[i++] = parts[x, y];
                    }
                }
            }
            return activeParts;
        }
        else { return null; }
    }

    public GameObject[] GetThrusters()
    {
        GameObject[] allParts = GetActiveParts();

        Debug.Log("N Parts: " + allParts.Length);

        int nThrusters = 0;

        foreach (GameObject part in allParts)
        {
            if (part.GetComponent<SpaceShipPart_Thruster>() != null)
            {
                nThrusters++;
            }
        }

        GameObject[] thrusters = new GameObject[nThrusters];
        int i = 0;

        foreach (GameObject part in allParts)
        {
            if (part.GetComponent<SpaceShipPart_Thruster>() != null)
            {
                thrusters[i++] = part;
            }
        }

        return thrusters;

    }

    public GameObject[] GetBatteries()
    {
        GameObject[] allParts = GetActiveParts();

        //Debug.Log("N Parts: " + allParts.Length);

        int nBatteries = 0;

        foreach (GameObject part in allParts)
        {
            if (part.GetComponent<SpaceshipBatteryPart>() != null)
            {
                nBatteries++;
            }
        }

        GameObject[] batteries = new GameObject[nBatteries];
        int i = 0;

        foreach (GameObject part in allParts)
        {
            if (part.GetComponent<SpaceshipBatteryPart>() != null)
            {
                batteries[i++] = part;
            }
        }

        return batteries;

    }


    public GameObject[] GetShields()
    {
        GameObject[] allParts = GetActiveParts();

        //Debug.Log("N Parts: " + allParts.Length);

        int nShields = 0;

        foreach (GameObject part in allParts)
        {
            if (part.GetComponent<SpaceShipShieldPart>() != null)
            {
                nShields++;
            }
        }

        GameObject[] shields = new GameObject[nShields];
        int i = 0;

        foreach (GameObject part in allParts)
        {
            if (part.GetComponent<SpaceShipShieldPart>() != null)
            {
                shields[i++] = part;
            }
        }

        return shields;

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
        SpaceShipPlans.Instance.BuildSpaceShip();
        Debug.Log("BuildSpaceship!");

        
      
    }

    public void OrganizeParts()
    {
        allBatteries = GetBatteries();
        allThrusters = GetThrusters();
        allShields = GetShields();
    }

    public float getEnergy()
    {
        float energy = 0;

        foreach (GameObject b in allBatteries)
        {
            energy += GetComponent<SpaceshipBatteryPart>().energy;
        }

        return energy;
    }

    public float getBatteryCapacity()
    {
        float capacity = 0;

        foreach (GameObject b in allBatteries)
        {
            capacity += b.GetComponent<SpaceshipBatteryPart>().capacity;
        }

        return capacity;
    }

    public float getShieldCapacity()
    {
        float capacity = 0;

        foreach (GameObject b in allShields)
        {
            capacity += b.GetComponent<SpaceShipShieldPart>().shieldCapacity;
        }

        return capacity;
    }

    public float getShipRadius()
    {
        GameObject[] parts = GetActiveParts();

        float d = 0;


        //get part furthest from center
        foreach (GameObject p in parts)
        {

            //Pythagoras. offset through parent (partsParent) has to be aknowledge
            float dNew = (Mathf.Pow(p.transform.localPosition.x + p.transform.parent.localPosition.x, 2) + Mathf.Pow(p.transform.localPosition.y + p.transform.parent.localPosition.y, 2));

            //Debug.Log(dNew + ": " + (p.transform.localPosition.x + p.transform.parent.localPosition.x) + "/" + (p.transform.localPosition.y + p.transform.parent.localPosition.y));

            if (dNew > d)
            {
                d = dNew;
            }
        }

        return Mathf.Sqrt(d);
    }

   

    void Awake()
    {
        parts = new GameObject[5, 7];
        instance = this;
     
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
