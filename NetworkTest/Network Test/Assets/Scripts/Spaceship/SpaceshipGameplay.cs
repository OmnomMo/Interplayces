using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpaceshipGameplay : NetworkBehaviour {


    public int hitPoints;
    public int maxHitpoints;
    public float shieldCapacity;
    public float shield;
    public float energy;
    public float energyCapacity;

    public float thrustPower;
    public float shieldPower;
    public float scanPower;

    public float scanEnergyDrain;

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


    [ClientRpc]
    internal void RpcSetThrust(float energy)
    {
        thrustPower = energy;
    }

    [ClientRpc]
    internal void RpcSetShield(float energy)
    {
        shieldPower = energy;
    }

    [ClientRpc]
    internal void RpcSetScan(float energy)
    {
        scanPower = energy;
    }

    [ClientRpc]
    internal void RpcDrainPower(float amount)
    {
        energy -= amount;

        if (energy < 0)
        {
            energy = 0;
        }

        UpdateBatteries();
    }


    public bool DrainEnergy(float nEnergy)
    {
        if (nEnergy > energy)
        {
            return false;
        }
        else
        {
            NetworkActions.Instance.CmdDrainPower(nEnergy);
            return true;
        }
    }
	
    public void RechargeShield()
    {
        if (shield < shieldCapacity * shieldPower)
        {
            if (DrainEnergy(shieldEnergyDrain * SpaceshipParts.Instance.allShields.Length))
            {
                shield += shieldRechargeRate * SpaceshipParts.Instance.allShields.Length;

             
            }
        }
        if (shield > shieldCapacity * shieldPower)
        {
            shield = shieldCapacity * shieldPower;
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

        ScanPower();
        
	}

    public void ScanPower()
    {

        if (GameState.Instance.isPlayerNavigator())
        {
            if (DrainEnergy(scanEnergyDrain * scanPower * SpaceshipParts.Instance.allScanners.Length))
            {
                Camera.main.GetComponent<FollowSpaceship>().camHeight = 100 + Mathf.Pow((150 * scanPower * SpaceshipParts.Instance.allScanners.Length), 1.25f);
            }
            else
            {
                Camera.main.GetComponent<FollowSpaceship>().camHeight = 100;
            }
        }
    }
}
