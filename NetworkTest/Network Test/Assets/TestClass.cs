using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClass: MonoBehaviour {

    public GameObject spaceship;
    private static TestClass instance;

    public static TestClass Instance { get { return instance; } }

 
    // Use this for initialization
    void Start () {


        instance = this;


    }

    

    public void SpawnParts()
    {

        spaceship.GetComponent<SpaceshipParts>().AddPart(0, 1, 1);
        spaceship.GetComponent<SpaceshipParts>().AddPart(0, 1, 2);
        spaceship.GetComponent<SpaceshipParts>().AddPart(0, 1, 3);
        spaceship.GetComponent<SpaceshipParts>().AddPart(0, 1, 4);
        spaceship.GetComponent<SpaceshipParts>().AddPart(0, 0, 1);
        spaceship.GetComponent<SpaceshipParts>().AddPart(1, 1, 0);
        spaceship.GetComponent<SpaceshipParts>().AddPart(1, 2, 1);
       // spaceship.GetComponent<SpaceshipParts>().AddPart(0, 1, 2);
        spaceship.GetComponent<SpaceshipParts>().CenterPivot();

    }
	
	// Update is called once per frame
	void Update () {


        //Debug.Log("captain: " + GameState.Instance.isPlayerCaptain() + "\nnavigator: " + GameState.Instance.isPlayerNavigator());
        if (Input.GetButtonDown("DebugButton"))
        {
            Debug.Log("Debug!");
            EndBuilding.Instance.EndPhase();
        }
    }
}
