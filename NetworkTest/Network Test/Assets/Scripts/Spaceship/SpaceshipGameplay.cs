using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipGameplay : MonoBehaviour {


    public int hitPoints;
    public float energy;
    public float energyCapacity;

    private static SpaceshipGameplay instance;
    public static SpaceshipGameplay Instance
    {
        get { return instance; }
    }


    // public GameObject[] parts;

    // Use this for initialization
    void Awake () {
        instance = this;
	}

    public void DrainEnergy(float nEnergy)
    {
        energy -= nEnergy;
        
        if (energy < 0)
        {
            energy = 0;
        }

        UpdateBatteries();
    }
	
    public void UpdateBatteries()
    {
        GameObject[] allBatteries = SpaceshipParts.Instance.allBatteries;

        foreach (GameObject b in allBatteries)
        {
            if (b.GetComponent<SpaceshipBatteryPart>() != null)
            {
                b.GetComponent<SpaceshipBatteryPart>().energy = energy / allBatteries.Length;
            }
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
