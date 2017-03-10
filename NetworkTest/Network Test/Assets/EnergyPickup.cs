using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnergyPickup : NetworkBehaviour {

    public float energyAmount;
    public bool pickedUp;

    [SyncVar]
    public int id;

	// Use this for initialization
	void Start () {
        pickedUp = false;
        StartCoroutine(registerPickup());
       // Debug.Log(PickupManager.Instance.energyPickups.Count);
	}

    public IEnumerator registerPickup()
    {
        yield return null;
        id = PickupManager.Instance.registerPickup(this);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override string ToString()
    {
        return "EnergyPickup nr" + id;
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
                    PickupManager.Instance.PickupEnergy(this);
                    //GameObject.Destroy(gameObject);
                    //pickedUp = true;
                }
            }
        }
    }
}
