using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkActions : NetworkBehaviour {




    private static NetworkActions instance;

    public static NetworkActions Instance { get { return instance; } }

    // Use this for initialization
    void Start () {

        instance = this;

	}
	
	// Update is called once per frame
	void Update () {
	
	}


   [Command]
   public void CmdEndPhase()
    { 
        EndBuilding.Instance.RpcEndPhase();
    }
    

    [Command]
    public void CmdHighlightPlanet (int nPlanet)
    {
        PlanetNavigation.Instance.RpcSetActivePlanet(nPlanet);

    }

    [Command]
    public void CmdDragSphere (GameObject sphere)
    {
        //Debug.Log("Drag!");
        sphere.GetComponent<DragAround>().dragged = true;
        //sphere.GetComponent<DragAround>().RpcStartDrag();
    }

    [Command]
    public void CmdStopDragSphere(GameObject sphere)
    {
        sphere.GetComponent<DragAround>().dragged = false;
       // sphere.GetComponent<DragAround>().RpcStopDrag();
    }

    [Command]
    public void CmdStopPlanetHighlight(int nPlanet)
    {
        
        PlanetNavigation.Instance.RpcUnsetActivePlanet(nPlanet);
    }

    [Command]
    public void CmdSetThrust(float energy)
    {
        SpaceshipGameplay.Instance.RpcSetThrust(energy);
    }

    [Command]
    public void CmdSetShield(float energy)
    {
        SpaceshipGameplay.Instance.RpcSetShield(energy);
    }

    [Command]
    public void CmdSetScan(float energy)
    {
        SpaceshipGameplay.Instance.RpcSetScan(energy);
    }

    [Command]
    public void CmdDrainPower(float amount)
    {
        SpaceshipGameplay.Instance.RpcDrainPower(amount);
    }
}
