﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetInfo : MonoBehaviour {

    public string planetInfo;
    public Text infoTextObject;
    public Collider sphereOfInfluence;
    public Canvas planetCanvas;

    public bool playerStay;

	// Use this for initialization
	void Start () {
        infoTextObject.text = planetInfo;
	}

    public void ShowInfo()
    {
        planetCanvas.gameObject.SetActive(true);
    }

    public void HideInfo()
    {
        planetCanvas.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
        

	}
}
