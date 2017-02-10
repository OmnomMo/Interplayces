using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipGameplay : MonoBehaviour {


    public int hitPoints;
    public int maxHitpoints;
    public float shieldCapacity;
    public float shield;
    public float energy;
    public float energyCapacity;



    public float shieldRechargeRate;
    public float shieldEnergyDrain;
    public Material shieldMaterial;
    

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

    public bool DrainEnergy(float nEnergy)
    {
        if (nEnergy > energy)
        {
            return false;
        }
        else
        {

            energy -= nEnergy;

            if (energy < 0)
            {
                energy = 0;
            }

            UpdateBatteries();
            return true;
        }
    }
	
    public void RechargeShield()
    {
        if (shield < shieldCapacity)
        {
            if (DrainEnergy(shieldEnergyDrain * SpaceshipParts.Instance.allShields.Length))
            {
                shield += shieldRechargeRate * SpaceshipParts.Instance.allShields.Length;

                if (shield > shieldCapacity)
                {
                    shield = shieldCapacity;
                }
            }
        }
    }

    public void DealShieldDamage(float d)
    {
        shield -= (int)d;

        if (shield <= 0)
        {
            DealHullDamage(shield * -1);
            shield = 0;
        }
    }

    public void UpdateShieldOpacity()
    {
        shieldMaterial.color = new Color(shieldMaterial.color.r, shieldMaterial.color.g, shieldMaterial.color.b, (float) (0.5 * (shield / shieldCapacity)));
    }

    public void DealHullDamage(float d)
    {
        hitPoints -= (int)d;

        if (hitPoints <= 0)
        {
            hitPoints = 0;
        }
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
        RechargeShield();
        UpdateShieldOpacity();
	}
}
