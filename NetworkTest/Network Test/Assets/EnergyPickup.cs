using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickup : MonoBehaviour {

    public float energyAmount;
    public bool pickedUp;

	// Use this for initialization
	void Start () {
        pickedUp = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if (GameState.Instance.isPlayerCaptain())
        {

            if (other.gameObject.layer == LayerMask.NameToLayer("Player") || GetComponent<Collider>().gameObject.layer == LayerMask.NameToLayer("Shield"))
            {
                if (!pickedUp)
                {
                    SpaceshipGameplay.Instance.RechargeEnergy(energyAmount);
                    GameObject.Destroy(gameObject);
                    pickedUp = true;
                }
            }
        }
    }
}
