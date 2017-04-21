using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Controls and Management for interface variant 3. Gets and Maps input Axis from flight throttle quadrant onto energy distribution.
//Manages display of health and energy bar.

public class EnergyDistributionVariant3 : MonoBehaviour {

    public GameObject healthBar;
    public GameObject energyBar;
    public GameObject thrustBar;
    public GameObject shieldBar;
    public GameObject scanBar;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        healthBar.GetComponent<Image>().fillAmount = (float)SpaceshipGameplay.Instance.hitPoints / SpaceshipGameplay.Instance.maxHitpoints;




        if (SpaceshipGameplay.Instance.energyCapacity > 0)
        {
            energyBar.GetComponent<Image>().fillAmount = (float)SpaceshipGameplay.Instance.energy / SpaceshipGameplay.Instance.energyCapacity;
        }


        if (GameState.Instance.isPlayerNavigator())
        {

            thrustBar.GetComponent<Image>().fillAmount = (Input.GetAxis("thrustAxis") + 1) / 2;
            shieldBar.GetComponent<Image>().fillAmount = (Input.GetAxis("shieldAxis") + 1) / 2;
            scanBar.GetComponent<Image>().fillAmount = (Input.GetAxis("scanAxis") + 1) / 2;

            NetworkActions.Instance.CmdSetThrust((Input.GetAxis("thrustAxis") + 1) / 2);
            NetworkActions.Instance.CmdSetShield((Input.GetAxis("shieldAxis") + 1) / 2);
            SpaceshipGameplay.Instance.scanPower = (Input.GetAxis("scanAxis") + 1) / 2;
        }

    }
}
