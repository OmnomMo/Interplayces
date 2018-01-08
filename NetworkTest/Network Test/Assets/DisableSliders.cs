using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableSliders : MonoBehaviour {


    public bool hasScannerParts;
    public bool hasShieldParts;


    public Slider scanSlider;
    public Slider shieldSlider;
    public Sprite sliderDisabled;
    public Image scanSymbolGrey;
    public Image shieldSymbolGrey;

	// Use this for initialization
	void Start () {
		if (SpaceshipParts.Instance.allScanners.Length == 0)
        {
            hasScannerParts = false;
        } else
        {
            hasScannerParts = true;
        }

        if (SpaceshipParts.Instance.allShields.Length == 0)
        {
            hasShieldParts = false;
        } else
        {
            hasShieldParts = true;
        }

        if (!hasScannerParts)
        {
            scanSlider.interactable = false;
            scanSlider.targetGraphic.gameObject.GetComponent<Image>().sprite = sliderDisabled;
            scanSymbolGrey.enabled = true;
        }

        if (!hasShieldParts)
        {
            shieldSlider.interactable = false;
            shieldSlider.targetGraphic.gameObject.GetComponent<Image>().sprite = sliderDisabled;
            shieldSymbolGrey.enabled = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
