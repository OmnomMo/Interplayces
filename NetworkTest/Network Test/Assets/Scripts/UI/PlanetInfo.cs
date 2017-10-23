using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Stores string info about celestial bodies, toggles display of said info.

public class PlanetInfo : MonoBehaviour {


    public string planetName;

    public string planetInfoKey;

    public Tooltip planetTooltip;
    public Sprite planetImage;

    public Collider sphereOfInfluence;

    public bool playerStay;

	// Use this for initialization
	void Start () {

	}

    public void ShowInfo()
    {

        if (planetTooltip == null || (planetTooltip != null && !planetTooltip.isActiveAndEnabled))
        {

            
            
                planetTooltip = TooltipManager.Instance.NewTooltip(planetInfoKey, planetImage);

        }

    }

    public void HideInfo()
    {
       // Debug.Log("hidePlanet info");

        if (planetTooltip != null)
        {
            planetTooltip.Hide();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
        

	}
}
