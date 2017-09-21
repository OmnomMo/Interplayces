using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Stores string info about celestial bodies, toggles display of said info.

public class PlanetInfo : MonoBehaviour {


    public string planetName;
    public string planetInfo;

    public Tooltip planetTooltip;
    public Sprite planetImage;

    public Collider sphereOfInfluence;

    public bool playerStay;

	// Use this for initialization
	void Start () {

	}

    public void ShowInfo()
    {

        if (!planetTooltip.isActiveAndEnabled)
        {
            planetTooltip.gameObject.SetActive(true);
            planetTooltip.SetTTVisibility(true, true);
            planetTooltip.SetTTArrowTarget(null);
            planetTooltip.Show(planetInfo, planetImage);
        }

    }

    public void HideInfo()
    {
        Debug.Log("hidePlanet info");
        planetTooltip.Hide();
    }
	
	// Update is called once per frame
	void Update () {
		
        

	}
}
