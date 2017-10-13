using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipPart_Thruster : MonoBehaviour, ShipPart {

    public float maxPower;
    public bool isFiring;
    public bool partEnabled;
    public GameObject fireEffect;


    Gradient normalGrad;
    Gradient boostGrad;

    public int ID;
    public int posX;
    public int posY;

    // Use this for initialization
    void Start () {
        //fireEffect.SetActive(isFiring);
        fireEffect.GetComponent<ParticleSystem>().emissionRate = 0;

        normalGrad = new Gradient();
        normalGrad.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(new Color(0.03f, 0.2f, 0.2f), 0.25f), new GradientColorKey(new Color(0.25f, 0.1f, 0.1f), 0.4f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 0.5f), new GradientAlphaKey(0.0f, 1.0f) });


        boostGrad = new Gradient();
        boostGrad.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(new Color(0.03f, 0.2f, 0.2f), 0.25f), new GradientColorKey(new Color(0.03f, 0.2f, 0.2f), 0.4f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 0.5f), new GradientAlphaKey(0.0f, 1.0f) });

    }

    public void Fire()
    {
        if (!isFiring)
        {
            isFiring = true;

            fireEffect.GetComponent<ParticleSystem>().emissionRate = 300;
            //fireEffect.SetActive(true);
        }
    }

    public void StopFire()
    {
        if (isFiring)
        {
            isFiring = false;
            fireEffect.GetComponent<ParticleSystem>().emissionRate = 0;
            //fireEffect.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (isFiring)
        {

           

            if (SpaceshipGameplay.Instance.gameObject.GetComponent<SpaceshipMovement>().boostMultiplier == 1)
            {
                fireEffect.GetComponent<ParticleSystem>().startSize = 15 * SpaceshipGameplay.Instance.thrustPower;

                ParticleSystem ps = fireEffect.GetComponent<ParticleSystem>();
                var col = ps.colorOverLifetime;
               
                col.color = normalGrad;



            }
            else
            {


                ParticleSystem ps = fireEffect.GetComponent<ParticleSystem>();
                var col = ps.colorOverLifetime;


                col.color = boostGrad;



                fireEffect.GetComponent<ParticleSystem>().startSize = 30 * SpaceshipGameplay.Instance.thrustPower;

            }
        }
    }

    public void SetID(int newID)
    {
        ID = newID;
    }

    public int getID()
    {
        return ID;
    }

    public void SetPosX(int newPosX)
    {
        posX = newPosX;
    }

    public int GetPosX()
    {
        return posX;
    }

    public void SetPosY(int newPosY)
    {
        posY = newPosY;
    }

    public int GetPosY()
    {
        return posY;
    }

    public void SetEnabled(bool enabled)
    {
        partEnabled = enabled;
    }

    public bool IsEnabled()
    {
        return partEnabled;
    }

    public bool RemoveFromGrid()
    {


        return PlayingGrid.Instance.RemovePiece(this.gameObject);
        
    }
}
