using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipPlans : MonoBehaviour {

    public LinkedList<StoredShipPart> parts;

    private static SpaceShipPlans instance;
    public static SpaceShipPlans Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        instance = this;
        Object.DontDestroyOnLoad(gameObject);
        parts = new LinkedList<StoredShipPart>();
    }


    public void ClearAllParts()
    {
        parts = new LinkedList<StoredShipPart>();
    }

    public void AddPart(int id_, int posX_, int posY_)
    {
        parts.AddLast(new StoredShipPart(id_, posX_, posY_));
    }

    public override string ToString()
    {
        string s = "";

        var currentNode = parts.First;
        int n = 0;

        while ((currentNode != null))
        {
            s += ("Shippart n " + n + ":  ID - " + currentNode.Value.id + "   pos - " + currentNode.Value.posX + "/" + currentNode.Value.posY + "\n");
            n++;
            currentNode = currentNode.Next;
        }
        return s;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void BuildSpaceShip()
    {
       if (SpaceshipParts.Instance != null)
        {
            var currentNode = parts.First;
         

            while ((currentNode != null))
            {
                SpaceshipParts.Instance.AddPart(currentNode.Value.id, currentNode.Value.posX, currentNode.Value.posY);
                   currentNode = currentNode.Next;
            }

            SpaceshipParts.Instance.CenterPivot();

            SpaceshipParts.Instance.OrganizeParts();

            SpaceshipGameplay.Instance.energyCapacity = SpaceshipParts.Instance.getCapacity();
            SpaceshipGameplay.Instance.energy = SpaceshipGameplay.Instance.energyCapacity;


        }
    }
}

public class StoredShipPart
{
    public int id;
    public int posX;
    public int posY;

    public StoredShipPart(int id_, int posX_, int posY_)
    {
        id = id_;
        posX = posX_;
        posY = posY_;
    }
}
