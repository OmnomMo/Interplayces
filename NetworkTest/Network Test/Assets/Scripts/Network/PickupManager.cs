using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


//Keeps a list of EnergyPickups. Keeps list consistent in Network.

public class PickupManager : NetworkBehaviour {

    public List<EnergyPickup> energyPickups;

    public List<ResourcePickup> resourcePickups;




    private static PickupManager instance;
    public static PickupManager Instance { get { return instance; } }

    private void Awake()
    {
        energyPickups = new List<EnergyPickup>();
        resourcePickups = new List<ResourcePickup>();
        instance = this;
    }

    public override string ToString()
    {
        string list = "";

        foreach (EnergyPickup ep in energyPickups)
        {
            list += ep.ToString() + "\n";
        }

        list += "ResourcePickups: \n";


        foreach (ResourcePickup rp in resourcePickups)
        {
            list += rp.ToString() + "\n";
        }



        return list;
    }

    public int registerPickup(EnergyPickup pickup)
    {
        energyPickups.Add(pickup);
        //Debug.Log(ToString());
        return energyPickups.IndexOf(pickup);
    }

    public int registerPickup(ResourcePickup pickup)
    {
        resourcePickups.Add(pickup);
        return resourcePickups.IndexOf(pickup);
    }

    public void PickupResource(ResourcePickup p)
    {
        NetworkActions.Instance.CmdPickupResource(p.id);

        Debug.Log("PickUp " + p.id.ToString());

        if (GameState.Instance.holoLensConnected) {

            Message m = new Message();

            string[] ipckupID = new string[1];



            
            ipckupID[0] = p.id.ToString();

            m.commandID = (int)NetworkCommands.CmdPickupResource;
            m.parameters = ipckupID;
            TCPSocketServer.Instance.Send(m);
        }
    }

    

    public void PickupEnergy (EnergyPickup p)
    {

        
        NetworkActions.Instance.CmdPickupEnergy(p.id);

        Debug.Log("PickUp " + p.id.ToString());

        if (GameState.Instance.holoLensConnected)
        {

            Message m = new Message();

            string[] ipckupID = new string[1];




            ipckupID[0] = p.id.ToString();

            m.commandID = (int)NetworkCommands.CmdPickupResource;
            m.parameters = ipckupID;
            TCPSocketServer.Instance.Send(m);
        }
    }

    [ClientRpc]
    public void RpcDestroyPickup (int id)
    {
        // Debug.Log(n);

        EnergyPickup pickup = null;

        foreach (EnergyPickup ep in energyPickups)
        {
            if (ep.id == id)
            {
                pickup = ep;
                break;
            }
        }

        pickup.GetComponent<AudioSource>().Play();

        pickup.pickedUp = true;
        pickup.gameObject.GetComponentInChildren<MeshRenderer>(true).enabled = false;

        Component halo = pickup.transform.GetChild(0).GetComponent("Halo");
        halo.GetType().GetProperty("enabled").SetValue(halo, false, null);

    }

    [ClientRpc]
    public void RpcDestroyResourcePickup(int id)
    {
        ResourcePickup pickup = null;

        foreach (ResourcePickup rp in resourcePickups)
        {
            if (rp.id == id)
            {
                pickup = rp;
                break;
            }
        }
        pickup.GetComponent<AudioSource>().Play();

        pickup.pickedUp = true;
        pickup.gameObject.GetComponentInChildren<MeshRenderer>(true).enabled = false;

        Component halo = pickup.transform.GetChild(0).GetComponent("Halo");
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
