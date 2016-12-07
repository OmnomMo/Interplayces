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
    public void CmdSetThrust(int energy)
    {
        Camera.main.GetComponent<References>().spaceship.GetComponent<SpaceshipMovement>().RpcSetThrust(energy);
    }
}
