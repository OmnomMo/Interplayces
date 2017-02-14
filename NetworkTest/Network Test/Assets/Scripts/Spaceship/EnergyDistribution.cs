using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyDistribution : MonoBehaviour {

    public GameObject thrustSlider;
    public GameObject shieldSlider;
    public GameObject scanSlider;

    public bool controllerActive;

    public void UpdateThrustPower()
    {
        NetworkActions.Instance.CmdSetThrust(thrustSlider.GetComponent<Slider>().value);
    }

    public void UpdateShieldPower()
    {
        NetworkActions.Instance.CmdSetShield(shieldSlider.GetComponent<Slider>().value);
    }

    public void UpdateScanPower()
    {
        NetworkActions.Instance.CmdSetScan(scanSlider.GetComponent<Slider>().value);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       // Debug.Log("Thrust Axis: " + Input.GetAxis("thrustAxis"));
        //Debug.Log("Shield Axis: " + Input.GetAxis("shieldAxis"));
       // Debug.Log("Scan Axis: " + Input.GetAxis("scanAxis"));


        if (GameState.Instance.isPlayerNavigator() && controllerActive)
        {

            thrustSlider.GetComponent<Slider>().value = (Input.GetAxis("thrustAxis") + 1) /2;
            shieldSlider.GetComponent<Slider>().value = (Input.GetAxis("shieldAxis") + 1) / 2;
            scanSlider.GetComponent<Slider>().value = (Input.GetAxis("scanAxis") + 1) / 2;
        }

	}
}
