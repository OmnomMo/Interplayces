using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Switches between navigatro interface depending on the version that is active

public class MainSceneManager : MonoBehaviour {


    public GameObject navInterface;
    //public GameObject navInterfaceV01;
    //public GameObject navInterfaceV02;

    //public GameObject navInterfaceV03;

    //public int activeInterface;



    private static MainSceneManager instance;
    public static MainSceneManager Instance
    {
        get { return instance; }
    }


    void Awake()
    {
        if (GameState.Instance == null)
        {
 
            Debug.Log("Gamestate not active. Restarting");
            Application.LoadLevel(0);
        }
    }

    // Use this for initialization
    void Start () {

        instance = this;

        if (GameState.Instance == null)
        {
            Debug.Log("Gamestate not active. Restarting");
            Application.LoadLevel(0);
        }
        else
        {


            navInterface.SetActive(false);


            //  DontDestroyOnLoad(gameObject);
            if (GameState.Instance.isPlayerNavigator())
            {
                navInterface.SetActive(true);

            }
        }

    }

    public void ActivateGameInterface()
    {
        if (GameState.Instance.isPlayerNavigator())
        {

            navInterface.SetActive(true);
        }
    }

    public void DeactivateGameInterface()
    {

        //SpaceshipGameplay.Instance.gameObject.SetActive(false);

        if (GameState.Instance.isPlayerNavigator())
        {
            navInterface.SetActive(false);
        }
    }
    
	
	// Update is called once per frame
	void Update () {
		
	}
}
