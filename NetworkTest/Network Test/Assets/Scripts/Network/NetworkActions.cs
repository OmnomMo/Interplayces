using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkActions : NetworkBehaviour {




    private static NetworkActions instance;

    public static NetworkActions Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {

        

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
    public void CmdEnterEndScreen()
    {
        ToEndScreen.Instance.RpcEnterEndScreen();
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
    public void CmdDealShieldDamage(float damage)
    {
        SpaceshipGameplay.Instance.RpcDealShieldDamage(damage);
    }

    [Command]
    public void CmdRechargeEnergy(float amount)
    {
        SpaceshipGameplay.Instance.RpcRechargeEnergy(amount);
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

    [Command]
    public void CmdRestartGame()
    {

        GameObject.Destroy(Score.Instance.gameObject);

        MultiplayerSetup.Instance.ServerChangeScene("SpaceShipEditor_Tracking");
    }

    //[Command]
    //public void CmdCreateContainers()
    //{

    //    if (SwitchShipParts.Instance != null)
    //    {
    //        SwitchShipParts.Instance.RpcDebug();
    //    }
    //}

    [Command]
    public void CmdSetPartTypes(int x, int y, int newID)
    {
        SwitchShipParts.Instance.RpcSetPT(x, y, newID);
    }

    [Command]
    public void CmdPreSetPartTypes(int x, int y, int newID)
    {
        CreatePredefinedShip.Instance.RpcSetPT(x, y, newID);
    }

    [Command]
    public void CmdPickupEnergy (int n)
    {
        PickupManager.Instance.RpcDestroyPickup(n);
    }
}
