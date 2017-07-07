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

    //Network-commands 


    //Scene Management
    //---------------------------------------------------------------

    [Command]
    public void CmdRestartGame()
    {

     NetworkPlayer.Instance.RpcRestartGame();

  
    }


    [Command]
    public void CmdReturnToLevelSelect()
    {
        NetworkPlayer.Instance.RpcReturnToLevelSelect();
    }

    [Command]
    public void CmdPauseGame()
    {
        NetworkPlayer.Instance.RpcPauseGame();
    }

    [Command] 
    public void CmdUnPauseGame()
    {
        NetworkPlayer.Instance.RpcUnpauseGame();
    }

    public void CmdRestartLevel()
    {
        NetworkPlayer.Instance.RpcRestartLevel();
    }

    public void CmdShowEndMessage(int message)
    {
        NetworkPlayer.Instance.RpcShowEndMessage(message);
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
    //---------------------------------------------------------------

    //Ship Controls
    //---------------------------------------------------------------
    [Command]
    public void CmdSetThrust(float energy)
    {
        if (SpaceshipGameplay.Instance != null)
        {
            SpaceshipGameplay.Instance.RpcSetThrust(energy);
        }
    }

    [Command]
    public void CmdSetShield(float energy)
    {
        if (SpaceshipGameplay.Instance != null)
        {
            SpaceshipGameplay.Instance.RpcSetShield(energy);
        }
    }

    [Command]
    public void CmdDealShieldDamage(float damage)
    {
        if (SpaceshipGameplay.Instance != null)
        {
            SpaceshipGameplay.Instance.RpcDealShieldDamage(damage);
        }
    }

    [Command]
    public void CmdRechargeEnergy(float amount)
    {
        if (SpaceshipGameplay.Instance != null)
        {
            SpaceshipGameplay.Instance.RpcRechargeEnergy(amount);
        }
    }

    [Command]
    public void CmdSetScan(float energy)
    {
        if (SpaceshipGameplay.Instance != null)
        {
            SpaceshipGameplay.Instance.RpcSetScan(energy);
        }
    }

    [Command]
    public void CmdDrainPower(float amount)
    {
        if (SpaceshipGameplay.Instance != null)
        {
            SpaceshipGameplay.Instance.RpcDrainPower(amount);
        }
    }

    

    [Command]
    public void CmdPickupEnergy(int n)
    {
        PickupManager.Instance.RpcDestroyPickup(n);
    }


    [Command]
    public void CmdPickupResource(int n)
    {
        //Debug.Log(n);
        PickupManager.Instance.RpcDestroyResourcePickup(n);
    }

    [Command]
    public void CmdSetEnergy(int n)
    {
        if (SpaceshipGameplay.Instance != null)
        {
            SpaceshipGameplay.Instance.RpcSetEnergy(n);
        }
    }
    //---------------------------------------------------------------

    //Ship Building
    //---------------------------------------------------------------

    [Command]
    public void CmdDragSphere(GameObject sphere)
    {
        //Debug.Log("Drag!");
        //Debug.Log("Start Dragging " + sphere.ToString());
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
    public void CmdSetPartTypes(int x, int y, int newID)
    {
       // Debug.Log("SetPartType " + x + y + newID);
        SwitchShipParts.Instance.RpcSetPT(x, y, newID);
    }

    [Command]
    public void CmdPreSetPartTypes(int x, int y, int newID)
    {
        CreatePredefinedShip.Instance.RpcSetPT(x, y, newID);
        Debug.Log("Set Preset Part");
    }

    //---------------------------------------------------------------

    //Navigation
    //---------------------------------------------------------------

    [Command]
    public void CmdHighlightPlanet (int nPlanet)
    {
        PlanetNavigation.Instance.RpcSetActivePlanet(nPlanet);

    }

    [Command]
    public void CmdStopPlanetHighlight(int nPlanet)
    {

        PlanetNavigation.Instance.RpcUnsetActivePlanet(nPlanet);
    }

    [Command]
    public void CmdHighlightMousePosition(float posX, float posY, float posZ)
    {
        FreeNavigation.Instance.RpcSetTargetPoint(posX, posY, posZ);

    }



  
}
