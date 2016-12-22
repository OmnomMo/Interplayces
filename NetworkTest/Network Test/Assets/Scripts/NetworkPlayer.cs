using UnityEngine;
using System.Collections;

public class NetworkPlayer : MonoBehaviour {

    public enum PlayerTypes { Captain, Navigator, NavigatorAR, None}

    PlayerTypes playerType;


    public Canvas dialogue;
    private static NetworkPlayer instance;
    public static NetworkPlayer Instance { get { return instance;} }

	// Use this for initialization
	void Start () {

        instance = this;
        playerType = PlayerTypes.None;
        
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(playerType + " is captain: " + (playerType == PlayerTypes.Captain) + " IsCaptain: " + isPlayerCaptain());
	}

    public PlayerTypes getPlayerType()
    {
        return playerType;
    }

    public void setPlayerCaptain()
    {


        //Debug.Log("Set Player Captain");
        playerType = PlayerTypes.Captain;
    }

    public bool isPlayerCaptain()
    {
        return (playerType == PlayerTypes.Captain);
    }

    public void setPlayerNavigator()
    {

       // Debug.Log("Set Player Navigator");
        playerType = PlayerTypes.Navigator;
        Camera.main.gameObject.GetComponent<CameraBehaviour>().ChangeCameraToNavigator();

        Camera.main.GetComponent<References>().navigatorInterface.SetActive(true);
        Camera.main.GetComponent<References>().energyBar.GetComponent<EnergyBar>().Initialize();

        //transform.Find("Interface Navigator").gameObject.SetActive(true);
        //GameObject.Find("EnergyBar").GetComponent<EnergyBar>().Initialize();
    }

    public bool isPlayerNavigator()
    {
        return (playerType == PlayerTypes.Navigator);
    }

    public void setPlayerNavigatorAR()
    {
        playerType = PlayerTypes.NavigatorAR;
    }

    public bool isPlayerNavigatorAR()
    {
        return (playerType == PlayerTypes.NavigatorAR);
    }

    public void removeCanvas()
    {
        dialogue.gameObject.SetActive(false);
    }
}
