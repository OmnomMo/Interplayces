using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlanetNavigation : NetworkBehaviour {

    public GameObject[] planets;
    // [SyncVar]
    [SyncVar] //(hook = "OnChangeActivePlanet")]
    public int activePlanet;
    

    [SyncVar]
    public bool isActive;
    public GameObject pointer;

    private static PlanetNavigation instance;

    public static PlanetNavigation Instance { get { return instance; } }

    // Use this for initialization
    void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

 


    public void RequestHighlight(GameObject planet)
    {

        int selectedPlanet = System.Array.IndexOf(planets, planet);

       // NetworkActions.Instance.CmdHighlightPlanet();
        if (planet != planets[activePlanet])
        {
            //If another planet has been previously active
            if (isActive)
            {
                //remove highlight from previously active planet
                NetworkActions.Instance.CmdStopPlanetHighlight(activePlanet);
            }


            //Call network action to set all planets active
            NetworkActions.Instance.CmdHighlightPlanet(selectedPlanet);
        } else
        {
            //if selected planet is not active
            if (!isActive)
            {
                NetworkActions.Instance.CmdHighlightPlanet(selectedPlanet);
            } else //if selected planet has been active
            {
                NetworkActions.Instance.CmdStopPlanetHighlight(selectedPlanet);
            }
            
        }
    }
    
    //Is called on all clients when active planet is set
    [ClientRpc]
    public void RpcSetActivePlanet(int nPlanet)
    {
        isActive = true;
        activePlanet = nPlanet;

        if (GameState.Instance.isPlayerNavigator())
        {


            planets[activePlanet].GetComponentInChildren<HighlightPlanet>().SetHighlight();
        }

        if (GameState.Instance.isPlayerCaptain())
        {
            pointer.GetComponent<PointToTarget>().setTargetPlanet(planets[activePlanet].transform);
        }
    }

    [ClientRpc]
    public void RpcUnsetActivePlanet(int nPlanet)
    {
        isActive = false;
        if (GameState.Instance.isPlayerNavigator())
        {
            planets[activePlanet].GetComponentInChildren<HighlightPlanet>().UnsetHighlight();
        }
        if (GameState.Instance.isPlayerCaptain())
        {
            pointer.GetComponent<PointToTarget>().unsetTargetPlanet();
        }
    }

}
