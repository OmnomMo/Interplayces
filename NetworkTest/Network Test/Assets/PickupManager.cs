using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PickupManager : NetworkBehaviour {

    public List<EnergyPickup> energyPickups;



    private static PickupManager instance;
    public static PickupManager Instance { get { return instance; } }

    private void Awake()
    {
        energyPickups = new List<EnergyPickup>();
        instance = this;
    }

    public override string ToString()
    {
        string list = "";

        foreach (EnergyPickup ep in energyPickups)
        {
            list += ep.ToString() + "\n";
        }

        return list;
    }

    public int registerPickup(EnergyPickup pickup)
    {
        energyPickups.Add(pickup);
        //Debug.Log(ToString());
        return energyPickups.IndexOf(pickup);
    }


    public void PickupEnergy (EnergyPickup p)
    {

        
        NetworkActions.Instance.CmdPickupEnergy(p.id);
    }

    [ClientRpc]
    public void RpcDestroyPickup (int n)
    {
       // Debug.Log(n);
        energyPickups[n].pickedUp = true;
        energyPickups[n].gameObject.GetComponentInChildren<MeshRenderer>(true).enabled = false;

        Component halo = energyPickups[n].transform.GetChild(0).GetComponent("Halo");
        halo.GetType().GetProperty("enabled").SetValue(halo, false, null);

    }

    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(ToString());
    }
}
