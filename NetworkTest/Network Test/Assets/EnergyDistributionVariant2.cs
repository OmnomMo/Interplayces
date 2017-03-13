using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyDistributionVariant2 : MonoBehaviour {


    public GameObject batteryDisplay;
    public GameObject healthDisplay;
    public GameObject batteryPointer;
    public GameObject thrustSlider;
    public GameObject shieldSlider;
    public GameObject scanSlider;
    public GameObject shieldIconGrey;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //hitpointsDisplay.text = hitPoints.ToString() + " HP";
        // hitpointsDisplay.GetComponent<RectTransform>().localScale = new Vector3((float)hitPoints/maxHitpoints, 1, 1);
  

        if (SceneManager.Instance.activeInterface == 2)
        {
            NetworkActions.Instance.CmdSetThrust(thrustSlider.GetComponent<Slider>().value);
            NetworkActions.Instance.CmdSetShield(shieldSlider.GetComponent<Slider>().value);
            SpaceshipGameplay.Instance.scanPower = scanSlider.GetComponent<Slider>().value;
        }

        healthDisplay.GetComponent<Image>().fillAmount = (SpaceshipGameplay.Instance.hitPoints / (float)SpaceshipGameplay.Instance.maxHitpoints);

        //Debug.Log((float)SpaceshipGameplay.Instance.hitPoints / SpaceshipGameplay.Instance.maxHitpoints);

        


        if (SpaceshipGameplay.Instance.energyCapacity > 0)
        {
            //energyDisplay.GetComponent<RectTransform>().localScale = new Vector3(1, (float)energy / energyCapacity, 1);
            batteryDisplay.GetComponent<Image>().fillAmount = ((float)SpaceshipGameplay.Instance.energy / SpaceshipGameplay.Instance.energyCapacity) * 0.85f;
            batteryPointer.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 90 - ((float)SpaceshipGameplay.Instance.energy / SpaceshipGameplay.Instance.energyCapacity * 150));
        }
    }
}
