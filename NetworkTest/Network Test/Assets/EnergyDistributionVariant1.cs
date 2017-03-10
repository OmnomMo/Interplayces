using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyDistributionVariant1 : MonoBehaviour {

    public Image[] thrustDisplay;
    public Image[] ShieldDisplay;
    public Image[] ShieldDisplayGrey;
    public Image[] ScannerDisplay;


	// Use this for initialization
	void Start () {
		
	}

    public void IncreaseThrusterEnergy()
    {
        SpaceshipGameplay.Instance.thrustPower += 0.2f;

        if (SpaceshipGameplay.Instance.thrustPower > 1f)
        {
            SpaceshipGameplay.Instance.thrustPower = 1f;
        }
    }

    public void DecreaseThrusterEnergy()
    {
        SpaceshipGameplay.Instance.thrustPower -= 0.2f;

        if (SpaceshipGameplay.Instance.thrustPower < 0f)
        {
            SpaceshipGameplay.Instance.thrustPower = 0f;
        }
    }

    // Update is called once per frame
    void Update () {
        UpdateEnergyBars();
	}

    void UpdateEnergyBars()
    {

        Color visible = Color.white;
        Color invisible = new Color(1, 1, 1, 0);

        int nThrusterBars = (int)(SpaceshipGameplay.Instance.thrustPower * 5);
        Debug.Log(nThrusterBars);

        thrustDisplay[4].GetComponent<Image>().color = invisible;
        thrustDisplay[3].GetComponent<Image>().color = invisible;
        thrustDisplay[2].GetComponent<Image>().color = invisible;
        thrustDisplay[1].GetComponent<Image>().color = invisible;
        thrustDisplay[0].GetComponent<Image>().color = invisible;

        switch (nThrusterBars)
        {
            case 5:
                thrustDisplay[4].GetComponent<Image>().color = visible;
                thrustDisplay[3].GetComponent<Image>().color = visible;
                thrustDisplay[2].GetComponent<Image>().color = visible;
                thrustDisplay[1].GetComponent<Image>().color = visible;
                thrustDisplay[0].GetComponent<Image>().color = visible;
                break;
            case 4:
                thrustDisplay[3].GetComponent<Image>().color = visible;
                thrustDisplay[2].GetComponent<Image>().color = visible;
                thrustDisplay[1].GetComponent<Image>().color = visible;
                thrustDisplay[0].GetComponent<Image>().color = visible;
                break;
            case 3:
                thrustDisplay[2].GetComponent<Image>().color = visible;
                thrustDisplay[1].GetComponent<Image>().color = visible;
                thrustDisplay[0].GetComponent<Image>().color = visible;
                break;
            case 2:
                thrustDisplay[1].GetComponent<Image>().color = visible;
                thrustDisplay[0].GetComponent<Image>().color = visible;
                break;
            case 1:
                thrustDisplay[0].GetComponent<Image>().color = visible;
                break;
            case 0:
                break;
        }
    }
}
