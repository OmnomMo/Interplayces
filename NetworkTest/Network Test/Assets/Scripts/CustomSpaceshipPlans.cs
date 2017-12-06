using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSpaceshipPlans : MonoBehaviour {

    public StoredShipPart[] parts;

    public bool buildCustomSpaceship;


    void Awake()
    {


        
    }

    public void BuildSpaceShip()
    {
        if (SpaceshipParts.Instance != null)
        {


            foreach (StoredShipPart sp in parts)
            {
                SpaceshipParts.Instance.AddPart(sp.id, sp.posX, sp.posY, sp.partEnabled);
            }

            //var currentNode = parts.First;


            //while ((currentNode != null))
            //{

            //    SpaceshipParts.Instance.AddPart(currentNode.Value.id, currentNode.Value.posX, currentNode.Value.posY, currentNode.Value.partEnabled);
            //    currentNode = currentNode.Next;
            //}

            SpaceshipParts.Instance.CenterPivot();

            SpaceshipParts.Instance.OrganizeParts();


            SpaceshipGameplay.Instance.energyCapacity = SpaceshipParts.Instance.getBatteryCapacity();
            SpaceshipGameplay.Instance.energy = SpaceshipGameplay.Instance.energyCapacity;

            SpaceshipGameplay.Instance.shieldCapacity = SpaceshipParts.Instance.getShieldCapacity();
            SpaceshipGameplay.Instance.shield = SpaceshipGameplay.Instance.shieldCapacity;

            SpaceshipGameplay.Instance.hitPoints = SpaceshipGameplay.Instance.maxHitpoints;


            SpaceshipParts.Instance.weight = SpaceshipParts.Instance.allBatteries.Length + SpaceshipParts.Instance.allScanners.Length + SpaceshipParts.Instance.allThrusters.Length + SpaceshipParts.Instance.allShields.Length;
            SpaceshipParts.Instance.GetComponent<Rigidbody>().mass = SpaceshipParts.Instance.weight;

            float shieldScale = SpaceshipParts.Instance.getShipRadius() * 2 + 3;
            SpaceshipParts.Instance.shieldObject.transform.localScale = new Vector3(shieldScale, shieldScale, shieldScale);


        }
    }


// Use this for initialization
void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
