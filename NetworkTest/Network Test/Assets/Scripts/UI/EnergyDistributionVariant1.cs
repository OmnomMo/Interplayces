using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Controls and Management for Interface Variant I, Everything is controlled using buttons that call methods for increasing and decreasing energy in different systems.

public class EnergyDistributionVariant1 : MonoBehaviour {

    public Image[] thrustDisplay;
    public Image[] ShieldDisplay;

    public Image shieldIconGrey;
    public Image shieldWarningIcon;

    public Image[] ShieldDisplayGrey;
    public Image[] ScannerDisplay;

    public Image healthBar;
    public Image energyBar;


	// Use this for initialization
	void Start () {
		
	}

    public void IncreaseThrusterEnergy()
    {
        float newThrustPower = 0;

        newThrustPower = SpaceshipGameplay.Instance.thrustPower + 0.2f;

        if (newThrustPower > 1f)
        {
            newThrustPower = 1f;
        }

        NetworkActions.Instance.CmdSetThrust(newThrustPower);
    }

    public void DecreaseThrusterEnergy()
    {
        float newThrustPower = 0;

        newThrustPower = SpaceshipGameplay.Instance.thrustPower - 0.2f;

        if (newThrustPower < 0f)
        {
            newThrustPower = 0f;
        }

        NetworkActions.Instance.CmdSetThrust(newThrustPower);
    }



        public void IncreaseShieldEnergy()
    {
        float newShieldPower = 0;

        newShieldPower = SpaceshipGameplay.Instance.shieldPower + 0.2f;

        if (newShieldPower > 1f)
        {
            newShieldPower = 1f;
        }

        NetworkActions.Instance.CmdSetShield(newShieldPower);
    }

    public void DecreaseShieldEnergy()
    {
        float newShieldPower = 0;

        newShieldPower = SpaceshipGameplay.Instance.shieldPower - 0.2f;

        if (newShieldPower < 0f)
        {
            newShieldPower = 0f;
        }

        NetworkActions.Instance.CmdSetShield(newShieldPower);
    }

    public void IncreaseScannerEnergy()
    {
        SpaceshipGameplay.Instance.scanPower += 0.2f;

        if (SpaceshipGameplay.Instance.scanPower > 1f)
        {
            SpaceshipGameplay.Instance.scanPower = 1f;
        }
    }

    public void DecreaseScannerEnergy()
    {
        SpaceshipGameplay.Instance.scanPower -= 0.2f;

        if (SpaceshipGameplay.Instance.scanPower < 0f)
        {
            SpaceshipGameplay.Instance.scanPower = 0f;
        }
    }

    // Update is called once per frame
    void Update () {


        //hitpointsDisplay.text = hitPoints.ToString() + " HP";
        // hitpointsDisplay.GetComponent<RectTransform>().localScale = new Vector3((float)hitPoints/maxHitpoints, 1, 1);
        healthBar.GetComponent<Image>().fillAmount = (float)SpaceshipGameplay.Instance.hitPoints / SpaceshipGameplay.Instance.maxHitpoints;

      // Debug.Log(   (float)SpaceshipGameplay.Instance.hitPoints / SpaceshipGameplay.Instance.maxHitpoints);


        if (SpaceshipGameplay.Instance.energyCapacity > 0)
        {
            //energyDisplay.GetComponent<RectTransform>().localScale = new Vector3(1, (float)energy / energyCapacity, 1);
            energyBar.GetComponent<Image>().fillAmount = (float)SpaceshipGameplay.Instance.energy / SpaceshipGameplay.Instance.energyCapacity;
        }

        UpdateEnergyBars();
	}

    void UpdateEnergyBar(Image[] displayPanels, int nBars, Color cVisible, Color cInvisible)
    {
        switch (nBars)
        {
            case 5:
                displayPanels[4].color = cVisible;
                displayPanels[3].color = cVisible;
                displayPanels[2].color = cVisible;
                displayPanels[1].color = cVisible;
                displayPanels[0].color = cVisible;
                break;
            case 4:
                displayPanels[4].color = cInvisible;
                displayPanels[3].color = cVisible;
                displayPanels[2].color = cVisible;
                displayPanels[1].color = cVisible;
                displayPanels[0].color = cVisible;
                break;
            case 3:
                displayPanels[4].color = cInvisible;
                displayPanels[3].color = cInvisible;
                displayPanels[2].color = cVisible;
                displayPanels[1].color = cVisible;
                displayPanels[0].color = cVisible;
                break;
            case 2:
                displayPanels[4].color = cInvisible;
                displayPanels[3].color = cInvisible;
                displayPanels[2].color = cInvisible;
                displayPanels[1].color = cVisible;
                displayPanels[0].color = cVisible;
                break;
            case 1:
                displayPanels[4].color = cInvisible;
                displayPanels[3].color = cInvisible;
                displayPanels[2].color = cInvisible;
                displayPanels[1].color = cInvisible;
                displayPanels[0].color = cVisible;
                break;
            case 0:
                displayPanels[4].color = cInvisible;
                displayPanels[3].color = cInvisible;
                displayPanels[2].color = cInvisible;
                displayPanels[1].color = cInvisible;
                displayPanels[0].color = cInvisible;
                break;
        }
    }

    void UpdateEnergyBars()
    {

        Color visible = Color.white;
        Color invisible = new Color(1, 1, 1, 0);

        int nThrusterBars = (int)(SpaceshipGameplay.Instance.thrustPower * 5);
        //Debug.Log(nThrusterBars);

        int nShieldBars = (int)(SpaceshipGameplay.Instance.shieldPower * 5);

        int nScannerBars = (int)(SpaceshipGameplay.Instance.scanPower * 5);

        UpdateEnergyBar(thrustDisplay, nThrusterBars, visible, invisible);
        UpdateEnergyBar(ShieldDisplay, nShieldBars, visible, invisible);
        UpdateEnergyBar(ScannerDisplay, nScannerBars, visible, invisible);



        shieldIconGrey.GetComponent<Image>().color = invisible;

        if (SpaceshipGameplay.Instance.shieldOnCooldown)
        {
            shieldWarningIcon.GetComponent<Image>().color = visible;
            shieldIconGrey.GetComponent<Image>().color = visible;
            UpdateEnergyBar(ShieldDisplayGrey, nShieldBars, visible, invisible);
        } else
        {
            UpdateEnergyBar(ShieldDisplayGrey, 0, visible, invisible);
            shieldWarningIcon.GetComponent<Image>().color = invisible;
        }
    }
}
