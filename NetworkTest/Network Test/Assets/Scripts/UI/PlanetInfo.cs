using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Stores string info about celestial bodies, toggles display of said info.

public class PlanetInfo : MonoBehaviour {


    public string planetName;
    public string planetInfo;
    public Text infoTextObject;
    public Text nameTextObject;
    public Collider sphereOfInfluence;
    public Canvas planetCanvas;

    public bool playerStay;

	// Use this for initialization
	void Start () {
        infoTextObject.text = planetInfo;
        nameTextObject.text = planetName;
	}

    public void ShowInfo()
    {
        if (GameState.Instance.isPlayerCaptain())
        {
            planetCanvas.gameObject.SetActive(true);
        }
    }

    public void HideInfo()
    {
        planetCanvas.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
        

	}
}
