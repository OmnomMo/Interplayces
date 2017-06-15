using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Switches between navigatro interface depending on the version that is active

public class SceneManager : MonoBehaviour {

    public GameObject navInterfaceV01;
    public GameObject navInterfaceV02;

    public GameObject navInterfaceV03;

    public int activeInterface;


    private static SceneManager instance;
    public static SceneManager Instance
    {
        get { return instance; }
    }


    void Awake()
    {
        if (GameState.Instance == null)
        {
            instance = this;
            Debug.Log("Gamestate not active. Restarting");
            Application.LoadLevel(0);
        }
    }

    // Use this for initialization
    void Start () {

        if (GameState.Instance == null)
        {
            Debug.Log("Gamestate not active. Restarting");
            Application.LoadLevel(0);
        }
        else
        {


            navInterfaceV01.SetActive(false);
            navInterfaceV02.SetActive(false);
            navInterfaceV03.SetActive(false);


            //  DontDestroyOnLoad(gameObject);
            if (GameState.Instance.isPlayerNavigator())
            {

                switch (activeInterface)
                {
                    case 1: navInterfaceV01.SetActive(true);
                        break;
                    case 2: navInterfaceV02.SetActive(true);
                        break;
                    case 3:
                        navInterfaceV03.SetActive(true);
                        break;
                    default:
                        Debug.Log("ERROR: No interface set to active");
                        break;
                }


              //  Debug.Log("I'm da Navigataa");
                //Camera.main.gameObject.GetComponent<CameraBehaviour>().ChangeCameraToNavigator();

               // Camera.main.GetComponent<References>().navigatorInterface.SetActive(true);
                //Camera.main.GetComponent<References>().energyBar.GetComponent<EnergyBar>().Initialize();
            }
        }

    }

    public void ActivateGameInterface()
    {
        if (GameState.Instance.isPlayerNavigator())
        {
            switch (activeInterface)
            {
                case 1:
                    navInterfaceV01.SetActive(true);
                    break;
                case 2:
                    navInterfaceV02.SetActive(true);
                    break;
                case 3:
                    navInterfaceV03.SetActive(true);
                    break;
                default:
                    Debug.Log("ERROR: No interface set to active");
                    break;
            }

        }
    }

    public void DeactivateGameInterface()
    {

        //SpaceshipGameplay.Instance.gameObject.SetActive(false);

        if (GameState.Instance.isPlayerNavigator())
        {
            switch (activeInterface)
            {
                case 1:
                    navInterfaceV01.SetActive(false);
                    break;
                case 2:
                    if (navInterfaceV02 == null)
                    {
                        navInterfaceV02 = GameObject.Find("Interface Navigator Version_02");
                    }
                    navInterfaceV02.SetActive(false);
                    break;
                case 3:
                    navInterfaceV03.SetActive(false);
                    break;
                default:
                    Debug.Log("ERROR: No interface set to active");
                    break;
            }

        }
    }
    
	
	// Update is called once per frame
	void Update () {
		
	}
}
