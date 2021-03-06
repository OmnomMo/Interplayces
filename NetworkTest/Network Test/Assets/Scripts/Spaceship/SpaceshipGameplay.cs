﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SpaceshipGameplay : NetworkBehaviour {

    //hitpoint and max hitpoints of spaceship

    [SyncVar]
    public int hitPoints;
    public int maxHitpoints;

    //shield capacity (calculated sum of all capacities of all active shield parts)
    [SyncVar]
    public float shieldCapacity;
    public float shield;

    //Max energy (energy of all batteries)
    public float energy;
    public float energyCapacity;

    //Power assigned to all systems by navigator
    public float thrustPower;
    public float shieldPower;
    public float scanPower;

    public float scanEnergyDrain;

    public float shieldRechargeRate;
    public float shieldEnergyDrain;
    public Material shieldMaterial;
    public GameObject shieldObject;

    [SyncVar]
    public float shieldCooldown;
    [SyncVar]
    public bool shieldOnCooldown;
    [SyncVar]
    public float shieldCooldownStartTime;

    public Image hitpointsDisplay;
    public Image energyDisplay;

    public float timeLastSent;

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

    // Update is called once per frame
    void Update()
    {

        if (shieldOnCooldown)
        {
            if (Time.time - shieldCooldownStartTime > shieldCooldown)
            {
                shieldOnCooldown = false;
            }
        }

        if (energy <= 0)
        {
            ToEndScreen.Instance.EndZeroEnergy();
        }


        if (GameState.Instance.holoLensConnected)
        {

            if (Time.time - timeLastSent > 0.29f)
            {
                timeLastSent = Time.time;

                Message m = new Message();

                string[] energyValue = new string[1];
                energyValue[0] = energy.ToString();

                m.commandID = (int)NetworkCommands.CmdSetEnergyValue;
                m.parameters = energyValue;
                TCPSocketServer.Instance.Send(m);
            }


        }



        RechargeShield();
        UpdateShieldOpacity();

        ScanPower();

        if (GameState.Instance.isPlayerCaptain())
        {
            DrainEnergy(shieldPower * shieldEnergyDrain * SpaceshipParts.Instance.allShields.Length * Time.deltaTime);
        }

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

    [ClientRpc] 
    internal void RpcSetEnergy(int n)
    {
        energy = n;
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
        if (!shieldOnCooldown)
        {
            if (shield < shieldCapacity * shieldPower)
            {
                // if (DrainEnergy(shieldEnergyDrain * SpaceshipParts.Instance.allShields.Length))
                {
                    shield += shieldRechargeRate * SpaceshipParts.Instance.allShields.Length;


                }
            }
            if (shield > shieldCapacity * shieldPower)
            {
                shield = shieldCapacity * shieldPower;
            }
        }
    }



    public void DealShieldDamage(float d)
    {
        NetworkActions.Instance.CmdDealShieldDamage(d);
    }

    [ClientRpc]
    public void RpcDealShieldDamage(float damage)
    {

        if (!shieldOnCooldown)
        {
           // Debug.Log("Deal " + (int)damage + "shield damage!");

            shield -= (int)damage;

            if (shield <= 0)
            {
                Debug.Log("SetShieldCooldown");
                shieldOnCooldown = true;
                shieldCooldownStartTime = Time.time;
                DealHullDamage(shield * -1);
                shield = 0;
            }
        } else
        {
            DealHullDamage(damage);
        }
    }

    public void RechargeEnergy(float amount)
    {
        NetworkActions.Instance.CmdRechargeEnergy(amount);
    }

    [ClientRpc]
    public void RpcRechargeEnergy(float amount)
    {

        energy += amount;

        if (energy > energyCapacity)
        {
            energy = energyCapacity;
            //Debug.Log("Fully recharged!");
        }

        //Debug.Log("Recharge " + (int)amount + " energy!");
    }

    public void UpdateShieldOpacity()
    {
        shieldMaterial.color = new Color(shieldMaterial.color.r, shieldMaterial.color.g, shieldMaterial.color.b, (float) (0.5 * (shield / shieldCapacity)));
        if (shield <= 0.01f)
        {
            shieldObject.GetComponent<CapsuleCollider>().enabled = false;
        } else
        {
            shieldObject.GetComponent<CapsuleCollider>().enabled = true;
        }
    }

    public void DealHullDamage(float d)
    {

        if (GameState.Instance.holoLensConnected)
        {

   

                Message m = new Message();

                string[] healthValue = new string[1];
                healthValue[0] = hitPoints.ToString();

                m.commandID = (int)NetworkCommands.CmdSetHealthValue;
                m.parameters = healthValue;
                TCPSocketServer.Instance.Send(m);
            
        }



        hitPoints -= (int)d;

        if (hitPoints <= 0)
        {
            hitPoints = 0;


            Debug.Log("Zero Hitpoints remaining. To End screen.");
           ToEndScreen.Instance.EndZeroHP();
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


    public void ScanPower()
    {

        if (GameState.Instance.isPlayerNavigator())
        {
            if (DrainEnergy(scanEnergyDrain * scanPower * SpaceshipParts.Instance.allScanners.Length * Time.deltaTime))
            {
                Camera.main.GetComponent<FollowSpaceship>().camHeight = 200 + Mathf.Pow((500 * scanPower * SpaceshipParts.Instance.allScanners.Length), 1.25f);
            }
            else
            {
                Camera.main.GetComponent<FollowSpaceship>().camHeight = 200;
            }
        } else
        {
            Camera.main.GetComponent<FollowSpaceship>().camHeight = 120;
        }
    }
}
