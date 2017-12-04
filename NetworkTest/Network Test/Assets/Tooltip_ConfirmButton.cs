﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip_ConfirmButton : MonoBehaviour {

    public Objective confirmObjective;
    public GameObject confirmButton;
    public GameObject panel;
    public GameObject panelNoButton;

	// Use this for initialization
	void Start () {
        StartCoroutine(SetVisibilityDelayed());
	}

    public IEnumerator SetVisibilityDelayed()
    {
        yield return null;
        if (GameState.Instance.isPlayerCaptain())
        {
            confirmButton.SetActive(false);
            panel.SetActive(false);
            panelNoButton.SetActive(true);
        }
        else
        {
            confirmButton.SetActive(true);
            panel.SetActive(true);
            panelNoButton.SetActive(false);
        }


    }
    public void Confirm()
    {
        confirmObjective.Complete();
    }


	// Update is called once per frame
	void Update () {
		
	}
}
